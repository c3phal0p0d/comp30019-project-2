using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTorchLight : MonoBehaviour
{   
    ParticleSystem flame;
    Light light;

    void Awake()
    {
        flame = GetComponentInChildren(typeof(ParticleSystem)) as ParticleSystem;
        light = GetComponentInChildren(typeof(Light)) as Light;
        flame.enableEmission = false;
        light.enabled = false;
    }

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Player") {
            flame.enableEmission = true;
            light.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.tag == "Player") {
            flame.enableEmission = false;
            light.enabled = false;
        }
    }

}
