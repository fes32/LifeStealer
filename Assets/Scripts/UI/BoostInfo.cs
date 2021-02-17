using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoostInfo : MonoBehaviour
{
    [SerializeField] private BoostSpawner _boostSpawner;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _showTime;

    private void OnEnable()
    {
        _boostSpawner.BoostTakedHandler += OnBoostTaked;
    }

    private void OnDisable()
    {
        _boostSpawner.BoostTakedHandler -= OnBoostTaked;
    }

    private void OnBoostTaked(string description)
    {
        _text.text = description;
        StartCoroutine(ShowBoostDescription());
    }

    private IEnumerator ShowBoostDescription()
    {
        float elapsedTime = 0;

        while (elapsedTime < _showTime)
        {
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }
        _text.text = " ";
    }
}