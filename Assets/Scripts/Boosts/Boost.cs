using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Boost : MonoBehaviour
{
    [SerializeField] private ParticleSystem _takeEffect;
    [SerializeField] private string _description;
    [SerializeField] private float _timeAfterTake;

    public string Description => _description;
    public float TimeAfterTake => _timeAfterTake;

    public event UnityAction<Boost> BoostTaked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Activate(player);
        }
    }

    private void OnEnable()
    {
        if (_takeEffect != null)
            _takeEffect.gameObject.SetActive(false);
    }

    private void Activate(Player player)
    {
        Action(player);
        if (_takeEffect != null)
            _takeEffect.gameObject.SetActive(true);
        StartCoroutine(DisableAfterTaked());
    }

    private IEnumerator DisableAfterTaked()
    {
        float elapsedTime = 0;

        while (elapsedTime < _timeAfterTake)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        BoostTaked?.Invoke(this);
    }

    abstract public void Action(Player player);
}
