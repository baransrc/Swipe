using System.Collections;
using System.Collections.Generic;
using Gameplay.Item;
using UnityEngine;

public class Board
{
   private List<Item> _items;

   public Board()
   {
      _items = new List<Item>();
   }

   public void AddItem(Item item)
   {
      if (item == null) return;
         
      _items.Add(item);
   }

   public Item GetRandomItem()
   {
      if (_items.Count <= 0) return null;

      return _items[Random.Range(0, _items.Count)];
   }

   public int GetItemCount()
   {
      return _items.Count;
   }
   public Item GetItem(int index)
   {
      if (index < 0) return null;
      if (index >= _items.Count) return null;

      return _items[index];
   }
   
   public Item GetLowestItem()
   {
      if (_items.Count <= 0) return null;

      return _items[0];
   }
   
   public Item GetHighestItem()
   {
      if (_items.Count <= 0) return null;

      return _items[_items.Count - 1];
   }

   public void PopItem(Item item)
   {
      if (_items.Count <= 0) return;
      
      _items.RemoveAt(_items.FindIndex(x => x.GetItemId() == item.GetItemId()));
   }

   public void PopItem(int index)
   {
      if (_items.Count <= index) return;
      
      _items.RemoveAt(index);
   }

   public void PopLowestItem()
   {
      if (_items.Count <= 0) return;
      
      _items.RemoveAt(0);
   }
}
