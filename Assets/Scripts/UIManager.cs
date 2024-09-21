using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private GameManager gameManager; 
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject btnStart;
    [SerializeField] private GameObject btnStop;
    [SerializeField] private GameObject btnNewGame;
    [SerializeField] private GameObject btnReturnToMainMenu;
    [SerializeField] private GameObject imgGameOver;
    [SerializeField] private GameObject imgWinGame; 
    public static UIManager Instance { get; private set; }
    private float _scoreValue = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void updateScoreForward()
    {
        _scoreValue++;
        scoreTxt.text = _scoreValue.ToString();
    }
    public void updateScoreBackwards()
    {
        _scoreValue--;
        scoreTxt.text = _scoreValue.ToString();
    }

    public void OnStopButtonPressed()
    {
        if (gameManager != null)
        {
            gameManager.EndGame();
        }
    }

    public void ShowGameOver()
    {
        panelGameOver.SetActive(true);
        imgGameOver.SetActive(true);
        imgWinGame.SetActive(false);

    }

    public void ShowMainMenu()
    {
        panelGameOver.SetActive(false);
        panelMainMenu.SetActive(true);
        btnStart.SetActive(true);
        btnStop.SetActive(true);
    }
    public void RestartScore()
    {
        _scoreValue = 0;
        scoreTxt.text = _scoreValue.ToString();
    }

    public void HandleGameWon()
    {
        ShowGameOver();
        imgGameOver.SetActive(false);
        imgWinGame.SetActive(true);
    }
    
}
