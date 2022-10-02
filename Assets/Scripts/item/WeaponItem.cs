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
    public WeaponItem thisweapon;
    public GameObject playerHands;

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

    public void Equip()
    {
        thisweapon.GetComponent<Rigidbody>().isKinematic = true;
        thisweapon.transform.position = playerHands.transform.position;
        thisweapon.transform.parent = playerHands.transform; 
    }
    private void Update()
    {
        if (canpickup == true)
        {

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("click");
                thisweapon.Equip();
            }

        }

    }
}
