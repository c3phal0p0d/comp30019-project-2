using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMoving : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
    }
}
