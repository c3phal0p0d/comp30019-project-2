using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinDisplay : MonoBehaviour
{
    [SerializeField] private Canvas minotaurDeath;



    // Update is called once per frame
    void Update()
    {
        minotaurDeath.enabled = true;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
