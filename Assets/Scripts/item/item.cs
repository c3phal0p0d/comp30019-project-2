using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//adapted from https://medium.com/@yonem9/create-an-unity-inventory-part-1-basic-data-model-3b54451e25ec

public class Item{
    public int id;
    public string title;
    public string description;
    public Sprite icon;
    public int power;
    public int health;
    
    public Item(int id, string title, string description, Sprite icon, int power, int health){

        this.id = id;
        this.title = title;
        this.description= description;
        this.icon = Resources.Load<Sprite>("Assets/Items" + title); 
        this.power = power;
        this.health = health;
    }

    public Item(Item item)
    {

        this.id = item.id;
        this.title = item.title;
        this.description = item.description;
        this.icon = Resources.Load<Sprite>("Assets/Items" + item.title);
        this.power = item.power;
        this.health = item.health;
    }
}
