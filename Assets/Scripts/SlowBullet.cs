using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBullet : BulletScript
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var enemy))
        {
            enemy.TakeDamage(Damage);
        }
    }
}
