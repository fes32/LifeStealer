using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoostSpawner : MonoBehaviour
{
    [SerializeField] private BoostPool[] _boostPools;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _minTimeSpawn;
    [SerializeField] private float _maxTimeSpawn;

    private float _elapsedTimeAfterLastSpawn = 0;
    private float _randomSpawnTime;

    public event UnityAction<string> BoostTakedHandler;

    private void OnEnable()
    {
        _randomSpawnTime = Random.Range(_minTimeSpawn, _maxTimeSpawn);

        StartCoroutine(SpawnBoost());
    }

    private IEnumerator SpawnBoost()
    {
        while (_elapsedTimeAfterLastSpawn < _randomSpawnTime)
        {
            _elapsedTimeAfterLastSpawn += Time.deltaTime;
            yield return null;
        }
        Spawn();
    }

    private void Spawn()
    {
        _randomSpawnTime = Random.Range(_minTimeSpawn, _maxTimeSpawn);
        _elapsedTimeAfterLastSpawn = 0;
     
        if(TryGetRandomBoost(out Boost boost))
        {
            var spawnPoint = GetRandomSpawnPosition();
            boost.transform.position = spawnPoint.position;
            boost.BoostTaked += BoostTaked;
        }
    }

    private bool TryGetRandomBoost(out Boost randomBoost)
    {

        int randomBoostIndex = Random.Range(0, _boostPools.Length);

        _boostPools[randomBoostIndex].TryGetBoost(out GameObject boost);

        randomBoost = boost.GetComponent<Boost>();

        return randomBoost != null;
    }

    private Transform GetRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, _spawnPoints.Length);

        Transform spawnPoint = _spawnPoints[randomIndex];                
       
        return spawnPoint;
    }

    private void BoostTaked(Boost boost)
    {
        BoostTakedHandler?.Invoke(boost.Description);
        boost.BoostTaked -= BoostTaked;

        StopCoroutine(SpawnBoost());
        StartCoroutine(SpawnBoost());
    }


    public void Reset()
    {
        foreach(var pool in _boostPools)
        {
            pool.Reset();
        }

        this.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
    }
}