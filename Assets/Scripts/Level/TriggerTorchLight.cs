using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTorchLight : MonoBehaviour
{   
    [SerializeField]
    private GameObject flame;
    [SerializeField]
    private GameObject light;

    void Awake()
    {
        flame.SetActive(false);
        light.SetActive(false);
    }

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Player") {
            flame.SetActive(true);
            light.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.tag == "Player") {
            flame.SetActive(false);
            light.SetActive(false);
        }
    }

}
