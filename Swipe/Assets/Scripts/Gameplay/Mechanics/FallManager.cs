using UnityEngine;

public class FallManager : MonoBehaviour
{
    public GameController gameController;
    private const float Padding = 1.75f;
    private const float FallSpeed = 5f;
    private const float Threshold = -1f;

    public void ManageFalls()
    {
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
