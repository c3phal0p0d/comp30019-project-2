using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaUI : MonoBehaviour
{
    [SerializeField]
    private UIBar3D uibar;
    [SerializeField]
    private Stamina stamina;

    void Start()
    {
        DisplayTheValue();
    }

    void Update()
    {
        DisplayTheValue();
    }

    private void DisplayTheValue()
    {
        uibar.BarWidth = stamina.MaxValue;
        uibar.UnderBarWidth = stamina.Value;
        float toShow = stamina.Value - 0.5f;
        uibar.OverBarWidth = (toShow < 0) ? 0 : toShow;
    }
}
