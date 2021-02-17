using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private GameObject _menuPanel;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _pauseButton.onClick.AddListener(OnPauseBattonClick);
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
        _pauseButton.onClick.RemoveListener(OnPauseBattonClick);
    }

    private void OnPlayButtonClick()
    {
        Time.timeScale = 1;
        _menuPanel.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void OnPauseBattonClick()
    {
        _menuPanel.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void Reset()
    {
        _menuPanel.SetActive(false);
    }
}
