using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.MonoBehaviours;
using CodeMonkey.Utils;

public class PlayerAimWeapon : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 aimEndPointPosition;
        public Vector3 shootPosition;
    }
    private Transform aimPointTransform;
    private Transform aimEndPointTransform;

    [SerializeField] private float bulletCooldown;
    private float canShootTimer;

    private void Awake()
    {
        aimPointTransform = transform.Find("AimPoint");
        aimEndPointTransform = aimPointTransform.Find("AimEndPointPosition");
    }

    private void Update()
    {
        HandleAiming();
        HandleShooting();
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimPointTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void HandleShooting()
    {
        if (canShootTimer > 0)
        {
            canShootTimer -= Time.deltaTime;
        }
        
        if (canShootTimer <= 0)
        {
            canShootTimer = bulletCooldown;
            Shoot();
        }
        /*
        if (Input.GetMouseButton(0))
        {
            if (canShootTimer <= 0)
            {
                canShootTimer = bulletCooldown;
                Shoot();
            }
        }
        */
        
    }
    private void Shoot()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Enemy enemy = Utils.GetClosest(aimEndPointTransform.position, 15000f);
        //Debug.Log(enemy);
        if (enemy != null)
        {
            OnShoot?.Invoke(this, new OnShootEventArgs
            {
                aimEndPointPosition = aimEndPointTransform.position,
                shootPosition = enemy.transform.position,
                //shootPosition = mousePosition,
            });
        }
    }
}
