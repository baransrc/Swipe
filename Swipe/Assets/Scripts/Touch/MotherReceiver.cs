using System.Collections;
using UnityEngine;

public class MotherReceiver : TouchReceiver
{
    public TrailRenderer trail;
    public TouchReceiver lastTouchedReceiver;
    
    private Color lastColor;

    public override void LockTouch()
    {
        return;
    }

    public override void UnlockTouch()
    {
        return;
    }

    public bool CanSnap(TouchReceiver receiver)
    {
        var position = receiver.GetPosition();
      
        var center = transform.position;

        if (center.x > position.x + receiver.xRange) return false;
        if (center.x < position.x - receiver.xRange) return false;
        if (center.y > position.y + receiver.yRange) return false;
        if (center.y < position.y - receiver.yRange) return false;
       
        return true;
    }
    
    public void SnapToReceiver(TouchReceiver receiver)
    {
        if (receiver == null) return;
        
        var position = receiver.GetPosition();

        if (!CanSnap(receiver)) return;

        if (lastTouchedReceiver == null) lastTouchedReceiver = receiver;
        if (receiver.id != lastTouchedReceiver.id) lastTouchedReceiver = receiver;

        UpdatePosition(position);
    }

    public void ChangeColor(TouchReceiver receiver)
    {
        if (receiver == null) return;

        var startColor = receiver.id == id ? GetColor() : receiver.GetColor();
        startColor.a = 1;

        var endColor = lastTouchedReceiver == null ? GetColor() : lastTouchedReceiver.GetColor();
        endColor = receiver.id == id ? GetColor() : endColor;
        endColor.a = 1;

        trail.startColor = startColor;
        trail.endColor = endColor;
        sprite.color = startColor;
    }

    public void ReturnDefaultStateWithDelay()
    {
        StartCoroutine(ReturnToDefaults(defaultPosition, 0.2f));
    }

    private IEnumerator ReturnToDefaults(Vector3 position, float delay)
    {
        yield return MoveToPosition(position, delay);
        Refresh();
    }
    
    private IEnumerator MoveToPosition(Vector3 position, float timeToMove)
    {
        var currentPos = transform.localPosition;
        var percentageElapsed = 0f;
        
        while(percentageElapsed < 1 && !RecievedTouch())
        {
            percentageElapsed += Time.deltaTime / timeToMove;
            transform.localPosition = Vector3.Lerp(currentPos, position, percentageElapsed);
            
            yield return null;
        }
    }

    public void Refresh()
    {
        ChangeColor(this);
        lastTouchedReceiver = null;
    }
    
    protected override void Awake()
    {
        base.Awake();
//        lastColor = color;
    }
}
