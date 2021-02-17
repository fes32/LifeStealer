using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _time;
    [SerializeField] private Game _game;
    [SerializeField] private GameObject _gameOverScreen;

    private void OnEnable()
    {
        _player.Dying += OnPlayerDie;
        _resetButton.onClick.AddListener(OnResetButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _player.Dying -= OnPlayerDie;
        _resetButton.onClick.RemoveListener(OnResetButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    private void OnPlayerDie(float time, int score)
    {
        Time.timeScale = 0;
        _gameOverPanel.SetActive(true);
        _healthBar.gameObject.SetActive(false);
        _score.text = score.ToString();
        _time.text = time.ToString();
    }

    private void OnResetButtonClick()
    {
        _game.Reset();
        Time.timeScale = 1;
        _healthBar.gameObject.SetActive(true);
        _gameOverScreen.SetActive(false);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void Reset()
    {
        _gameOverScreen.SetActive(false);
    }
}