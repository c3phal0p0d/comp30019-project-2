using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : BarStat
{
    [SerializeField]
    private float useRate;
    [SerializeField]
    private float rechargeRate;
    [SerializeField]
    private float exhaustionTime;



    private float exhaustionTimer;
    private bool privIsExhausted;

    public void ConsumeStamina()
    {
        Increment(-(UseRate + RechargeRate) * Time.deltaTime);
        if (IsEmpty)
        {
            exhaustionTimer = exhaustionTime;
            privIsExhausted = true;
        }
    }

    private void Update()
    {
        if (IsExhausted)
            exhaustionTimer -= Time.deltaTime;

        if (privIsExhausted && exhaustionTimer < float.Epsilon)
            privIsExhausted = false;

        if (!privIsExhausted)
            Increment(rechargeRate * Time.deltaTime);
    }

    public bool IsExhausted => exhaustionTimer > 0;
    public float UseRate => useRate;
    public float RechargeRate => rechargeRate;
}
