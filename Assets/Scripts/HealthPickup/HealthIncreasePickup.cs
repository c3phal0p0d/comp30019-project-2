using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncreasePickup : MonoBehaviour
{

    [SerializeField] private Vector3 rotation;
    [SerializeField] private Canvas fullHealthIndicator;
    [SerializeField] private Canvas statIncreaseMessage;

    private bool isPickedUp = false;

    void Update()
    {
        transform.Rotate(0, 1, 0, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();

        if (health != null)
        {
            if (health.Value < health.MaxValue && !isPickedUp){
                health.Increment(1);
                FindObjectOfType<AudioManager>().Play("CollectStatPickup");
                statIncreaseMessage.enabled = true;
                isPickedUp = true;

                // Remove from scene leaving only health indicator message
                transform.GetChild(0).gameObject.SetActive(false);
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
        if (isPickedUp){
            gameObject.SetActive(false);
        }
        fullHealthIndicator.enabled = false;
    }
}