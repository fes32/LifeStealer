using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ParticleSystem _effect;

    private float _elapsedTime = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
            enemy.TakeDamage(5);
        else if (other.TryGetComponent(out MagicShard shard))
            Destroy(shard.gameObject);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _lifeTime)
        {
            Destroy(this.gameObject);
        }
        _rigidbody.velocity = transform.forward * _speed;
    }
}
