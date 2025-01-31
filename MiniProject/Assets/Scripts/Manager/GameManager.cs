using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private float speedUpTime = 0f;
    private float speedUpInterval = 10f;

    public event System.Action onSpeedUp;
    public event System.Action onStopGame;

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    private void Update()
    {
        speedUpTime += Time.deltaTime;

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

    public void StartGame()
    {
        Time.timeScale = 1f;
    }

    public void ReStartGame()
    {

    }
}
