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
    [SerializeField]
    private float damageDelay = 1.1f;

    public void Cast()
    {
        RaycastHit[] hits = Physics.RaycastAll(ray.transform.position, ray.transform.forward, attackDistance);
        foreach (RaycastHit hit in hits) {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.CompareTag(selfTag))
                continue;
            if (!hitObject.CompareTag(targetTag))
                break;
            StartCoroutine(DealDamage(hitObject));
        }
    }

     IEnumerator DealDamage(GameObject hitObject){
        // Wait for player attack animation to finish playing before dealing damage to enemy
        yield return new WaitForSeconds(damageDelay);

        // Deal damage
        hitObject.GetComponentInParent<BarStat>().Increment(-1); // Needs to change based on weapon
     }
}
