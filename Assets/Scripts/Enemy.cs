using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Reference Objects
    [SerializeField] private EnemySO _enemySO;
    private GameObject _player;
    private PlayerScript _playerScript;
    private Rigidbody2D _rigidbody;

    //Health System
    private HealthSystem _healthSystem;

    //Other
    public static List<Enemy> EnemyList;
    public static int Spatialgroup;
    public static List<Enemy> EnemySpatialGroup;
    
    private void Awake()
    {
        _healthSystem = new HealthSystem(_enemySO.MaxHealth);
        _player = GameObject.Find("Player");
        _playerScript = _player.GetComponent<PlayerScript>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _healthSystem.OnHealthChange += HealthSystem_OnHealthChange;
        Spatialgroup = Utils.GetSpatialGroupStatic(transform.position.x, transform.position.y);
        if (EnemySpatialGroup == null)
        {
            EnemySpatialGroup = new List<Enemy>
            {
                this
            };
        }
        else
        {
            EnemySpatialGroup.Add(this);
        }
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
    void Update()
    {
         //transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _enemySO.Speed * Time.deltaTime);
        //_rigidbody.MovePosition((Vector2)transform.position + (_enemySO.Speed * Time.deltaTime * _player.transform.position));
       
    }

    private void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
        _rigidbody.MovePosition(Vector2.MoveTowards(transform.position, _player.transform.position, _enemySO.Speed * Time.deltaTime));

        PushNearbyEnemies();

        int newSpatialGroup = Utils.GetSpatialGroupStatic(transform.position.x, transform.position.y);
        if (newSpatialGroup != Spatialgroup)
        {
            EnemySpatialGroup.Remove(this);
            Spatialgroup = newSpatialGroup;
            EnemySpatialGroup.Add(this);
        }
    }

    private void PushNearbyEnemies()
    {
        List<Enemy> currAreaEnemies = EnemySpatialGroup.ToList();

        foreach(Enemy enemy in currAreaEnemies)
        {
            if (enemy == null) continue;
            if (enemy == this) continue;

            float distance = Mathf.Abs(transform.position.x - enemy.transform.position.x) + Mathf.Abs(transform.position.y - enemy.transform.position.y);
            if (distance < 0.2f)
            {
                Vector3 direction = transform.position - enemy.transform.position;
                direction.Normalize();
                enemy.transform.position -= direction * Time.deltaTime * _enemySO.Speed * 5;
            }
        }
    }

    public void TakeDamage()
    {
        //int randomDamage = Random.Range(_playerScript.Damage, _playerScript.Damage + 100);
        //Take Damage
        _healthSystem.Damage(_playerScript.Damage);
        //Debug.Log(_healthSystem.GetHealth());

        //Damage Popup
        bool isCriticalHit = Random.Range(0, 100) < 30;
        DamagePopup.Create(this.transform.position, _playerScript.Damage, isCriticalHit);
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
