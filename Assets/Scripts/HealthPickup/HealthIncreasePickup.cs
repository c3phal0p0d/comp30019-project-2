using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncreasePickup : MonoBehaviour
{

    [SerializeField] private Vector3 rotation;
    [SerializeField] private Canvas fullHealthIndicator;
    [SerializeField] private Canvas statIncreaseMessage;

    void Update()
    {
        transform.Rotate(0, 1, 0, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();

        if (health != null)
        {
            if (health.Value < health.MaxValue){
                health.Increment(1);
                FindObjectOfType<AudioManager>().Play("CollectStatPickup");
                statIncreaseMessage.enabled = true;
                DestroyObject();
            }
            else {
                fullHealthIndicator.enabled = true;
            }
        }
    }

    IEnumerator DestroyObject(){
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other){
        fullHealthIndicator.enabled = false;
    }
}