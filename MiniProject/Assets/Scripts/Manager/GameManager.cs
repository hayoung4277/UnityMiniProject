using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    private float currentTime = 0f;
    public float Score { get; set; }

    private float speedUpTime = 0f;
    private float speedUpInterval = 10f;

    public event System.Action onSpeedUp;
    public event System.Action onStopGame;

    private void Awake()
    {
        Score = 0f;

        scoreText.text = $"Score: {Score:F2}";
        timeText.text = $"Time: {currentTime}";

        Time.timeScale = 1f;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        speedUpTime += Time.deltaTime;

        scoreText.text = $"Score: {Score}";
        timeText.text = $"Time: {currentTime}";


        if (speedUpTime >= speedUpInterval)
        {
            onSpeedUp.Invoke();
            speedUpTime = 0f;
        }
    }

    public void StopGame()
    {
        Time.timeScale = 0f ;
        onStopGame.Invoke();
    }

    public void AddScore(float amount)
    {
        Score += amount;
    }
}
