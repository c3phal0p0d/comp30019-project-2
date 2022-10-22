using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private RaycastAttack caster;

    private PlayerStats.Stat strengthStat;
    private Animator swordAnimator;

    private void Start()
    {
        strengthStat = GetComponent<PlayerStats>().GetStat(PlayerStats.StatType.Strength);
        swordAnimator = GameObject.FindWithTag("Weapon").GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)){
            caster.Cast(strengthStat.Value);
            swordAnimator.SetTrigger("Attack");
            FindObjectOfType<AudioManager>().Play("WeaponAttack");
        }
    }
}
