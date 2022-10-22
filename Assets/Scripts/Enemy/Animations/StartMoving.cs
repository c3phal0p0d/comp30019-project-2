using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMoving : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
    }

}
