using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem: Item
{
    public WeaponItem(int id, string title, string category, string description, Sprite icon, int power, int health) :
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

    public bool canpickup;
    public GameObject myenemy;


    public void OnCollisionEnter(Collision enemy) //if player collides with item canpickup is set to true
    {
        if (enemy.collider.name == "enemy")
        {
            if (equipped == true) //only works if weapon is equipped
            {
                //do damage to enemy
            }
        }
    }


    public void OnTriggerExit(Collider other)
    {
        //stop doing damage to enemy
    }



    private void Update()
    {
 

    }
}
