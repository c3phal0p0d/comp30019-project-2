using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeath : StateMachineBehaviour
{

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.gameObject!=null){
            Object.Destroy(animator.gameObject);
        }

    }

}
