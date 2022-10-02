using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ItemDatabase
{
    public List<Item> items = new List<Item>();

    private void Awake()
    {
     //   BuildDatabase();
    }

    private void Update()
    {
      //  UpdateDatabase();
    }

    public Item getItem(string itemName)
    {
        return items.Find(item => item.title == itemName);
    }

    void BuildDatabase()//items in inventory at the start of game

    {
        items = new List<Item>() {
            new Item(0, "apple", "FoodItem","food item restoring 1 health", Resources.Load<Sprite>("Assets/Items/apple"), 1, 2)
        };

    }

    void UpdateDatabase()
    {
    }

    public void AddItem(Item newitem)
    {
        items.Add(newitem);

    }

    void DeleteItem(Item itemToRemove)
    {
        if (itemToRemove != null)
            items.Remove(itemToRemove);

    }
}