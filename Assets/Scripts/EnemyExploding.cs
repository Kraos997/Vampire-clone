using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExploding : Enemy
{
    public override void Death()
    {
        base.Death();
        Instantiate(GameAssets.I.pfAOEAttack, transform.position, Quaternion.identity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Player>(out _))
        {
            Death();
        }
    }
}
