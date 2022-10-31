using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationFinished : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.SetBool("IsAttacking", false);
    }

}
