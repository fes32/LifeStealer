using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoostPool : ObjectPool
{
    [SerializeField] private GameObject _template;

    private void OnEnable()
    {
        Init(_template);
    }

    public bool TryGetBoost(out GameObject boost)
    {
        if(TryGetObject(out boost))
        {
            boost.GetComponent<Boost>().BoostTaked += OnBoostTaked;
        }

        return boost != null;
    }

    private void OnBoostTaked(Boost boost)
    {
        boost.BoostTaked -= OnBoostTaked;
        boost.gameObject.SetActive(false);
    }
}