using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBullet : MonoBehaviour
{
    [SerializeField]
    private float velocityScale;
    [SerializeField]
    private float sizeScale;
    [SerializeField]
    private float lifetime;

    private float damage;

    public void Initialize(Vector3 position, Vector3 direction, float damage, float charge)
    {
        this.damage = damage;
        transform.parent.position = position;
        transform.parent.GetComponent<Rigidbody>().velocity = direction * velocityScale * charge;
        transform.parent.localScale = Vector3.one * sizeScale * (charge + 1) / 2;
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
            GameObject.Destroy(gameObject.transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().Increment(-damage);
        }
    }
}
