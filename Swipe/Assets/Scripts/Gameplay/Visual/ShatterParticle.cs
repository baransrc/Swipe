using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterParticle : MonoBehaviour
{
    public ParticleSystem BeamLight;
    public ParticleSystem ExplosionTrail;
    public GameObject MainParticle;
    
    public void AdjustTint(Color color)
    {
        if (BeamLight == null || ExplosionTrail == null) 
            throw new System.NullReferenceException("There are null particles.");


        // BeamLight:
        var beamLightColors = new Gradient();
        beamLightColors.SetKeys(new GradientColorKey[] 
                                {
                                    new GradientColorKey(Color.white, 0.0f), 
                                    new GradientColorKey(color, 1.0f)
                                },
                                new GradientAlphaKey[]
                                {
                                    new GradientAlphaKey(30, 0.0f),
                                    new GradientAlphaKey(10, 1.0f)
                                });

        var beamLightColorsOverLifeTime = new Gradient();
        beamLightColorsOverLifeTime.SetKeys(new GradientColorKey[] 
                                            {
                                                new GradientColorKey(color, 0.0f), 
                                                new GradientColorKey(color, 1.0f), 
                                            },
                                            new GradientAlphaKey[]
                                            {
                                                new GradientAlphaKey(0, 0.0f),
                                                new GradientAlphaKey(0.5f, 0.082f),
                                                new GradientAlphaKey(0.5f, 0.532f),
                                                new GradientAlphaKey(0, 1.0f),
                                            });

        var beamLightMain = BeamLight.main;
        var beamColors = new ParticleSystem.MinMaxGradient(beamLightColors);
        
        beamColors.mode = ParticleSystemGradientMode.RandomColor;
        beamLightMain.startColor = beamColors;
        
        var beamLightColorOverLifeTime = BeamLight.colorOverLifetime;
        beamLightColorOverLifeTime.color = beamLightColorsOverLifeTime;

        // Explosion Trail:

        var explosionTrailGradient = new Gradient();
        explosionTrailGradient.SetKeys(new GradientColorKey[] 
                                            {
                                                new GradientColorKey(color, 0.0f), 
                                                new GradientColorKey(color, 1.0f), 
                                            },
                                            new GradientAlphaKey[]
                                            {
                                                new GradientAlphaKey(0, 0.0f),
                                                new GradientAlphaKey(0.5f, 0.035f),
                                                new GradientAlphaKey(0.5f, 0.612f),
                                                new GradientAlphaKey(0, 1.0f),
                                            });

        var explosionColors = new ParticleSystem.MinMaxGradient(explosionTrailGradient);
        explosionColors.mode = ParticleSystemGradientMode.Gradient;
        var explosionTrail = ExplosionTrail.trails;
        explosionTrail.colorOverLifetime = explosionColors;
    }

    public void Enable(bool enabled)
    {
       MainParticle.SetActive(enabled);
    }

    public float GetDuration()
    {
        return Mathf.Max(ExplosionTrail.main.duration, BeamLight.main.duration);
    }
    
    private IEnumerator TryParticles()
    {
        while(true)
        {
            Enable(true);
            yield return new WaitForSeconds(2f);
            Enable(false);
            yield return new WaitForSeconds(2f);   
        }
    }
}