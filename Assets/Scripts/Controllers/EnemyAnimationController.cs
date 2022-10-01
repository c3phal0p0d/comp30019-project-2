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
        // Attack
        if (Input.GetKey(KeyCode.A)){
            animator.SetBool("Move", false);
            animator.SetTrigger("Attack");
        }
        
        // Hit
        else if (Input.GetKey(KeyCode.S)){
            animator.SetBool("Move", false);
            animator.SetTrigger("Hit");
        }

        // Death
        else if (Input.GetKey(KeyCode.D)){
            animator.SetBool("Move", false);
            animator.SetTrigger("Die");
        }
    }
}
