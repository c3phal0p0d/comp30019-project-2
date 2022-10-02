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
    public bool canpickup;
    public FoodItem thisobject;

    void Start()
    {
        canpickup = false;
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
        if (canpickup == true)
        {

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("click");
                thisobject.Eat();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("click");
                thisobject.AddtoInventory(gameinventory);
            }

        }

    }

    public void OnCollisionEnter(Collision myobject)
    {
        if (myobject.collider.name == "item")
        {
            canpickup = true;
        }
    }

    private void OnTriggerExit(Collider myobject)
    {
        canpickup = false; 

    }
}