using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private BoostSpawner _boostSpawner;
    [SerializeField] private ObjectPool[] _spawners;
    [SerializeField] private Menu _menuPanel;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private GameOverScreen _gameOverScreen;

    public void Reset()
    {

        _player.Reset();
        _boostSpawner.Reset();
        _menuPanel.Reset();
        _healthBar.Reset();
        _gameOverScreen.Reset();

        foreach(var item in _spawners)
        {
            item.Reset();
        }

        Time.timeScale = 1;
    }
}
