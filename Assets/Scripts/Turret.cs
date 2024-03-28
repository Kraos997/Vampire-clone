using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IDamageable
{
    [SerializeField] private TurretSO _turretSO;
    private HealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = new HealthSystem(_turretSO.MaxHealth);

        _healthSystem.OnHealthChange += HealthSystem_OnHealthChange;
        Utils.ManageEnemyList(this.gameObject);
    }

    public void TakeDamage(int damage)
    {
        _healthSystem.Damage(damage);

        //Damage Popup
        bool isCriticalHit = Random.Range(0, 100) < 30;
        DamagePopup.Create(transform.position, damage, isCriticalHit);
    }

    public void Death()
    {
        GameManager.EnemyList.Remove(this.gameObject);
        Destroy(gameObject);
        LevelSystem.Instance.IncreaseExperience(_turretSO.Experience);
    }

    private void HealthSystem_OnHealthChange(object sender, System.EventArgs e)
    {
        if (_healthSystem.GetHealth() == 0)
        {
            Death();
        }
    }
}
