using System;
using System.Collections.Generic;
using Gameplay;
using Unity.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TouchDetector touchDetector;
    public ColorPalette colorPalette;
    public Board board;
    private int _itemId;
    
    
    // Debug
    public GameObject itemPrefab;

    public void CreateItems()
    {
        var padding = 1f;
        
        for (var i = 0; i < 15; i++)
        {
            var previousItem = board.GetHighestItem();
            var position = previousItem != null ? previousItem.GetPosition().y + 1.6f : -1.5f;
            var itemGameObject = Instantiate(itemPrefab, new Vector3(0, position, 0), Quaternion.identity);
            var item = itemGameObject.GetComponent<Item>();
            item.Initialize(this, Utilities.EnumExtensions.GetRandomValue<ItemColor>(), Vector2.one, 1);
            board.AddItem(item);
        }
    }
    // Debug
    
    
    
    public int GetItemId()
    {
        _itemId++;
        return _itemId;
    }
    
    public void SetupEnvironment()
    {
        touchDetector.DetermineRadius();
        _itemId = -1;
        
        board = new Board();
    }

    public void LockTouch()
    {
        touchDetector.LockTouch();
    }

    public void UnlockTouch()
    {
        touchDetector.UnlockTouch();
    }

    public void ProcessTouch()
    {
        // Shatter lowest item if activated color and the color of the lowest item is same.
    }
    
    private void OnEnable()
    {
        touchDetector.OnReceivedTouch += ProcessTouch;
    }

    private void OnDisable()
    {
        touchDetector.OnReceivedTouch -= ProcessTouch;
    }

    private void Awake()
    {
        SetupEnvironment();
    }

    private void Start()
    {
        CreateItems();
    }
}
