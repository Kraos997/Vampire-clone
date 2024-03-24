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
    //Bullets
    public Transform pfBullet;
    public Transform pfSlowBullet;
    public Transform pfScourgeBullet;
    public Transform pfMiniScourgeBullet;

    //Enemys
    public Transform pfEnemy1;
    public Transform pfEnemySlow;

    //Weapons
    public Transform pfOrbitingProjectile;
    public Transform pfSpinningSword;

    //VFX 
    public Transform pfVisualWarning;
    public Transform pfDamagePopup;
}
