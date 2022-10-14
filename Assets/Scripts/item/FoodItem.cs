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

    void Start()
    {
  
    }

    public void Eat()
    {
        //PlayerStats.health += health;
    }



    private void Update()
    {   

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
            this.Eat();
        }

        if (Input.GetMouseButtonDown(1))
        {
            gameinventory.AddItem(this);
        }
        
            addtoinventory = false;
        }

        

    }

