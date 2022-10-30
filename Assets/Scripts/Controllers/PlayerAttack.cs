using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Canvas pauseMenu;
    [SerializeField] private RaycastAttack caster;

    private PlayerStats.Stat strengthStat;
    private Animator swordAnimator;

    [SerializeField] private float attackCooldown = 1.5f;
    private float cooldown;

    private bool canAttack = false;
    public bool isPaused = false;

    private void Start()
    {
        strengthStat = GetComponent<PlayerStats>().GetStat(PlayerStats.StatType.Strength);
        swordAnimator = GameObject.FindWithTag("Weapon").GetComponent<Animator>();
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;
        cooldown = (cooldown < 0) ? 0 : cooldown;

        if (cooldown == 0 && Input.GetMouseButtonDown(0) && !isPaused)
        {
            cooldown = attackCooldown;
            canAttack = true;
        }

        if (canAttack && cooldown < attackCooldown)
        {
            swordAnimator.SetTrigger("Attack");
            FindObjectOfType<AudioManager>().Play("WeaponAttack");
            caster.Cast(strengthStat.Value);
            canAttack = false;
        }

        // Pause game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.enabled = true;
            Time.timeScale = 0;
            FindObjectOfType<AudioManager>().Stop("BackgroundMusic");
            isPaused = true;
        }
    }
}
