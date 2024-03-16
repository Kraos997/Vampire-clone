using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    private Vector3 _shootDir;

    public void Setup(Vector3 shootDir)
    {
        this._shootDir = shootDir;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));
    }

    void Update()
    {
        transform.position += _moveSpeed * Time.deltaTime * _shootDir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out var enemy))
        {
            Destroy(gameObject);
            enemy.TakeDamage();
        }
    }
}
