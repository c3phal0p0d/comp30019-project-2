using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField]
    private float initialHealth = 10;

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
        base.Increment(amount);
        if (IsEmpty)
            Object.Destroy(gameObject);
    }
}
