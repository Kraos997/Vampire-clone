using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro _textMeshPro;
    private float _disappearTimer;
    private Color _textColor;
    private static int _sordingOrder;

    public static DamagePopup Create(Vector2 position, int damageAmount, bool isCriticalHit)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.I.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit);

        return damagePopup;
    }
    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, bool isCriticalHit)
    {
        _textMeshPro.SetText(damageAmount.ToString());
        if (!isCriticalHit)
        {
            _textMeshPro.fontSize = 4;
            _textColor = UtilsClass.GetColorFromString("FFFFFF");
        }
        else
        {
            _textMeshPro.fontSize = 6;
            _textColor = UtilsClass.GetColorFromString("FF0B00");
        }
        _textMeshPro.color = _textColor;
        _disappearTimer = 1f;

        _sordingOrder++;
        _textMeshPro.sortingOrder = _sordingOrder;
    }

    private void Update()
    {
        float moveYSpeed = 1f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        _disappearTimer -= Time.deltaTime;
        if (_disappearTimer < 0f)
        {
            float disappearSpeed = 3f;
            _textColor.a -= disappearSpeed * Time.deltaTime;
            _textMeshPro.color = _textColor;
            if(_textColor.a < 0f)
            {
                Destroy(gameObject);
            }
        }
    }

}
