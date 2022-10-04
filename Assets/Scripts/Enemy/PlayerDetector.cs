using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField]
    private string selfTag;
    [SerializeField]
    private string targetTag;
    [SerializeField]
    private float sightDistance;
    [SerializeField]
    private Transform origin;

    public bool CanDetect()
    {
        RaycastHit[] hits = Physics.RaycastAll(origin.position, PlayerManager.instance.gameObject.transform.position - origin.position, sightDistance);
        foreach(RaycastHit hit in hits)
        {
            if (hit.transform.gameObject.CompareTag(selfTag))
                continue;
            return hit.transform.gameObject.CompareTag(targetTag);
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightDistance);
    }
}
