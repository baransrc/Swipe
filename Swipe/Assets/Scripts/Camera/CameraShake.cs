using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeDuration = 0f;
    
    public float shakeMagnitude = 0.2f;
    public float dampingSpeed = 0.5f;
    public Vector3 initialPosition;

    public void TriggerShake() 
    {
        shakeDuration = 0.2f;
    }

    private void Shake()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
   
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }
    private void OnEnable()
    {
        initialPosition = transform.localPosition;
    }
    
    private void Update()
    {
        Shake();
    }
}
