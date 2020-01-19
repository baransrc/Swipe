using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExplosion : MonoBehaviour, IExplosible
{
    public float MinForce;
    public float MaxForce;
    public float ExplosionRadius;
    public Transform [] ShatterFragments;
    public ShatterParticle ShatterParticle;
    private List<Vector3> _initialPositions;

    public void SetColor(Color color)
    {
        foreach (var shatterFragment in ShatterFragments)
        {
            var spriteRenderer = shatterFragment.GetComponent<SpriteRenderer>();
            
            if (spriteRenderer == null)
                throw new NullReferenceException("Current shatterFragment has no component of type SpriteRenderer.");

            spriteRenderer.color = color;
        }
    }

    public void Explode()
    {
        if (ShatterFragments == null || ShatterFragments.Length <= 0)
            throw new InvalidOperationException("ShatterFragments is empty or not initialized.");
        
        foreach (var shatterFragment in ShatterFragments)
        {
            var rigidBody = shatterFragment.GetComponent<Rigidbody2D>();

            if (rigidBody == null)
                throw new NullReferenceException("Current shatterFragment has no component of type Rigidbody2D.");

            var explosionForce = UnityEngine.Random.Range(MinForce, MaxForce);
            var rotationForce = UnityEngine.Random.Range(MinForce, MaxForce);

            rigidBody.bodyType = RigidbodyType2D.Dynamic; 
            
            AddExplosionForce(rigidBody, explosionForce, transform.position);
            AddRotationForce(rigidBody, rotationForce);
        }
    }

    public void Reset()
    {
        for (var i = 0; i < ShatterFragments.Length; i++)
        {
            ShatterFragments[i].localPosition = _initialPositions[i];
            
            var rigidBody = ShatterFragments[i].GetComponent<Rigidbody2D>();

            if (rigidBody == null)
                throw new NullReferenceException("Current shatterFragment has no component of type Rigidbody2D.");

            rigidBody.bodyType = RigidbodyType2D.Static;  
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = 0;
            rigidBody.rotation = 0;
        }

        SetColor(Color.white);
    }

    private void AddRotationForce(Rigidbody2D body, float rotationForce)
    {
        body.angularVelocity = rotationForce;
    }

    private void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float upliftModifier = 0)
	{
		var direction = body.transform.position - explosionPosition;	
		var wearoff = 1 - ( direction.magnitude / ExplosionRadius );
        var baseForce = direction.normalized * explosionForce * wearoff;
        baseForce.z = 0;

		body.AddForce(baseForce);

        if (upliftModifier != 0)
        {
            var upliftWearoff = 1 - upliftModifier / ExplosionRadius;
            var upliftForce = Vector2.up * explosionForce * upliftWearoff;
            
            body.AddForce(upliftForce);
        }
	}

    private void InitializeInitialPositions()
    {
        _initialPositions = new List<Vector3>();

        foreach (var shatterFragment in ShatterFragments)
        {
            _initialPositions.Add(shatterFragment.localPosition);
        }
    }

    private IEnumerator TryExplode()
    {
        Color [] colors = {Color.red, Color.blue, Color.yellow};

        while(true)
        {
            SetColor(colors[UnityEngine.Random.Range(0, colors.Length)]);
            yield return new WaitForSeconds(1f);
            ShatterParticle.Enable(true);
            Explode();
            yield return new WaitForSeconds(3f);
            ShatterParticle.Enable(false);
            Reset();
        }
    }

    private void Start() 
    {
        InitializeInitialPositions();
        StartCoroutine(TryExplode());
    }
}
