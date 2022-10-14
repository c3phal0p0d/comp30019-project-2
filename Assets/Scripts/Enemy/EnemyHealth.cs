using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField]
    private float initialHealth = 10;
    [SerializeField]
    private float damageDelay = 1.1f;

    private PlayerStats.Stat maxHealth;

    void Awake()
    {
        SetHealth(initialHealth);
    }

    public void SetHealth(float health)
    {
        initialHealth = health;
        maxHealth = new PlayerStats.Stat(0, health, health);
        SetStat(maxHealth);
    }

    public override void Increment(float amount)
    {
        StartCoroutine(TakeDamage(amount));
    }

    IEnumerator TakeDamage(float amount){
        // Wait for player attack animation to finish playing before damaging enemy
        yield return new WaitForSeconds(damageDelay);

        // Enemy takes damage
        base.Increment(amount);
        if (amount<0){
            FindObjectOfType<AudioManager>().Play("EnemyHit");
        }
        if (IsEmpty)
            Object.Destroy(gameObject);
    }
}
