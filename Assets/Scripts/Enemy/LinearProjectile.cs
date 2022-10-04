using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearProjectile : MonoBehaviour
{
    private string selfTag;
    private string targetTag;
    private float damage;
    private Vector3 velocity;
    private float lifetime;

    public void SetParameters(string selfTag, string targetTag, float damage, Vector3 velocity, float lifetime)
    {
        this.selfTag = selfTag;
        this.targetTag = targetTag;
        this.damage = damage;
        this.velocity = velocity;
        GetComponent<Rigidbody>().velocity = velocity;
        this.lifetime = lifetime;
    }

    private void Update()
    {
        if (lifetime < 0)
            Object.Destroy(gameObject);
        lifetime -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collider)
    {
        GameObject obj = collider.gameObject;
        if (obj.CompareTag(selfTag))
            return;
        if (obj.CompareTag(targetTag))
            collider.gameObject.GetComponentInParent<Health>().Increment(-damage);
        Object.Destroy(gameObject);
    }
}
