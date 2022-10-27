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
    [SerializeField]
    LayerMask layerMask;

    public bool CanDetect()
    {
        RaycastHit hit;
        if (Physics.Raycast(origin.position, PlayerManager.instance.PlayerCenter - origin.position, out hit, sightDistance, layerMask)) {
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
