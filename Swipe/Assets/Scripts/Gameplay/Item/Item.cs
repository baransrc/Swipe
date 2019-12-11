using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml;
using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Item
{
 public class Item : MonoBehaviour
{
    private ItemColor _itemColor;
    private Vector2 _size;
    private int _layerCount;
    private int _itemId;
    private GameController _gameController;

    public SpriteRenderer spriteRenderer;
    public TextMeshPro layerCountDisplay;


    public void Initialize(GameController gameController, ItemColor itemColor, int layerCount)
    {
        _gameController = gameController;
        _itemId = _gameController.GetItemId();
        _itemColor = itemColor;
        _layerCount = layerCount;
        
        AlterLayerCount(0);
        DetermineColor();
    }

    public void AlterLayerCount(int valueToAdd)
    {
        _layerCount += valueToAdd;
        
        if (_layerCount < 0) _layerCount = 0;
        
        layerCountDisplay.SetText("{0}", _layerCount);
    }

    public void DetermineColor()
    {
        spriteRenderer.color = _gameController.colorPalette.GetColor(_itemColor);
    }
    
    public void SetPosition(float x, float y)
    {
        transform.position = new Vector3(x, y, 0f);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    
    public void Fall(float velocity)
    {
        transform.position = new Vector3(0, transform.position.y + velocity, 0);
    }

    public void Recycle()
    {
        _gameController.board.PopItem(this);
        Destroy(gameObject);
    }

    public void Shatter()
    {    
        AlterLayerCount(-1);

        if (_layerCount != 0)
        {
            _itemColor = Utilities.EnumExtensions.GetRandomValue<ItemColor>();
            DetermineColor();
            return;
        }
        
        // Play Shatter animation
        // Send to pool
        
        Recycle();
    }

    public int GetItemId()
    {
        return _itemId;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}   

}


