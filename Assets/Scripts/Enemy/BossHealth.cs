using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : EnemyHealth
{

    [SerializeField] private Canvas winScreen;
    [SerializeField] private GameObject pSystem;

    public override void Increment(float amount)
    {
        if (isDead)
        {    // Prevent enemy from taking further damage if already dead
            return;
        }
        string animationTrigger = "";
        if (amount < 0)
        {
            // Pause enemy forward movement while animation is playing 
            agent.isStopped = true;

            isHit = true;
            string enemyHitAudio = "Enemy" + enemyNumber + "Hit";
            FindObjectOfType<AudioManager>().Play(enemyHitAudio);
            animationTrigger = "Hit";
        }
        base.Increment(amount);
        if (IsEmpty)
        {
            GetComponent<Collider>().enabled = false;
            transform.GetChild(5).GetComponent<Collider>().enabled = false;
            // Pause enemy forward movement while animation is playing 
            agent.isStopped = true;
            isDead = true;
            animationTrigger = "Die";

            RemoveObjectsFromScene();
        }
        if (animationTrigger != "")
        {
            animator.SetTrigger(animationTrigger);
        }
    }

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("BossMusic");
        FindObjectOfType<AudioManager>().Stop("BackgroundMusic");
    }

    private void Update()
    {
        if (isDead)
        {
            gameObject.GetComponent<ShootAtPlayer>().enabled = false;
            gameObject.GetComponentInChildren<PlayerTooClose>().enabled = false;

            // Display win screen
            winScreen.enabled = true;
            pSystem.transform.rotation = Quaternion.identity;
            pSystem.transform.position = new Vector3(15.3f, 14.6f, 20.4f);
            pSystem.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("StartScene");
            }
        }
    }

    private void RemoveObjectsFromScene()
    {
        // FindObjectOfType<AudioManager>().Stop("BossMusic");

        // remove stat items
        GameObject[] statItemsInScene = GameObject.FindGameObjectsWithTag("StatItem");
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

        // Stop player movement
        GameObject player = GameObject.Find("Player");
        foreach (MonoBehaviour c in player.GetComponents<MonoBehaviour>())
        {
            c.enabled = false;
        }
    }

}
