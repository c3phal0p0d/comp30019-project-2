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
        if (Input.GetKey(KeyCode.W)){
            animator.SetBool("Move", true);
        }

        // Attack
        if (Input.GetKey(KeyCode.A)){
            animator.SetTrigger("Attack");
        }
        
        // Hit
        else if (Input.GetKey(KeyCode.S)){
            animator.SetTrigger("Hit");
        }

        // Death
        else if (Input.GetKey(KeyCode.D)){
            animator.SetTrigger("Die");
        }
    }
}
