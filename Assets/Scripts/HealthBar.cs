using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthSystem _healthSystem;

    public void Setup(HealthSystem healthSystem)
    {
        this._healthSystem = healthSystem;

        healthSystem.OnHealthChange += HealthSystem_OnHealthChange;
    }

    private void HealthSystem_OnHealthChange(object sender, System.EventArgs e)
    {
        transform.Find("Bar").localScale = new Vector3(_healthSystem.GetHealthPercent(), 1);
    }
}
