using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private MagicShard _shard;
    [SerializeField] private float _minAttackTimeDelay;
    [SerializeField] private float _maxAttackTimeDelay;
    [SerializeField] private Transform _shootPoint;

    private float _elapsedTimeAfterLastAttack = 0;
    private float _currentDelayTime = 0;

    private void OnEnable()
    {
        _shard.gameObject.SetActive(false);
    }

    private void Update()
    {
        _elapsedTimeAfterLastAttack += Time.deltaTime;

        if (_elapsedTimeAfterLastAttack >= _currentDelayTime)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _elapsedTimeAfterLastAttack = 0;
        _currentDelayTime = Random.Range(_minAttackTimeDelay, _maxAttackTimeDelay);

        var shard = Instantiate(_shard);
        shard.gameObject.SetActive(true);

        shard.transform.rotation = transform.rotation;
        shard.transform.position = _shootPoint.position;
    }
}
