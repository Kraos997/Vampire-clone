using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AOEAttack : MonoBehaviour
{
    [SerializeField] private float _damage;
    private float _damageTimer;
    [SerializeField] private float _damageCooldown;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out var enemy))
        {
            if (_damageTimer > 0)
            {
                _damageTimer -= Time.deltaTime;
            }

            if (_damageTimer <= 0)
            {
                _damageTimer = _damageCooldown;
                enemy.TakeDamage((int)_damage);
            }
        }
    }
}
