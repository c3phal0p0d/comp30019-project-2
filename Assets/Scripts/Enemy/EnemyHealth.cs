using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BarStat
{
    [SerializeField]
    private float initialHealth = 10;
    [SerializeField]
    private int enemyNumber;

    private PlayerStats.Stat maxHealth;
    private Animator animator;

    void Awake()
    {
        SetHealth(initialHealth);
        animator = GetComponent<Animator>();
    }

    public void SetHealth(float health)
    {
        initialHealth = health;
        maxHealth = new PlayerStats.Stat(0, health, health);
        SetStat(maxHealth);
    }

    public override void Increment(float amount)
    {
        if (amount<0){
            string enemyHitAudio = "Enemy" + enemyNumber + "Hit";
            FindObjectOfType<AudioManager>().Play(enemyHitAudio);
            animator.SetTrigger("Hit");
        }
        base.Increment(amount);
        if (IsEmpty){
            animator.SetTrigger("Die");
        }
    }

}
