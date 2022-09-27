using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringyValue
{
    private float mass;
    private float dampening;
    private float springConstant;
    private float velocity;

    public SpringyValue(float startingValue, float desiredValue, float mass, float frequency, float dampening)
    {
        CurrentValue = startingValue;
        DesiredValue = desiredValue;
        this.mass = mass;
        this.dampening = dampening;
        springConstant = 2 * Mathf.PI * frequency;
        springConstant = mass * springConstant * springConstant + dampening * dampening / (mass * 4);
        velocity = 0;
    }

    public void UpdateValue()
    {
        velocity += ((DesiredValue - CurrentValue) * springConstant - dampening * velocity) / mass * Time.deltaTime;
        CurrentValue += velocity * Time.deltaTime;
    }

    public float CurrentValue { get; private set; }
    public float DesiredValue { get; set; }
}
