using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private float _speed;

    private float value;

    private void OnEnable()
    {
        _player.ChangedHealth += OnChangedHealth;
    }

    private void OnDisable()
    {
        _player.ChangedHealth -= OnChangedHealth;
    }

    private void Update()
    {
        _healthBar.value = Mathf.MoveTowards(_healthBar.value, value, _speed * Time.deltaTime);
    }

    private void OnChangedHealth(float currentHealth)
    {
        value = currentHealth;
    }

    public void Reset()
    {
        _healthBar.gameObject.SetActive(true);
        value = _player.Health ;
    }
}