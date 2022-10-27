using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private PlayerDetector playerDetector;

    private Transform target;
    private Vector3 lastKnownPosition;
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        target = PlayerManager.instance.PlayerObject.transform;
        agent = GetComponent<NavMeshAgent>();
        lastKnownPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if ((target.position - transform.position).sqrMagnitude < 11 * 11)
        {
            FaceTarget();
        }
        if (playerDetector.CanDetect())
        {

            animator.SetBool("Move", true);
            if ((target.position - transform.position).sqrMagnitude <= 1.5f * 1.5f)
            {
                lastKnownPosition = transform.position;
            }

            else
            {
                lastKnownPosition = target.position;
            }


            agent.SetDestination(lastKnownPosition);


        }
        else
        {
            animator.SetBool("Move", false);
        }

        if (Vector3.Distance(transform.position, lastKnownPosition) <= agent.stoppingDistance)
        {
            FaceTarget();
        }
    }

    void FaceTarget()
    {
        // direction to target 
        Vector3 direction = (target.position - transform.position).normalized;
        //rotation where we point to that target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        //update enemy rotation to point into that direction with some smoothing 
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
