using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingProjectile : MonoBehaviour
{
    private GameObject _centerObject;
    [SerializeField] private float _radius = 2f, _speed = 2f;
    [SerializeField] private int _damage = 8;
    private float _angle;

    private void Start()
    {
        _centerObject = GameObject.Find("Player");
    }
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        float x = _centerObject.transform.position.x + Mathf.Cos(_angle) * _radius;
        float y = _centerObject.transform.position.y + Mathf.Sin(_angle) * _radius;

        Vector3 current = new (x, y);
        transform.position = current;

        _angle += _speed * Time.deltaTime;
    }

    public void ChangeAngle(float angle)
    {
        _angle = angle;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var enemy))
        {
            enemy.TakeDamage((int)_damage);
        }
    }
}
