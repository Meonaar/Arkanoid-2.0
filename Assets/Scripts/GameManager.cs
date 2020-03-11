using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public GameObject gameOverScreen;
    public GameObject VictoryScreen;

    public static event Action<int> OnLivesLost;

    public int actualLives = 3;
    public int Lives { get; set; }
    public bool IsGameStarted { get; set; }

    private void Start()
    {
        Lives = actualLives;
        Screen.SetResolution(1080, 1920, false);
        Ball.OnBallDeath += OnBallDeath;
        Brick.OnBrickDestruction += OnBrickDestruction;
    }

    private void OnBrickDestruction(Brick obj)
    {
        if (BrickManager.Instance.RemainingBricks.Count <= 0)
        {
            BallManager.Instance.ResetBalls();
            GameManager.Instance.IsGameStarted = false;
            BrickManager.Instance.LoadNextLevel();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnBallDeath(Ball obj)
    {
        if (BallManager.Instance.Balls.Count <= 0)
        {
            Lives--;

            if (Lives < 1)
            {
                gameOverScreen.SetActive(true);
            }
            else
            {
                OnLivesLost?.Invoke(Lives);
                BallManager.Instance.ResetBalls();
                IsGameStarted = false;
                BrickManager.Instance.LoadLevel(BrickManager.Instance.currentLevel);
            }
        }
    }

    public void ShowVictoryScreen()
    {
        VictoryScreen.SetActive(true);
    }

    private void OnDisable()
    {
        Ball.OnBallDeath -= OnBallDeath;    
    }
}
