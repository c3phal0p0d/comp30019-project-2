using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField]
    private EnemyHealth health;
    [SerializeField]
    private float lengthScale;
    [SerializeField]
    private float heightScale;
    [SerializeField]
    private float depthScale;
    [SerializeField]
    private GameObject cube;

    private void Start()
    {
        UpdateBar();
    }

    void Update()
    {
        UpdateBar();
    }

    private void UpdateBar()
    {
        float multiplier = (health.IsFull) ? 0 : 1;
        float barLength = lengthScale * health.Value / health.MaxValue;
        cube.transform.localScale = new Vector3(barLength, heightScale, depthScale) * multiplier;
    }
}
