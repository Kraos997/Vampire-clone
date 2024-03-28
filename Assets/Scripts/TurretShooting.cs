using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    private Vector3 _shootPosition, _aimEndPointPosition;
    private float _canShootTimer;
    [SerializeField] private float _cooldown;

    private void Update()
    {
        if (_canShootTimer > 0)
        {
            _canShootTimer -= Time.deltaTime;
        }

        if (_canShootTimer <= 0)
        {
            _canShootTimer = _cooldown;
            ShootPlayer();
        }

    }
    private void ShootPlayer()
    {
        GameObject player = Player.Instance.gameObject;
        _shootPosition = player.transform.position;
        _aimEndPointPosition = transform.position;
        Transform bulletTransform = Instantiate(GameAssets.I.pfTurretBullet, _aimEndPointPosition, Quaternion.identity);

        Vector3 shootDir = (_shootPosition - _aimEndPointPosition);
        bulletTransform.GetComponent<TurretBullet>().Setup(shootDir.normalized);
    }
}
