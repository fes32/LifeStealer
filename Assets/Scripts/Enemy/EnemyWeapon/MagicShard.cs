using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShard : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _damage;

    private float _timeAfterSpawn = 0;

    public float Damage => _damage;

    private void Update()
    {
        _timeAfterSpawn += Time.deltaTime;

        if (_timeAfterSpawn >= _lifeTime)
        {
            Destroy(this.gameObject);
        }
        _rigidbody.velocity = transform.forward * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(1);
            Hit();
        }
    }

    public void Hit()
    {
        Destroy(this.gameObject);
    }
}
