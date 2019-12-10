using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Gameplay;
using UnityEngine;

public class Item : MonoBehaviour
{
    private ItemColor _itemColor;
    private Vector2 _size;
    private int _layerCount;
    private int _itemId;
    private GameController _gameController;

    public SpriteRenderer spriteRenderer;
    

    public void Initialize(GameController gameController, ItemColor itemColor, Vector2 size, int layerCount)
    {
        _gameController = gameController;
        _itemId = _gameController.GetItemId();
        _itemColor = itemColor;
        _size = size;
        _layerCount = layerCount;

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
    
    public void Fall()
    {
        
    }

    public void Shatter()
    {
        
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
