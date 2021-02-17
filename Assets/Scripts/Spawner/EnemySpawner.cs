using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : ObjectPool
{
    [SerializeField] private GameObject _template;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _minTimeDelay;
    [SerializeField] private float _maxTimeDelay;
    [SerializeField] private Player _target;

    private float _currentDelayTime = 0;
    private float _elapsedDelayTime = 0;
    private int _indexCurrentSpawnPoint = 0;

    public event UnityAction<float> EnemyDying;

    private void OnEnable()
    {
        Init(_template);
    }

    private void Update()
    {
        _elapsedDelayTime += Time.deltaTime;

        if (_elapsedDelayTime >= _currentDelayTime)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        _elapsedDelayTime = 0;
        _currentDelayTime = Random.Range(_minTimeDelay, _maxTimeDelay);

        if (TryGetObject(out GameObject enemy))
        {
            if (_indexCurrentSpawnPoint == _spawnPoints.Length-1)
                _indexCurrentSpawnPoint = 0;
            else
                _indexCurrentSpawnPoint++;

            enemy.transform.position = _spawnPoints[_indexCurrentSpawnPoint].position;

            enemy.GetComponent<EnemyMovement>().Init(_target);
            enemy.TryGetComponent(out Enemy enemyComponent);
            enemyComponent.Dying += OnEnemyDying;
        }
    }

    private void OnEnemyDying(Enemy enemy, float enemyHealthReward)
    {
        enemy.Dying -= OnEnemyDying;
        EnemyDying?.Invoke(enemyHealthReward);
        enemy.gameObject.SetActive(false);
    }
}
