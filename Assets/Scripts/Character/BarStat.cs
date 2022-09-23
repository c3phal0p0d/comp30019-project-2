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

    public void Increment(float amount)
    {
        Value += amount;
        Value = (Value < 0) ? 0 : (Value > maxValue.Value) ? maxValue.Value : Value;
    }

    public float Value { get; private set; }
    public float MaxValue => maxValue.Value;
}
