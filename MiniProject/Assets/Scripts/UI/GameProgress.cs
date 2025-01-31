using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameProgress : GenericUI
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    public Player player;

    private float currentTime;
    private static float totalScore = 0;
    public float Score
    {
        get => totalScore;
        private set => totalScore = value;
    }
    public float CurrentTime => currentTime;

    private void Start()
    {
        Score = 0;
        currentTime = 0f;
        scoreText.text = $"Score: {Score}";
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        timeText.text = $"Time: {CurrentTime}";
    }

    public override void Open()
    {
        base.Open();

        scoreText.text = $"Score: {Score}";
        //timeText.text = $"Time: {CurrentTime.ToString("F2")}";
        timeText.text = $"Time: {player.SurviveTime.ToString("F2")}";
    }

    public override void Close()
    {
        base.Close();
    }

    public void AddScore(float amount)
    {
        Score += amount;
    }
}
