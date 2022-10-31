using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    private float initialTimer;
    [SerializeField]
    private float startDistance;
    [SerializeField]
    private float endDistance;
    [SerializeField]
    private Canvas deathMessage;

    private float timer;

    private void Start()
    {
        timer = initialTimer;

        // Remove objects from scene
        GameObject[] enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] statItemsInScene = GameObject.FindGameObjectsWithTag("StatItem");

        foreach (GameObject enemy in enemiesInScene)
        {
            enemy.SetActive(false);
        }

        foreach (GameObject statItem in statItemsInScene)
        {
            statItem.SetActive(false);
        }

        // Hide UI bars from view
        if (GameObject.Find("HealthBar") != null)
        {
            GameObject.Find("HealthBar").SetActive(false);
        }
        if (GameObject.Find("StaminaBar") != null)
        {
            GameObject.Find("StaminaBar").SetActive(false);
        }
        if (GameObject.Find("AmmoBar") != null)
        {
            GameObject.Find("AmmoBar").SetActive(false);
        }

    }

    private void Update()
    {
        transform.localPosition = transform.parent.forward * Mathf.Lerp(endDistance, startDistance, timer / initialTimer);

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 0;
            deathMessage.enabled = true;


            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
