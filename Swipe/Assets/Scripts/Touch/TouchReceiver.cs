using UnityEngine;

public class TouchReceiver : MonoBehaviour
{
   public float xRange = 0.5f;
   public float yRange = 0.5f;
   public Vector3 defaultPosition;
   public int id;
   public bool recievedTouch;
   public Color color;
   public Color disabledColor;
   public SpriteRenderer sprite;
   
   private bool _canReceiveTouch;
   
   private static int _currentId = 0;

   public virtual void LockTouch()
   {
       _canReceiveTouch = false;
       sprite.color = disabledColor;
   }
   
   public virtual void UnlockTouch()
   {
       _canReceiveTouch = true;
       sprite.color = color;
   }
   
   public void ReceiveTouch(Vector3 position)
   {
       if (!_canReceiveTouch) return;
       if (!IsTouchValid(position)) return;

        recievedTouch = true;
   }
   
   public Vector3 GetPosition()
   {
       return transform.position;
   }
   
   public void UpdatePosition(Vector3 position)
   {
       transform.position = position;
   }

   public void ToDefaultPosition()
   {
       transform.localPosition = defaultPosition;
   }

   public bool RecievedTouch()
   {
       return recievedTouch;
   }

   public void UnregisterTouch()
   {
       recievedTouch = false;
   }

   public bool IsTouchValid(Vector3 position)
   {
       var center = transform.position;

       if (position.x > center.x + xRange) return false;
       if (position.x < center.x - xRange) return false;
       if (position.y > center.y + yRange) return false;
       if (position.y < center.y - yRange) return false;

       return true;
   }
   
   private void SetId()
   {
       _currentId++;
       id = _currentId;
   }

   protected virtual void Awake()
   {
       recievedTouch = false;
       _canReceiveTouch = true;
       SetId();
       ToDefaultPosition();
       
   }
}
