using UnityEngine;

public class GameController : MonoBehaviour
{
    public TouchDetector touchDetector;

    public void SetupEnvironment()
    {
        touchDetector.DetermineRadius();
    }

    public void LockTouch()
    {
        touchDetector.LockTouch();
    }

    public void UnlockTouch()
    {
        touchDetector.UnlockTouch();
    }

    public void ProcessTouch()
    {
        
    }
    
    private void OnEnable()
    {
        touchDetector.OnReceivedTouch += ProcessTouch;
    }

    private void OnDisable()
    {
        touchDetector.OnReceivedTouch -= ProcessTouch;
    }

    private void Awake()
    {
        SetupEnvironment();
    }
}
