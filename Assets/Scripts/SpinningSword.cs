using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class SpinningSword : MonoBehaviour
{
    private GameObject _centerObject;
    [SerializeField] private float _radius, _speed;
    [SerializeField] private float _damage;
    private float _angle;

    private void Start()
    {
        _centerObject = GameObject.Find("Player");
    }

    private void Update()
    {
        Movement();
    }

    public void  Movement()
    {
        float x = _centerObject.transform.position.x + Mathf.Cos(_angle) * _radius;
        float y = _centerObject.transform.position.y + Mathf.Sin(_angle) * _radius;

        Vector3 current = new(x, y);
        transform.position = current;

        Vector3 aimDirection = (_centerObject.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);

        _angle -= _speed * Time.deltaTime;
    }

    public void ChangeAngle(float angle)
    {
        _angle = angle;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.TakeDamage((int)_damage);
        }
    }
}
