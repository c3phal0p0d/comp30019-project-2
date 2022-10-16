using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour

{
    [SerializeField] private PlayerStats.StatType stat;
    [SerializeField] private Vector3 rotation;

    void Update()
    {
        transform.Rotate(0, 1, 0, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {


        PlayerStats playerStat = other.GetComponent<PlayerStats>();

        if (playerStat != null)
        {

            playerStat.GetStat(stat).increment(1);
            Debug.Log(playerStat.GetStat(stat).Value);
            gameObject.SetActive(false);
        }
    }
}
