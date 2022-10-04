using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//adapted from https://medium.com/@yonem9/create-an-unity-inventory-part-1-basic-data-model-3b54451e25ec

public class Item: MonoBehaviour
{
    public int id;
    public string title;
    public string category;
    public string description;
    public Sprite icon;
    public int power;
    public int health;
    
    public Item(int id, string title, string category, string description, Sprite icon, int power, int health){

        this.id = id;
        this.title = title;
        this.category = category;
        this.description= description;
        this.icon = Resources.Load<Sprite>("Assets/Items" + title); 
        this.power = power;
        this.health = health;
    }

    public Item(Item item)
    {

        this.id = item.id;
        this.title = item.title;
        this.category = item.category;
        this.description = item.description;
        this.icon = Resources.Load<Sprite>("Assets/Items" + item.title);
        this.power = item.power;
        this.health = item.health;
    }

    public GameObject myHands; 
    bool canpickup; 
    public GameObject myobject; 
    bool hasItem;
    public bool equipped;
    public bool addtoinventory = false;

    void Start()
    {
        canpickup = false;   
        hasItem = false;
    }

    void Update()
    {
        if (canpickup == true) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("click");
                equipped = true;
                myobject.GetComponent<Rigidbody>().isKinematic = true;
                myobject.transform.position = myHands.transform.position;
                myobject.transform.parent = myHands.transform;
            }
            if (Input.GetMouseButtonDown(1))
            {
                addtoinventory = true;
            }
        }
        if (Input.GetButtonDown("q") && hasItem == true) 
        {
            myobject.GetComponent<Rigidbody>().isKinematic = false;
            myobject.transform.parent = null; 
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "item") 
        {
            canpickup = true;
            myobject = other.gameObject; 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canpickup = false; 

    }
}
