using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameProgress : GenericUI
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    //public TextMeshProUGUI fpsText;

    private float fps;
    private Player player;

    private float currentTime;
    private float score;
    public float Score => score;
    public float CurrentTime => currentTime;

    private float deltaTime = 0.0f;

    protected override void Awake()
    {
        var findPlayer = GameObject.FindWithTag("Player");
        player = findPlayer.GetComponent<Player>();
    }

    private void Start()
    {
        score = 0;
        currentTime = 0f;
        scoreText.text = $"Score: {Score}";

        //fpsText.enabled = false;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        timeText.text = $"Time: {CurrentTime.ToString("F2")}";

        //deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        //fps = 1.0f / deltaTime;

        fps = 1.0f / Time.deltaTime;

        //if (fpsText.enabled)
        //{
        //    fpsText.text = $"FPS: {fps.ToString("F2")}";
        //}
    }

    public override void Open()
    {
        base.Open();

        scoreText.text = $"Points: {Score}";
        //timeText.text = $"Time: {CurrentTime.ToString("F2")}";
        timeText.text = $"Distance: {player.SurviveTime.ToString("F2")}";
    }

    public override void Close()
    {
        base.Close();
    }

    public void AddScore(float amount)
    {
        score += amount;
        scoreText.text = $"Score: {score}";
    }

    //public void ShowFPS()
    //{
    //    if (fpsText.enabled == false)
    //    {
    //        fpsText.enabled = true;
    //    }
    //    else
    //    {
    //        fpsText.enabled = false;
    //    }
    //}
}
