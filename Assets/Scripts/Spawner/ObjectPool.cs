using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    private List<GameObject> _pool = new List<GameObject>();

    protected void Init(GameObject template)
    {
        for(int i=0; i<_capacity; i++)
        {
            var spawned = Instantiate(template, _container.transform);
            spawned.SetActive(false);
            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        if (result != null)
            result.SetActive(true);

        return result != null;
    }

    public void Reset()
    {
        foreach(var item in _pool)
        {
            item.SetActive(false);
        }
    }
}
