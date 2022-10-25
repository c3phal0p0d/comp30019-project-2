using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessMeshMaterial : MonoBehaviour
{
    [SerializeField]
    private Material[] dissolveMaterial;

    private SkinnedMeshRenderer meshRenderer;

    public void SwitchMaterial()
    {
        meshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
        Material[] materials = meshRenderer.materials;

        for (int i=0; i<materials.Length; i++){
            materials[i] = dissolveMaterial[i];
        }

        meshRenderer.materials = materials;

    }

    public void UpdateDissolve(float elapsedTime){
        Material[] materials = meshRenderer.materials;

        for (int i=0; i<materials.Length; i++){
            materials[i].SetFloat("_ElapsedTime", elapsedTime);
        }
    }
    
}
