using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private UIBar3D uibar;
    [SerializeField]
    private Health health;

    private SpringyValue displayValue;

    private void Start()
    {
        displayValue = new SpringyValue(0, health.MaxValue, 1, 1, 2);
        uibar.BarWidth = health.MaxValue;
        uibar.UnderBarWidth = displayValue.DesiredValue;
        uibar.OverBarWidth = displayValue.CurrentValue;
    }

    private void Update()
    {
        displayValue.DesiredValue = health.Value;
        displayValue.UpdateValue();

        uibar.BarWidth = health.MaxValue;
        uibar.UnderBarWidth = displayValue.DesiredValue;

        float toShow = displayValue.CurrentValue;
        toShow = (toShow > health.MaxValue) ? 2 * health.MaxValue - toShow : toShow;
        toShow = (toShow < 0) ? -toShow : toShow;
        uibar.OverBarWidth = toShow;
    }
}


