using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_3D_Test : MonoBehaviour
{
    [SerializeField]
    private float startValue = 0;
    [SerializeField]
    private float desiredValue = 1;
    [SerializeField]
    private float mass = 1;
    [SerializeField]
    private float frequency = 1;
    [SerializeField]
    private float dampening = 0.1f;

    private SpringyValue springyValue;

    void Start()
    {
        springyValue = new SpringyValue(startValue, desiredValue, mass, frequency, dampening);
        this.transform.localScale = new Vector3(springyValue.CurrentValue, springyValue.CurrentValue, springyValue.CurrentValue);
    }

    void Update()
    {
        springyValue.UpdateValue();
        this.transform.localScale = new Vector3(springyValue.CurrentValue, springyValue.CurrentValue, springyValue.CurrentValue);
    }
}
