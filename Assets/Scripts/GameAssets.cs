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

    [Header("Bullets:")]
    public Transform pfBullet;
    public Transform pfSlowBullet;
    public Transform pfScourgeBullet;
    public Transform pfMiniScourgeBullet;

    [Header("Enemys:")]
    public Transform pfEnemy1;
    public Transform pfEnemySlow;

    [Header("Turrets:")]
    public Transform pfTurret;

    [Header("Turret Attacks:")]
    public Transform pfTurretBullet;

    [Header("Weapons:")]
    public Transform pfOrbitingProjectile;
    public Transform pfSpinningSword;
    public Transform pfAOEAttack;

    [Header("VFX:")]
    public Transform pfVisualWarning;
    public Transform pfDamagePopup;
}
