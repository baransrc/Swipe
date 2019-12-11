using UnityEngine;

public class FallManager : MonoBehaviour
{
    public GameController gameController;
    public const float Threshold = -1f;
    private const float Padding = 1.75f;
    private const float FallSpeed = 1.3f;
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
        
        for (int i = 0; i < itemCount; i++)
        {
            var currentItem = gameController.board.GetItem(i);

            if (currentItem == null) continue;
            
            currentItem.Fall(velocity);

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
