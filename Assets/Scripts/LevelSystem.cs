using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem Instance { get; private set; }

    private float _level = 1;
    private float _experience = 0;
    private float _experienceCap = 100;

    public static event EventHandler OnLevelUp;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one LevelSystem instance");
        }
        Instance = this;
    }

    private void Update()
    {
        LevelUp();
    }

    public void LevelUp()
    {
        if (_experience > _experienceCap)
        {
            _experience -= _experienceCap;
            _experienceCap += 25;
            _level += 1;
            OnLevelUp?.Invoke(this, EventArgs.Empty);
        }
    }

    public void IncreaseExperience(int experience)
    {
        _experience += experience;
    }

    public float GetExperience()
    {
        return _experience;
    }

    public float GetExperienceCap()
    {
        return _experienceCap;
    }

    public float GetLevel()
    {
        return _level;
    }
}