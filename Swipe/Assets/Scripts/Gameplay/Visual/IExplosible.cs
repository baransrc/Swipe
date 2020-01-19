using UnityEngine;

public interface IExplosible
{
    void SetColor(Color color);
    void Reset();
    void Explode();
}