using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlowTime : MonoBehaviour
{
    [SerializeField] private GameInput _gameInput;
    private bool _isTimeSlowed = false;

    private void Start()
    {
        _gameInput.OnSlowTime += GameInput_OnSlowTime;
    }

    private void GameInput_OnSlowTime(object sender, System.EventArgs e)
    {
        StartCoroutine(SlowTimeCourotine());
    }

    IEnumerator SlowTimeCourotine()
    {
        if (_isTimeSlowed == false)
        {
            _isTimeSlowed = true;
            Time.timeScale = 0.7f;
            yield return new WaitForSeconds(3f);
            Time.timeScale = 1f;
            _isTimeSlowed = false;
        }
    }
}
