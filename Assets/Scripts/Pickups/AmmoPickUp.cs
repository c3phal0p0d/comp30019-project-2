using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Canvas fullAmmoIndicator;
    [SerializeField] private Canvas statIncreaseMessage;
    [SerializeField] private float increaseAmount;

    private bool isPickedUp = false;

    void Update()
    {
        transform.Rotate(0, 1, 0, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        Ammo ammo = other.GetComponent<Ammo>();

        if (ammo != null)
        {
            if (ammo.Value < ammo.MaxValue && !isPickedUp)
            {
                ammo.Increment(1);
                FindObjectOfType<AudioManager>().Play("CollectStatPickup");
                statIncreaseMessage.enabled = true;
                isPickedUp = true;

                // Remove from scene leaving only health indicator message
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                fullAmmoIndicator.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPickedUp)
        {
            gameObject.SetActive(false);
        }
        fullAmmoIndicator.enabled = false;
    }
}
