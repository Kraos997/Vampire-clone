using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets I
    {
        get
        {
            if (_i == null)
            {
                _i = (Instantiate(Resources.Load("GameAssets") as GameObject).GetComponent<GameAssets>());
            }
            return _i;
        }
    }

    public Transform pfDamagePopup;
    public Transform pfEnemy1;
    public Transform pfEnemySlow;
    public Transform pfBullet;
    public Transform pfVisualWarning;
    public Transform pfSlowBullet;
    public Transform pfOrbitingProjectile;
}
