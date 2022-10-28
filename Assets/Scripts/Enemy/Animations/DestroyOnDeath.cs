using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeath : StateMachineBehaviour
{
    [SerializeField]
    private bool isMinotaur = false;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.gameObject!=null&&!isMinotaur){
            Object.Destroy(animator.gameObject);
        }

        // Don't destroy if enemy is minotaur, just disable movement
        if (isMinotaur){
            Object.Destroy(animator.gameObject.GetComponent<PlayerDetector>());
            Object.Destroy(animator.gameObject.GetComponent<EnemyController>());
        }
        

    }

}
