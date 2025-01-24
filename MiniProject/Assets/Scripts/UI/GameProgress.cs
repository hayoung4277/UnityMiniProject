using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameProgress : GenericUI
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    private float currentTime = 0f;

    private void Start()
    {
        Open();
        scoreText.text = $"Score: {Score}";
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddScore(100);
            scoreText.text = $"{Score}";
        }

        timeText.text = $"Time: {currentTime}";
    }

    public override void Open()
    {
        base.Open();
        scoreText.text = $"Score: {Score}";
        timeText.text = $"Time: {currentTime}";
    }

    public override void Close()
    {
        base.Close();
    }

    public void AddScore(int add)
    {
        Score += add;
    }
}
