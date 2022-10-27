using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            // Pause enemy forward movement while animation is playing 
            agent.isStopped = true;


            isDead = true;
            animationTrigger = "Die";
            winScreen.enabled = true;
            pSystem.SetActive(true);
        }
        if (animationTrigger != "")
        {
            animator.SetTrigger(animationTrigger);
        }
    }

}
