using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class PlayerShootProjectile : MonoBehaviour
{
    [SerializeField] private PlayerAimWeapon playerAimWeapon;
    [SerializeField] private int _bulletCount;

    private void Awake()
    {
        GetComponent<PlayerAimWeapon>().OnShoot += PlayerShootProjectile_OnShoot;
    }

    private void PlayerShootProjectile_OnShoot(object sender, PlayerAimWeapon.OnShootEventArgs e)
    {
        BasicProjectile(e);
        SlowProjectile(e);
    }

    private void BasicProjectile(PlayerAimWeapon.OnShootEventArgs e)
    {
        int angle = 0;
        for (int i = 1; i < _bulletCount + 1; i++)
        {
            int angleTemp;
            if (i % 2 == 0)
            {
                angle = i * 2;
                angleTemp = angle;
            }
            else
            {
                angleTemp = -angle;
            }

            Transform bulletTransform = Instantiate(GameAssets.I.pfBullet, e.aimEndPointPosition, Quaternion.identity);

            Vector3 shootDir = Quaternion.Euler(0, 0, angleTemp) * (e.shootPosition - e.aimEndPointPosition);
            bulletTransform.GetComponent<BulletScript>().Setup(shootDir.normalized);
        }
    }

    private void SlowProjectile(PlayerAimWeapon.OnShootEventArgs e)
    {
        Transform bulletTransform = Instantiate(GameAssets.I.pfSlowBullet, e.aimEndPointPosition, Quaternion.identity);

        Vector3 shootDir = (e.shootPosition - e.aimEndPointPosition);
        bulletTransform.GetComponent<SlowBullet>().Setup(shootDir.normalized);
    }
}