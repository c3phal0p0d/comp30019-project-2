using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    [SerializeField] private Vector3 rotation;

    void Update()
    {
        transform.Rotate(0, 1, 0, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {

        Health health = other.GetComponent<Health>();

        if (health != null)
        {
            health.Increment(1);
            FindObjectOfType<AudioManager>().Play("CollectStatPickup");
            gameObject.SetActive(false);
        }
    }
}