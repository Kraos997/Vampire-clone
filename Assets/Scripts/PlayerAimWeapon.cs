using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.MonoBehaviours;
using CodeMonkey.Utils;

public class PlayerAimWeapon : MonoBehaviour
{
    private PlayerShootProjectile _playerShootProjectileScript;
    private delegate void ShootDelegate(Vector3 shootPosition, Vector3 aimEndPointPosition);
    ShootDelegate ScourgeBulletDelegate;
    ShootDelegate BasicBulletDelegate;
    ShootDelegate SlowBulletDelegate;

    private Vector3 aimEndPointPosition;
    private Vector3 shootPosition;

    private Transform aimPointTransform;
    private Transform aimEndPointTransform;

    [SerializeField] private float _basicBulletCooldown, _slowBulletCooldown, _scourgeBulletCooldown;
    private float _canShootBasicBulletTimer, _canShootSlowBulletTimer, _canShootScourgeBulletTimer;

    private void Awake()
    {
        aimPointTransform = transform.Find("AimPoint");
        aimEndPointTransform = aimPointTransform.Find("AimEndPointPosition");
        _playerShootProjectileScript = GetComponent<PlayerShootProjectile>();
        ScourgeBulletDelegate = _playerShootProjectileScript.ScourgeBullet;
        BasicBulletDelegate = _playerShootProjectileScript.BasicBullet;
        SlowBulletDelegate = _playerShootProjectileScript.SlowBullet;
    }

    private void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        Enemy enemy = Utils.GetClosest(aimEndPointTransform.position, 15000f);
        if (enemy != null)
        {
            aimEndPointPosition = aimEndPointTransform.position;
            shootPosition = enemy.transform.position;

            Shoot(ref _canShootScourgeBulletTimer, ref _scourgeBulletCooldown, ScourgeBulletDelegate);

            Shoot(ref _canShootBasicBulletTimer, ref _basicBulletCooldown, BasicBulletDelegate);

            Shoot(ref _canShootSlowBulletTimer, ref _slowBulletCooldown, SlowBulletDelegate);
        }
    }

    private void Shoot(ref float timer, ref float cooldown, ShootDelegate shootDelegate)
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            timer = cooldown;
            shootDelegate(shootPosition, aimEndPointPosition);
        }
    }
}
