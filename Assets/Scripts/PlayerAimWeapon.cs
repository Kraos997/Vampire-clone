using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.MonoBehaviours;
using CodeMonkey.Utils;

public class PlayerAimWeapon : MonoBehaviour
{
    private PlayerManageAttacking _playeManageAttackingScript;
    private delegate void ShootDelegate(Vector3 shootPosition, Vector3 aimEndPointPosition);
    private delegate void SpawnDelegate(Vector3 position);
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
        _playeManageAttackingScript = GetComponent<PlayerManageAttacking>();

        ScourgeBulletDelegate = _playeManageAttackingScript.ScourgeBullet;
        BasicBulletDelegate = _playeManageAttackingScript.BasicBullet;
        SlowBulletDelegate = _playeManageAttackingScript.SlowBullet;

        _playeManageAttackingScript.SpinningSwordSpawn();
        _playeManageAttackingScript.OrbitingProjectileSpawn();
    }

    private void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        GameObject enemy = Utils.GetClosest(aimEndPointTransform.position, 15000f);
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

    private void Spawn(ref float timer, ref float cooldown, SpawnDelegate spawnDelegate)
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            timer = cooldown;
            spawnDelegate(shootPosition);
        }
    }
}
