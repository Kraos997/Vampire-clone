using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class PlayerShootProjectile : MonoBehaviour
{
    [SerializeField] private PlayerAimWeapon playerAimWeapon;
    [SerializeField] private int _bulletCount, _orbitingProjectileCount, _spinningSwordCount;

    private void Awake()
    {
        OrbitingProjectileSpawn();
        SpinningSwordSpawn();
    }

    public void BasicBullet(Vector3 shootPosition, Vector3 aimEndPointPosition)
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

            Transform bulletTransform = Instantiate(GameAssets.I.pfBullet, aimEndPointPosition, Quaternion.identity);

            Vector3 shootDir = Quaternion.Euler(0, 0, angleTemp) * (shootPosition - aimEndPointPosition);
            bulletTransform.GetComponent<BulletScript>().Setup(shootDir.normalized);
        }
    }

    public void SlowBullet(Vector3 shootPosition, Vector3 aimEndPointPosition)
    {
        Transform bulletTransform = Instantiate(GameAssets.I.pfSlowBullet, aimEndPointPosition, Quaternion.identity);

        Vector3 shootDir = (shootPosition - aimEndPointPosition);
        bulletTransform.GetComponent<SlowBullet>().Setup(shootDir.normalized);
    }

    private void OrbitingProjectileSpawn()
    {
        float angle = Mathf.Deg2Rad * 360f / _orbitingProjectileCount;
        for (int i = 0; i < _orbitingProjectileCount; i++)
        {
            float angleTemp = angle * i;
            Transform orbitingProjectileTransform = Instantiate(GameAssets.I.pfOrbitingProjectile, transform.position, Quaternion.identity);

            orbitingProjectileTransform.GetComponent<OrbitingProjectile>().ChangeAngle(angleTemp);
        }
    }

    private void SpinningSwordSpawn()
    {
        int piece = 1;
        for (int i = 0; i < piece; i++)
        {
            float circleAngle = Mathf.PI * 2 / piece;
            float circleAngleTemp = circleAngle * i;
            if (i % 2 == 0)
            {
                float angle = circleAngle / _spinningSwordCount;
                for (int j = 0; j < _spinningSwordCount; j++)
                {
                    float angleTemp = angle * j;
                    Transform spinningSwordTransform = Instantiate(GameAssets.I.pfSpinningSword, transform.position, Quaternion.identity);

                    spinningSwordTransform.GetComponent<SpinningSword>().ChangeAngle(circleAngleTemp + angleTemp);
                }
            }
        }
    }

    public void ScourgeBullet(Vector3 shootPosition, Vector3 aimEndPointPosition)
    {
        Transform bulletTransform = Instantiate(GameAssets.I.pfScourgeBullet, aimEndPointPosition, Quaternion.identity);

        Vector3 shootDir = (shootPosition - aimEndPointPosition);
        bulletTransform.GetComponent<ScourgeBullet>().Setup(shootDir.normalized);
    }
}
