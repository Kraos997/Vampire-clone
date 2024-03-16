using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class HealthSystem
{
    public event EventHandler OnHealthChange;

    private int _health;
    private int _healthMax;

    public HealthSystem(int healthMax)
    {
        this._healthMax = healthMax;
        _health = healthMax;
    }

    public int GetHealth()
    {
        return _health;
    }

    public int GetHealthMax()
    {
        return _healthMax;
    }

    public float GetHealthPercent()
    {
        return (float)_health / _healthMax;
    }

    public void Damage(int damageAmount)
    {
        _health -= damageAmount;
        if (_health < 0)
        {
            _health = 0;
        }
        OnHealthChange?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        _health += healAmount;
        if (_health < 0)
        {
            _health = 0;
        }
        OnHealthChange?.Invoke(this, EventArgs.Empty);
    }

    public void IncreaseHealthMax(int increaseHealthMaxAmount)
    {
        _healthMax += increaseHealthMaxAmount;
        _health += increaseHealthMaxAmount;
    }

    public void DecreaseHealthMax(int decreaseHealthMaxAmount)
    {
        _healthMax -= decreaseHealthMaxAmount;
        _health -= decreaseHealthMaxAmount;
    }
}
