using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : Item
{
    public FoodItem(int id, string title, string category, string description, Sprite icon, int power, int health) :
        base(id, title, category, description, icon, power, health)
    {
        this.id = id;
        this.title = title;
        this.category = category;
        this.description = description;
        this.icon = Resources.Load<Sprite>("Assets/Items" + title);
        this.power = power;
        this.health = health;
    }

    public Inventory gameinventory;
    public FoodItem thisobject;

    void Start()
    {
  
    }

    public void Eat()
    {
        //PlayerStats.health += health;
    }

    public void AddtoInventory(Inventory myInventory)
    {
        myInventory.AddItem(thisobject);
    }

    private void Update()
    {   

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
            thisobject.Eat();
        }

        if (addtoinventory == true) //addtoinventory bool found in item class
        {
            thisobject.AddtoInventory(gameinventory);
            addtoinventory = false;
        }

        

    }

}