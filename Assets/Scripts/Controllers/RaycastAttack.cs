using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    [SerializeField]
    private LayerMask layerMask;


    public void Cast(float damage)
    {

        RaycastHit[] hits = Physics.RaycastAll(ray.transform.position, ray.transform.forward, attackDistance, layerMask);
        foreach (RaycastHit hit in SortByDistance(hits))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.CompareTag(selfTag))
                continue;
            if (hitObject.CompareTag(targetTag))
            {
                StartCoroutine(DealDamage(hitObject, damage));
            }
            else
                break;
        }
    }

    IEnumerator DealDamage(GameObject hitObject, float damage)
    {
        // Wait for player attack animation to finish playing before dealing damage to enemy
        yield return new WaitForSeconds(damageDelay);

        // Deal damage
        if (hitObject != null)
        {
            hitObject.GetComponentInParent<BarStat>().Increment(-damage);
        }
    }

    private List<RaycastHit> SortByDistance(RaycastHit[] hits)
    {
        List<RaycastHit> sortedHits = new List<RaycastHit>(hits.Length);
        foreach (RaycastHit hit in hits)
            sortedHits.Add(hit);
        sortedHits.Sort((hit2, hit1) => (int)Mathf.Sign(hit2.distance - hit1.distance));

        return sortedHits;
    }
}
