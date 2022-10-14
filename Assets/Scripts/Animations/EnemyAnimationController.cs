using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {   
        // Move
        if (Input.GetKeyDown(KeyCode.W)){
            animator.SetBool("Move", true);
        }

        // Attack
        if (Input.GetKeyDown(KeyCode.A)){
            animator.SetTrigger("Attack");
        }
        
        // Hit
        else if (Input.GetKeyDown(KeyCode.S)){
            animator.SetTrigger("Hit");
        }

        // Death
        else if (Input.GetKeyDown(KeyCode.D)){
            animator.SetTrigger("Die");
        }
    }
}
