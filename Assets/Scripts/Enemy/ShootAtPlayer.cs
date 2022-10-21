using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayer : MonoBehaviour
{
    [SerializeField]
    private PlayerDetector playerDetector;
    [SerializeField]
    private float damage = 1f;
    [SerializeField]
    private float distance = 5f;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float size = 1f;
    [SerializeField]
    private float cooldown = 5f;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private GameObject origin;
    [SerializeField]
    private float shootDelay;

    private Animator animator;
    private UnityEngine.AI.NavMeshAgent agent;
    private EnemyHealth enemyHealth;
    private float timer;
    private float lifetime;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        lifetime = distance / speed;
    }

    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;

        if (timer < float.Epsilon && playerDetector.CanDetect())
        {
            // Pause enemy forward movement
            //agent.enabled = false;

            // Play attack animation
            animator.SetTrigger("Attack");
            
            StartCoroutine("ShootProjectile");

            timer = cooldown;
        }
    }

    IEnumerator ShootProjectile(){
        // Wait for animation to finish before shooting projectile
        yield return new WaitForSeconds(shootDelay);

        if (!(enemyHealth.isHit || enemyHealth.isDead)){
            // Shoot projectile
            FindObjectOfType<AudioManager>().Play("Fireball");
            GameObject newProjectile = GameObject.Instantiate(projectile);
            newProjectile.transform.position = origin.transform.position;
            newProjectile.transform.localScale = new Vector3(size, size, size);
            newProjectile.GetComponent<LinearProjectile>().SetParameters(
                "Enemy",
                "Player",
                damage,
                (PlayerManager.instance.gameObject.transform.position - transform.position).normalized * speed,
                lifetime
            );
        }

        // Start enemy forward movement again
        //agent.enabled = true;
        
    }

}
