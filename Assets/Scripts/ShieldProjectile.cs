using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShieldProjectile : MonoBehaviour
{
    private GameObject _centerObject;
    [SerializeField] private float _radius = 2f, _speed = 2f;
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

        Vector3 current = new(x, y);
        transform.position = current;
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        transform.RotateAround(_centerObject.transform.position, Vector3.up, _speed * Time.deltaTime);

        _angle += _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<BulletScript>(out var enemy))
        {

        }
    }
}
