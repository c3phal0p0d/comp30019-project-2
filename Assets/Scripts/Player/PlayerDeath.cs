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
