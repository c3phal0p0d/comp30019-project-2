using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastAttack : MonoBehaviour
{
    // Need something to get equipped weapon / attack method
    [SerializeField]
    private string selfTag;
    [SerializeField]
    private string targetTag;
    [SerializeField]
    private float attackDistance = 3;
    [SerializeField]
    private GameObject ray;

    public void Cast(float damage)
    {
        RaycastHit[] hits = Physics.RaycastAll(ray.transform.position, ray.transform.forward, attackDistance);
        foreach (RaycastHit hit in hits)
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.CompareTag(selfTag))
                continue;
            if (!hitObject.CompareTag(targetTag))
                break;
            hitObject.GetComponentInParent<Health>().Increment(-damage);
        }
    }
}
