using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAnimationFinished : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<EnemyHealth>().isHit = false;
    }

}
