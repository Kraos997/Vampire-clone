using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Reference Objects
    [SerializeField] private EnemySO _enemySO;
    private Rigidbody2D _rigidbody;
    private float _lastAttackTime;

    //Health System
    private HealthSystem _healthSystem;

    //Other
    public static List<Enemy> EnemyList;
    
    private void Awake()
    {
        _healthSystem = new HealthSystem(_enemySO.MaxHealth);
        _rigidbody = GetComponent<Rigidbody2D>();

        _healthSystem.OnHealthChange += HealthSystem_OnHealthChange;

        if (EnemyList == null)
        {
            EnemyList = new List<Enemy>
            {
                this
            };
        }
        else
        {
            EnemyList.Add(this);
        }
    }
    private void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
        _rigidbody.MovePosition(Vector2.MoveTowards(transform.position, Player.Instance.transform.position, _enemySO.Speed * Time.deltaTime));
    }

    public void TakeDamage(int damage)
    {
        _healthSystem.Damage(damage);

        //Damage Popup
        bool isCriticalHit = Random.Range(0, 100) < 30;
        DamagePopup.Create(this.transform.position, damage, isCriticalHit);
    }

    public int DealDamage()
    {
        return _enemySO.Damage;
    }

    public virtual void Death()
    {
        EnemyList.Remove(this);
        Destroy(gameObject);
        LevelSystem.Instance.IncreaseExperience(_enemySO.Experience);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        int damage = _enemySO.Damage;
        if (!(Time.time - _lastAttackTime < Player.Instance.ImmunityTime))
        {
            if (collision.collider.TryGetComponent<Player>(out var player))
            {
                player.TakeDamage((int)damage);
                _lastAttackTime = Time.time;
            }
        }
    }
    private void HealthSystem_OnHealthChange(object sender, System.EventArgs e)
    {
        if(_healthSystem.GetHealth() == 0)
        {
            Death();
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
