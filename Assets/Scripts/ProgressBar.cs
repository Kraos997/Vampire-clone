using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _mask;

    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float experience = LevelSystem.Instance.GetExperience();
        float experienceCap = LevelSystem.Instance.GetExperienceCap();
        float fillAmount = experience / experienceCap;
        _mask.fillAmount = fillAmount;
    }

}
