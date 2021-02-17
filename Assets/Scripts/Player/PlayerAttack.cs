using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _distanceAttack;
    [SerializeField] private Player _player;
    [SerializeField] private ParticleSystem[] _hitEffects;
    [SerializeField] private GameObject _chargeAttack;
    [SerializeField] private float _attackDamage;

    private PlayerInput _input;
    private float _currentAttackDamage;

    public event UnityAction KillEnemyHandler;

    private void OnEnable()
    {
        foreach (var item in _hitEffects)
        {
            item.gameObject.SetActive(false);
        }

        _currentAttackDamage = _attackDamage;

        _input = new PlayerInput();
        _input.Enable();

        _input.Player.Attack.performed += ctx => OnAttack();
        _input.Player.ChargeAttack.performed += ctx => OnChargingPiercingAttack();
    }

    private void OnAttack()
    {
        ShowAttackEffect();

        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, _distanceAttack);

        Debug.DrawRay(transform.position, transform.forward * _distanceAttack, Color.red);

        if (hits.Length > 0)
        {
            if (hits[0].collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_currentAttackDamage);
                KillEnemyHandler?.Invoke();
            }
            else if (hits[0].collider.gameObject.TryGetComponent(out MagicShard shard))
            {
                shard.Hit();
            }
        } 
    }

    private void OnChargingPiercingAttack()
    {
        var chargeAttack = Instantiate(_chargeAttack);

        chargeAttack.transform.rotation = transform.rotation;
        chargeAttack.transform.position = transform.position;
        _player.TakeDamage(25);
    }

    private void ShowAttackEffect()
    {
        int randomHitEffectIndex = Random.Range(0, _hitEffects.Length);

        _hitEffects[randomHitEffectIndex].gameObject.SetActive(true);
        _hitEffects[randomHitEffectIndex].Play();
    }

    public void UpDamage(float damage)
    {
        _currentAttackDamage += damage;
    }

    public void Reset()
    {
        _currentAttackDamage = _attackDamage;
    }
}