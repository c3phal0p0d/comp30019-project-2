using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : BarStat
{
    public override void Increment(float amount)
    {
        if (amount<0){
                FindObjectOfType<AudioManager>().Play("PlayerHit");
        }
        base.Increment(amount);
    }
}