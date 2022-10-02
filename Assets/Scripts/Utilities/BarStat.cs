using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarStat : MonoBehaviour
{
    private PlayerStats.Stat maxValue;

    public void SetStat(PlayerStats.Stat toFollow)
    {
        this.maxValue = toFollow;
        Value = maxValue.Value;
    }

    public virtual void Increment(float amount)
    {
        Value += amount;
        Value = (Value < 0) ? 0 : (Value > maxValue.Value) ? maxValue.Value : Value;
    }

    public float Value { get; private set; }
    public float MaxValue => maxValue.Value;
    public bool IsEmpty => Value == maxValue.Min;
    public bool IsFull => Value == maxValue.Value;
}
