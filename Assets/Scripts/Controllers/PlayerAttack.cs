using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private RaycastAttack caster;

    private PlayerStats.Stat strengthStat;
    private Animator swordAnimator;

    [SerializeField] private float attackCooldown = 1.5f;
    private float cooldown;
    private float damageDelay = 1.1f;

    private bool canAttack = false;


    private void Start()
    {
        strengthStat = GetComponent<PlayerStats>().GetStat(PlayerStats.StatType.Strength);
        swordAnimator = GameObject.FindWithTag("Weapon").GetComponent<Animator>();
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;
        cooldown = (cooldown < 0) ? 0 : cooldown;

        if (cooldown == 0 && Input.GetMouseButtonDown(0))
        {
            cooldown = attackCooldown;
            canAttack = true;
        }

        if (canAttack && cooldown < attackCooldown - damageDelay)
        {
            swordAnimator.SetTrigger("Attack");
            FindObjectOfType<AudioManager>().Play("WeaponAttack");
            caster.Cast(strengthStat.Value);
            canAttack = false;


        }
    }
}
