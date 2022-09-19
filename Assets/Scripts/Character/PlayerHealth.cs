using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private UIBar3D uibar;
    [SerializeField]
    private float maxHealth = 5;

    private SpringyValue currentHealth;

    private void Start()
    {
        currentHealth = new SpringyValue(0, maxHealth, 1, 1, 1);
        IncrementMaxHealth(0);
        IncrementHealth(0);
    }

    public void IncrementMaxHealth(float amount)
    {
        maxHealth += amount;
        uibar.BarWidth = maxHealth;
    }

    public void IncrementHealth(float amount)
    {
        float newHealth = currentHealth.DesiredValue + amount;
        newHealth = (newHealth < 0) ? 0 : (newHealth > maxHealth) ? maxHealth : newHealth;
        currentHealth.DesiredValue = newHealth;
        uibar.UnderBarWidth = currentHealth.DesiredValue;
    }

    private void Update()
    {
        currentHealth.UpdateValue();
        uibar.OverBarWidth = currentHealth.CurrentValue;
    }
}
