﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using Gameplay;
using Gameplay.Item;
using UI;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public TouchDetector touchDetector;
    public ColorPalette colorPalette;
    public UIController uiController;
    public Board board;
    private int _itemId;
    private const int boardSize = 15;
    private int _score;
    
    // For Now:
    public GameObject itemPrefab;
    
    public int GetItemId()
    {
        _itemId++;
        return _itemId;
    }
    
    public void SetupEnvironment()
    {
        uiController.Initialize();
        touchDetector.DetermineRadius();
        
        _itemId = -1;
        _score = 0;
        
        board = new Board();
        
        uiController.SetScoreText(_score.ToString());
        CreateItems(boardSize);
    }

    public void LockTouch()
    {
        touchDetector.LockTouch();
    }

    public void UnlockTouch()
    {
        touchDetector.UnlockTouch();
    }

    public void IncreaseScore(int addition)
    {
        _score += addition;
        uiController.SetScoreText(_score.ToString());
    }

    public void ProcessTouch()
    {
        // Shatter lowest item if activated color and the color of the lowest item is same.
        var currentColor = touchDetector.lastTouchedReceiver.itemColor;
        var item = board.GetItem(0);

        if (item == null) return;
    
        item.Shatter(currentColor);
        
        CreateItems(Mathf.Max(boardSize - board.GetItemCount(), 0));
    }
    
    public void CreateItems(int count)
    {
        var padding = 1f;
        
        for (var i = 0; i < count; i++)
        {
            var previousItem = board.GetItem(board.GetItemCount() - 1); // Get highest item.
            var position = previousItem != null ? previousItem.GetPosition().y + FallManager.Padding : FallManager.InitialPosition;
            var itemGameObject = Instantiate(itemPrefab, new Vector3(0, position, 0), Quaternion.identity);
            
            var item = itemGameObject.GetComponent<Item>();
            board.AddItem(item);
            item.Initialize(this, Random.Range(1, 4));
        }
    }

    public void CheckForEndCondition()
    {
        var item = board.GetItem(0);

        if (item == null) return;

        if (item.GetPosition().y <= FallManager.Threshold)
        {
            board.Clear(); // For now 
        }
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
    }

    private void Update()
    {
    }
}
