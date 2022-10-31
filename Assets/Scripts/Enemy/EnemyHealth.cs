using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BarStat
{
    [SerializeField]
    protected float initialHealth = 10;
    [SerializeField]
    protected int enemyNumber;

    public bool isHit = false;
    public bool isDead = false;

    protected PlayerStats.Stat maxHealth;
    protected Animator animator;
    protected UnityEngine.AI.NavMeshAgent agent;

    void Awake()
    {
        SetHealth(initialHealth);
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void SetHealth(float health)
    {
        initialHealth = health;
        maxHealth = new PlayerStats.Stat(0, health, health);
        SetStat(maxHealth);
    }

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
            SpawnPickups spawnPickupsOnDeath = GetComponent<SpawnPickups>();
            if (spawnPickupsOnDeath??false)
                spawnPickupsOnDeath.DoSpawn(transform.position);
            // Pause enemy forward movement while animation is playing 
            agent.isStopped = true;

            isDead = true;
            animationTrigger = "Die";
        }
        if (animationTrigger != "")
        {
            animator.SetTrigger(animationTrigger);
        }
    }

}
