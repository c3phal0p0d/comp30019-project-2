using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private RaycastAttack caster;

    private PlayerStats.Stat strengthStat;

    private void Start()
    {
        strengthStat = GetComponent<PlayerStats>().GetStat(PlayerStats.StatType.Strength);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            caster.Cast(strengthStat.Value);
    }
}
