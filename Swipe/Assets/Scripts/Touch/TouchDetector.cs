using System;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class TouchDetector : MonoBehaviour
{
    public TouchReceiver [] touchReceivers;
    public TouchReceiver lastTouchedReceiver;
    public MotherReceiver motherReceiver;
    public Camera touchDetectorCamera;
    public CameraShake cameraShaker;
    public GameController gameController;
    public delegate void ReceivedTouch(); 
    public event ReceivedTouch OnReceivedTouch;

    private float touchRadius;
    private bool _shouldChangeColor;
    private bool _canReceiveTouch;

    public void LockTouch()
    {
        _canReceiveTouch = false;
        motherReceiver.LockTouch();
        
        foreach (var touchReceiver in touchReceivers)
        {
            touchReceiver.LockTouch();
        }
    }
    
    public void UnlockTouch()
    {
        _canReceiveTouch = true;
        motherReceiver.UnlockTouch();
        
        foreach (var touchReceiver in touchReceivers)
        {
            touchReceiver.UnlockTouch();
        }
    }
    
    private void ProcessTouch()
    {
        if (!Input.GetMouseButton(0))
        {
            motherReceiver.UnregisterTouch();
            
            foreach (var touchReceiver in touchReceivers)
            {
                touchReceiver.UnregisterTouch();
            }
            
            motherReceiver.ReturnDefaultStateWithDelay();
          
            lastTouchedReceiver = null;
            
            return;
        }
        

        var touchPosition = touchDetectorCamera.ScreenToWorldPoint(Input.mousePosition);
        touchPosition.z = 0;
        
        motherReceiver.ReceiveTouch(touchPosition);

        if (!motherReceiver.recievedTouch) return;
        
        motherReceiver.UpdatePosition(CalculateTouchReceiverPosition(touchPosition));
        
        touchPosition = motherReceiver.GetPosition();
        
        if (!_canReceiveTouch) return;
        
        foreach (var touchReceiver in touchReceivers)
        {
            if (!touchReceiver.IsTouchValid(touchPosition)) continue;
            if (lastTouchedReceiver != touchReceiver) _shouldChangeColor = true;

            touchReceiver.ReceiveTouch(touchPosition);

//            if (lastTouchedReceiver != null)
//            {
//                var lastPosition = lastTouchedReceiver.GetPosition();
//                var distance = touchReceiver.GetPosition() - lastPosition;
//                
//                Debug.DrawRay(lastPosition, distance, Color.green, 1f, false);
//            }
            
            lastTouchedReceiver = touchReceiver;
            
            break;
        }

        if (_shouldChangeColor)
        {
            motherReceiver.ChangeColor(lastTouchedReceiver);
            
            _shouldChangeColor = false;
            
            AndroidManager.HapticFeedback();
            
            cameraShaker.TriggerShake();

            if (OnReceivedTouch != null)
            {
                OnReceivedTouch();
            }
        }
        
        motherReceiver.SnapToReceiver(lastTouchedReceiver);
    }

    public void DetermineRadius()
    {
        foreach (var touchReceiver in touchReceivers)
        {
            var position = touchReceiver.GetPosition();
            var distance = Vector3.Distance(position, transform.position);
            
            touchRadius = Mathf.Max(touchRadius, distance);
        }
    }

    public float GetTouchRadius()
    {
        return touchRadius;
    }

    private Vector3 CalculateTouchReceiverPosition(Vector3 position)
    {
        var centerPosition = transform.position;
        var distance = Vector3.Distance(position, centerPosition);
        
        if (!(distance > touchRadius)) return position;
        
        var fromOriginToObject = position - centerPosition; 
            
        fromOriginToObject *= touchRadius / distance; 
            
        position = centerPosition + fromOriginToObject;

        return position;
    }

    private void Awake()
    {
        _canReceiveTouch = true;
    }

    private void Update()
    {
        ProcessTouch();
    }
}
