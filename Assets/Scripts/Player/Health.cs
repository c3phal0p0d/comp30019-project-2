using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : BarStat
{
    public override void Increment(float amount)
    {
        base.Increment(amount);

        Debug.Log("Incrementing by " + amount);
    }
}