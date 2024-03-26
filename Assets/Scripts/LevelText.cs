using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;

    void Awake()
    {
        _levelText = GetComponent<TextMeshProUGUI>();
        LevelSystem.OnLevelUp += PlayerScript_OnLevelUp;
        SetLevelText();
    }

    private void PlayerScript_OnLevelUp(object sender, System.EventArgs e)
    {
        SetLevelText();
    }

    public void SetLevelText()
    {
        float level = LevelSystem.Instance.GetLevel();
        _levelText.SetText("Level: " + level);
    }
}
