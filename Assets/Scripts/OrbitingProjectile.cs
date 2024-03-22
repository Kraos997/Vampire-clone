using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingProjectile : MonoBehaviour
{
    private GameObject _centerObject; 
    [SerializeField] private float _radius = 2f, _speed = 2f, _angle;
    [SerializeField] private int _damage = 8, _projectileCount;

    private void Start()
    {
        _centerObject = GameObject.Find("Player");
    }
    void Update()
    {
        for (int i = 1; i < _projectileCount + 1; i++)
        {
            if (i % 2 == 0)
            {
                _angle -= 180;
            }
            else
            {
                _angle += 180;
            }
            Instantiate(GameAssets.I.pfOrbitingProjectile, transform.position, Quaternion.identity);
        }
            float x = _centerObject.transform.position.x + Mathf.Cos(_angle) * _radius;
        float y = _centerObject.transform.position.y + Mathf.Sin(_angle) * _radius;

        transform.position = new Vector3(x, y);

        _angle += _speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.TakeDamage((int)_damage);
        }
    }
}
