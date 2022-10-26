using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BarStat
{
    [SerializeField]
    private float initialHealth = 10;
    [SerializeField]
    private int enemyNumber;

    public bool isHit = false;
    public bool isDead = false;

    private PlayerStats.Stat maxHealth;
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent agent;

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
        if (isDead){    // Prevent enemy from taking further damage if already dead
            return;
        }
        string animationTrigger = "";
        if (amount<0){
            // Pause enemy forward movement while animation is playing 
            agent.isStopped = true;

            isHit = true;
            string enemyHitAudio = "Enemy" + enemyNumber + "Hit";
            FindObjectOfType<AudioManager>().Play(enemyHitAudio);
            animationTrigger = "Hit";
        }
        base.Increment(amount);
        if (IsEmpty){
            // Pause enemy forward movement while animation is playing 
            agent.isStopped = true;

            isDead = true;
            animationTrigger = "Die";
        }
        if (animationTrigger!=""){
            animator.SetTrigger(animationTrigger);
        }
    }

}
