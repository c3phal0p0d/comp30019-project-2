using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOnLight : MonoBehaviour
{

    private GameObject torchLight;
    private GameObject flame;




    void Awake()
    {

        flame = transform.GetChild(1).gameObject;
        torchLight = transform.GetChild(0).gameObject;
        flame.SetActive(false);
        torchLight.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log(col);
        if (col.tag == "MainCamera")
        {
            flame.SetActive(false);
            torchLight.SetActive(true);



        }
    }

    private void OnTriggerExit(Collider col)
    {
        Debug.Log(col);
        if (col.tag == "MainCamera")
        {
            flame.SetActive(false);
            torchLight.SetActive(true);



        }
    }
}
