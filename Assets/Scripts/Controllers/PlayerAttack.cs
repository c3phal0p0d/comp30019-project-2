using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Canvas pauseMenu;
    [SerializeField] private RaycastAttack caster;
    [SerializeField] private PlayerAmmo ammo;

    private PlayerStats.Stat strengthStat;
    private Animator swordAnimator;

    [SerializeField] private float meleeAttackRange = 3f;
    [SerializeField] private float meleeAttackCooldownTime = 1.5f;
    [SerializeField] private float meleeDamageDelay = 0.8f;
    private float meleeAttackCooldown;

    [SerializeField] private GameObject rangedAttackBulletPrefab;
    [SerializeField] private Transform rangedAttackBulletOrigin;
    [SerializeField] private float rangedAttackRange = 100f;
    [SerializeField] private float minRangedAttackCharge = 0.5f;
    [SerializeField] private float rangedAttackChargeTime;
    [SerializeField] private float rangedAttackAmmoUse;
    private float rangedAttackCharge;
    private bool isChargingRangedAttack;

    private bool canAttack = false;
    public bool isPaused = false;



    private void Start()
    {
        strengthStat = GetComponent<PlayerStats>().GetStat(PlayerStats.StatType.Strength);
        swordAnimator = GameObject.FindWithTag("Weapon").GetComponent<Animator>();
    }

    private void Update()
    {
        meleeAttackCooldown -= Time.deltaTime;
        meleeAttackCooldown = (meleeAttackCooldown < 0) ? 0 : meleeAttackCooldown;

        if (Input.GetMouseButtonDown(1) && !ammo.IsEmpty && !isPaused)
        {
            isChargingRangedAttack = true;
            rangedAttackCharge = 0;
        }

        if (!isChargingRangedAttack && meleeAttackCooldown == 0 && Input.GetMouseButtonDown(0) && !isPaused)
        {
            meleeAttackCooldown = meleeAttackCooldownTime;
            canAttack = true;
        }

        if (isChargingRangedAttack)
        {
            float prevCharge = rangedAttackCharge;
            rangedAttackCharge += Time.deltaTime / rangedAttackChargeTime;
            if (rangedAttackCharge > 1f)
                rangedAttackCharge = 1f;
            float chargeChange = rangedAttackCharge - prevCharge;
            ammo.Increment(-rangedAttackAmmoUse * chargeChange);
            if (Input.GetMouseButtonUp(1) || ammo.IsEmpty)
            {
                isChargingRangedAttack = false;
                // Raycast attack
                if (rangedAttackCharge > minRangedAttackCharge)
                {
                    GameObject bullet = GameObject.Instantiate(rangedAttackBulletPrefab);
                    float damage = strengthStat.Value * rangedAttackCharge * rangedAttackCharge;
                    bullet.GetComponentInChildren<BouncyBullet>().Initialize(rangedAttackBulletOrigin.position, rangedAttackBulletOrigin.forward, damage, rangedAttackCharge);
                }
            }
        }

        if (canAttack && meleeAttackCooldown < meleeAttackCooldownTime)
        {
            swordAnimator.SetTrigger("Attack");
            FindObjectOfType<AudioManager>().Play("WeaponAttack");
            caster.Cast(strengthStat.Value, meleeAttackRange, meleeDamageDelay);
            canAttack = false;
        }

        // Pause game
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.enabled = true;
            Time.timeScale = 0;
            FindObjectOfType<AudioManager>().Stop("BackgroundMusic");
            FindObjectOfType<AudioManager>().Stop("BossMusic");
            isPaused = true;
        }
    }
}
