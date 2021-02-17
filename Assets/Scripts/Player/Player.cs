using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _minHealth;
    [SerializeField] private float _maxHealth;
    [SerializeField] private EnemySpawner[] _enemySpawners;
    [SerializeField] private float _countHealingPoints;
    [SerializeField] private float _damageValueOnTik;
    [SerializeField] private Transform _spawnPoint;

    public event UnityAction<float, int> Dying;
    public event UnityAction<float> ChangedHealth;

    private float _elapsedLifeTime = 0;
    private int _score=0;
    private float _currentHealth;
    private PlayerAttack _playerAttack;

    public float Health=>_currentHealth;

    private void OnEnable()
    {
        _playerAttack = GetComponent<PlayerAttack>();
        _currentHealth = _health;
        ChangedHealth?.Invoke(_currentHealth);
        foreach (var item in _enemySpawners)
        {
            item.EnemyDying += KillEnemy;
        }
        StartCoroutine(Bleeding());
    }
    
    private void OnDisable()
    {
        foreach (var item in _enemySpawners)
        {
            item.EnemyDying -= KillEnemy;
        }
        StopCoroutine(Bleeding());
    }

    private void Update()
    {
        _elapsedLifeTime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.TryGetComponent(out MagicShard shard))
        {
            TakeDamage(shard.Damage);
            shard.Hit();
        }
    }

    private IEnumerator Bleeding()
    {
        while (true)
        {
            TakeDamage(_damageValueOnTik*Time.deltaTime);

            yield return null;
        }
    }

    private void KillEnemy(float healthReward)
    {
        Healing(healthReward);
        _score++;
    }

    private void Die()
    {
        Dying?.Invoke((int)_elapsedLifeTime,_score);
        Time.timeScale = 0;
    }

    public void Healing(float health)
    {
        _currentHealth += health;
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;

        ChangedHealth?.Invoke(_currentHealth);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth < _minHealth)
            Die();
        ChangedHealth?.Invoke(_currentHealth);
    }

    public void UpDamage(float damage)
    {
        _playerAttack.UpDamage(damage);
    }

    public void Reset()
    {
        transform.position = _spawnPoint.position;
        _currentHealth = _health;
        _playerAttack.Reset();
        _elapsedLifeTime = 0;
        _score = 0;
    }
}