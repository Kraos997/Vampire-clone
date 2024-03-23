using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    public Vector3 ShootDir {  get; private set; }
    [field: SerializeField] public int Damage { get; private set; }

    public void Setup(Vector3 shootDir)
    {
        this.ShootDir = shootDir;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));
    }

    void Update()
    {
        transform.position += _moveSpeed * Time.deltaTime * ShootDir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out var enemy))
        {
            Destroy(gameObject);
            enemy.TakeDamage(Damage);
        }
    }
}
