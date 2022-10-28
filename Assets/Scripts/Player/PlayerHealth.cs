
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField]
    private GameObject deathObject;

    public override void Increment(float amount)
    {
        base.Increment(amount);
        if (IsEmpty)
        {
            FindObjectOfType<AudioManager>().Stop("BackgroundMusic");
            FindObjectOfType<AudioManager>().Play("Lose");
            foreach (MonoBehaviour c in GetComponents<MonoBehaviour>())
            {
                c.enabled = false;
            }
            deathObject.SetActive(true);
        }
    }
}
