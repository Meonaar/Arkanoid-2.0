using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text targetText;
    public Text livesText;

    public int score { get; set; }

    private void Awake()
    {
        Brick.OnBrickDestruction += OnBrickDestruction;
        BrickManager.OnLevelLoaded += OnLevelLoaded;
        GameManager.OnLivesLost += OnLiveLost;
    }
    private void Start()
    {
        OnLiveLost(GameManager.Instance.actualLives);
    }

    private void OnLiveLost(int remainingLives)
    {
        livesText.text = $"Lives: {remainingLives}";
    }

    private void OnLevelLoaded()
    {
        UpdateRemainingBrickText();
        UpdateScoreText(0);
    }

    private void UpdateScoreText(int i)
    {
        score += i;
        string scoreString = score.ToString().PadLeft(5, '0');
        scoreText.text = $"SCORE: {Environment.NewLine}{scoreString}";
    }

    private void OnBrickDestruction(Brick obj)
    {
        UpdateRemainingBrickText();
        UpdateScoreText(10);
    }

    private void UpdateRemainingBrickText()
    {
        targetText.text = $"TARGET:{Environment.NewLine}{BrickManager.Instance.RemainingBricks.Count} / {BrickManager.Instance.InitialBrickCount}";
    }

    private void OnDisable()
    {
        Brick.OnBrickDestruction -= OnBrickDestruction;
        BrickManager.OnLevelLoaded -= OnLevelLoaded;
    }
}
