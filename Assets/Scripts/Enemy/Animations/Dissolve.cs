using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : StateMachineBehaviour
{
    [SerializeField]
    private float dissolveSpeedMultiplier = 0.5f;

    private AccessMeshMaterial[] meshMaterialAccesors;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Disable empty health bar
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(false);

        // Change to dissolve materials
        meshMaterialAccesors = animator.GetComponentsInChildren<AccessMeshMaterial>();
        foreach (AccessMeshMaterial meshMaterialAccesor in meshMaterialAccesors){
            meshMaterialAccesor.SwitchMaterial();
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Play dissolve effect during death animation
        meshMaterialAccesors = animator.GetComponentsInChildren<AccessMeshMaterial>();
        foreach (AccessMeshMaterial meshMaterialAccesor in meshMaterialAccesors){
            meshMaterialAccesor.UpdateDissolve((stateInfo.normalizedTime%1)*stateInfo.length*dissolveSpeedMultiplier);
        }

    }

}
