using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _healthReward;
    [SerializeField] private float _health;
    [SerializeField] private float _minHealth;

    public event UnityAction<Enemy,float> Dying;

    private float _currentHealth;

    private void OnEnable()
    {
        _currentHealth= _health;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth-=damage;

        if (_currentHealth< _minHealth)
            Die();
    }

    private void Die()
    {
        Dying?.Invoke(this,_healthReward);
    }
}