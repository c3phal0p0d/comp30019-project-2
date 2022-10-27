using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastAttack : MonoBehaviour
{
    [SerializeField]
    private string selfTag;
    [SerializeField]
    private string targetTag;
    [SerializeField]
    private float attackDistance = 3;
    [SerializeField]
    private GameObject ray;
    [SerializeField]
    private float damageDelay = 1.1f;


    public void Cast(float damage)
    {

        RaycastHit[] hits = Physics.RaycastAll(ray.transform.position, ray.transform.forward, attackDistance);
        foreach (RaycastHit hit in hits)
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.CompareTag(targetTag))
            {
                StartCoroutine(DealDamage(hitObject, damage));
            }
        }
    }

    IEnumerator DealDamage(GameObject hitObject, float damage)
    {
        // Wait for player attack animation to finish playing before dealing damage to enemy
        yield return new WaitForSeconds(damageDelay);

        // Deal damage
        if (hitObject!=null){
            hitObject.GetComponentInParent<BarStat>().Increment(-damage);
        }
    }
}
