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

    private float exhaustionCounter;


    private void Update()
    {
        if (!IsExhausted && IsEmpty)
        {
            exhaustionCounter = exhaustionTime;
        }
        else
            exhaustionCounter -= Time.deltaTime;
        exhaustionCounter = (exhaustionCounter < 0) ? 0 : exhaustionCounter;
    }

    public bool IsExhausted => exhaustionCounter > 0;
    public float UseRate => useRate;
    public float RechargeRate => rechargeRate;
}
