using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private UIBar3D bar;
    [SerializeField] private PlayerAmmo ammo;
    [SerializeField] private float barScale;

    void Start()
    {
        bar.BarWidth = ammo.MaxValue * barScale;
        bar.OverBarWidth = ammo.Value * barScale;
    }

    void Update()
    {
        bar.OverBarWidth = ammo.Value * barScale;
    }
}
