using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTooClose : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float maxDamageCoolDown = 0.6f;
    private float damageCoolDown;
    private bool inflictDamage;
    private bool playerTooClose;

    // Update is called once per frame
    void Start()
    {
        damageCoolDown = maxDamageCoolDown;
    }
    void Update()
    {

        if (playerTooClose)
        {
            damageCoolDown -= Time.deltaTime;
        }

        if (damageCoolDown <= 0)
        {
            var x = PlayerManager.instance.gameObject.GetComponent<PlayerHealth>();
            x.Increment(-0.8f);
            damageCoolDown = maxDamageCoolDown;


        }

    }

    void OnTriggerEnter(Collider collider)

    {

        if (collider.gameObject.tag == "Player")
        {

            playerTooClose = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {


            playerTooClose = false;
            damageCoolDown = 2f;
        }
    }
}
