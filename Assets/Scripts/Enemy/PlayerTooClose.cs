using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTooClose : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float damageCoolDown = 2f;
    private bool inflictDamage;
    private bool playerTooClose;

    // Update is called once per frame
    void Update()
    {

        if (playerTooClose)
        {
            damageCoolDown -= Time.deltaTime;
        }

        if (damageCoolDown <= 0)
        {
            var x = PlayerManager.instance.gameObject.GetComponent<PlayerHealth>();
            x.Increment(-0.01f);
        }

    }

    void OnTriggerEnter(Collider collider)

    {

        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Enter");
            playerTooClose = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("close");

            playerTooClose = false;
            damageCoolDown = 2f;
        }
    }
}
