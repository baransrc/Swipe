using UnityEngine;

public class FallManager : MonoBehaviour
{
    public GameController gameController;
    public const float Threshold = -2.1f;
    public const float Padding = 1.75f;
    public const float InitialPosition = 4f;
    private const float FallSpeed = 1.85f;
    private const float KickstarterSpeed = 3f;
    private int _fallCounter;
    
    public void LockFall()
    {
        _fallCounter++;
    }

    public void UnlockFall()
    {
        _fallCounter--;
        if (_fallCounter < 0) _fallCounter = 0;
    }

    public void ManageFalls()
    {
        if (_fallCounter > 0) return;

        var itemCount = gameController.board.GetItemCount();

        var velocity = -1 * FallSpeed * Time.deltaTime;
        var kickstarterVelocity = -1 * KickstarterSpeed * Time.deltaTime;
        
        for (int i = 0; i < itemCount; i++)
        {
            var currentItem = gameController.board.GetItem(i);

            if (currentItem == null) continue;

            var currentVelocity = (i == 0 && currentItem.GetPosition().y >= InitialPosition)
                ? kickstarterVelocity
                : velocity;

            currentItem.Fall(currentVelocity);

            if (i == 0)
            {
                if (currentItem.GetPosition().y <= Threshold)
                {
                    currentItem.SetPosition(0, Threshold);
                }

                continue;
            }

            var previousItem = gameController.board.GetItem(i - 1);
            
            if (currentItem == null) continue;

            if (currentItem.GetPosition().y - previousItem.GetPosition().y != Padding)
            {
                currentItem.SetPosition(0, previousItem.GetPosition().y + Padding);
            }
        } 
    }

    private void Update()
    {
        ManageFalls();
    }
}
