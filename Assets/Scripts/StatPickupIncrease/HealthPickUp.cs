using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour

{
    [SerializeField] private PlayerStats.StatType stat;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Canvas statIncreaseMessage;

    private bool isPickedUp = false;

    void Update()
    {
        transform.Rotate(0, 1, 0, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerStats playerStat = other.GetComponent<PlayerStats>();

        if (playerStat != null && !isPickedUp)
        {
            playerStat.GetStat(stat).increment(1);
            FindObjectOfType<AudioManager>().Play("CollectStatPickup");
            statIncreaseMessage.enabled = true;
            isPickedUp = true;


            // Remove from scene leaving only health indicator message
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other){
        if (isPickedUp){
            gameObject.SetActive(false);
        }
    }
}
