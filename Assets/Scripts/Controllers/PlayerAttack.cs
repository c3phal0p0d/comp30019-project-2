using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private RaycastAttack caster;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            caster.Cast();
    }
}
