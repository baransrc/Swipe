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


    public void Initialize(GameController gameController, int layerCount,  ItemColor itemColor = ItemColor.Colorless)
    {
        _gameController = gameController;
        _itemId = _gameController.GetItemId();
        _layerCount = layerCount;
        
        ChangeItemColor(itemColor);
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

    public void Shatter(ItemColor inputItemColor)
    {
        if (inputItemColor != _itemColor) return;
        
        AlterLayerCount(-1);

        if (_layerCount != 0)
        {
            ChangeItemColor();
            DetermineColor();
            return;
        }
        
        // Play Shatter animation
        // Send to pool
        
        Recycle();
    }

    private void ChangeItemColor(ItemColor newItemColor = ItemColor.Colorless)
    {
        if (newItemColor != ItemColor.Colorless)
        {
            _itemColor = newItemColor;
            DetermineColor();
            
            return;
        }

        var currentItemColor = _itemColor;
        var itemColors = new []{ItemColor.Blue, ItemColor.Green, ItemColor.Red, ItemColor.Yellow};

        var itemAbove = _gameController.board.GetItemRelativeTo(this, 1); // Get item above
        var aboveItemColor = itemAbove == null ? currentItemColor : itemAbove.GetItemColor();
            
        var itemBelow = _gameController.board.GetItemRelativeTo(this, -1); // Get item below
        var belowItemColor = itemBelow == null ? currentItemColor : itemBelow.GetItemColor();
        
        while (_itemColor == currentItemColor || _itemColor == aboveItemColor || _itemColor == belowItemColor)
        {
            _itemColor = itemColors[Random.Range(0, itemColors.Length)];
        }
    }

    public int GetItemId()
    {
        return _itemId;
    }
    
    public ItemColor GetItemColor()
    {
        return _itemColor;
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


