using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://medium.com/@yonem9/create-an-unity-inventory-part-1-basic-data-model-3b54451e25ec

public class ItemDatabase : MonoBehaviour
{
   

    public List<Item> items = new List<Item>();

    private void Awake(){
        BuildDatabase();
    }

    public Item getItem(string itemName){
        return items.Find(item => item.title == itemName);
    }

    void BuildDatabase() {
        items = new List<Item>() {
            new Item(0, "apple", "FoodItem","food item restoring 1 health", Resources.Load<Sprite>("Assets/Items/apple"), 1, 2)
        };
    
        }
    }

