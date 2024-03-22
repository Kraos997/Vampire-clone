using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Reference Objects
    [SerializeField] private EnemySO _enemySO;
    private GameObject _player;
    private Rigidbody2D _rigidbody;

    //Health System
    private HealthSystem _healthSystem;

    //Other
    public static List<Enemy> EnemyList;
    
    private void Awake()
    {
        _healthSystem = new HealthSystem(_enemySO.MaxHealth);
        _player = GameObject.Find("Player");
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
        _rigidbody.MovePosition(Vector2.MoveTowards(transform.position, _player.transform.position, _enemySO.Speed * Time.deltaTime));
        //transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _enemySO.Speed * Time.deltaTime);
        //_rigidbody.MovePosition((Vector2)transform.position + (_enemySO.Speed * Time.deltaTime * _player.transform.position));
    }

    public void TakeDamage(int damage)
    {
        //int randomDamage = Random.Range(_playerScript.Damage, _playerScript.Damage + 100);
        //Take Damage
        _healthSystem.Damage(damage);
        //Debug.Log(_healthSystem.GetHealth());

        //Damage Popup
        bool isCriticalHit = Random.Range(0, 100) < 30;
        DamagePopup.Create(this.transform.position, damage, isCriticalHit);
    }

    public int DealDamage()
    {
        return _enemySO.Damage;
    }

    private void Death()
    {
        EnemyList.Remove(this);
        Destroy(gameObject);
        PlayerScript.Experience += _enemySO.Experience;
        //Debug.Log("exp: " + PlayerScript.Experience);
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
