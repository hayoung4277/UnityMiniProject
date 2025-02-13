using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameProgress : GenericUI
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI fpsText;

    public TextMeshProUGUI rankText;
    public TextMeshProUGUI bonusScoreText;

    private AudioSource audioSource;

    private static Transform canvasTransform;

    private Transform child2;
    private Transform child4;

    private float fps;
    private Player player;

    private float currentTime;
    private float score;
    public float Score => score;
    public float CurrentTime => currentTime;
    public bool isPause;

    protected override void Awake()
    {
        var findPlayer = GameObject.FindWithTag("Player");
        player = findPlayer.GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();

        GameObject canvasObj = GameObject.FindWithTag("Progress");
        if (canvasObj != null)
        {
            canvasTransform = canvasObj.transform;
            child2 = canvasTransform.GetChild(1);
            child4 = canvasTransform.GetChild(3);

            // 처음에는 두 번째 자식(메뉴 UI 등)을 비활성화
            child2.gameObject.SetActive(false);
            child4.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Canvas를 찾을 수 없습니다!");
        }

        isPause = false;
    }

    private void Start()
    {
        if (!isPause)
        {
            currentTime += Time.deltaTime;
            timeText.text = $"Time: {CurrentTime.ToString("F2")}";
            fps = 1.0f / Time.deltaTime;
        }

        fpsText.enabled = false;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        timeText.text = $"Time: {CurrentTime.ToString("F2")}";

        fps = 1.0f / Time.deltaTime; // FPS 계산
        if (fpsText != null)
        {
            fpsText.text = $"FPS: {Mathf.Round(fps)}";
        }
    }

    public override void Open()
    {
        base.Open();

        scoreText.text = $"Points: {Score}";
        timeText.text = $"Distance: {player.SurviveTime.ToString("F2")}";
    }

    public override void Close()
    {
        base.Close();
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void AddScore(float amount)
    {
        score += amount;
        scoreText.text = $"Score: {score}";
    }

    public void PauseGame()
    {
        isPause = true;
        Time.timeScale = 0;
        child2.gameObject.SetActive(true);
    }

    public void UnPauseGame()
    {
        isPause = false;
        Time.timeScale = 1;
        child2.gameObject.SetActive(false);
    }

    public void DisAbleRankText()
    {
        child4.gameObject.SetActive(false);
    }

    public void OnRankText()
    {
        Boss boss = GameObject.FindWithTag("Boss").GetComponent<Boss>();

        child4.gameObject.SetActive(true);

        rankText.text = $"{boss.Rank} Rank";
        bonusScoreText.text = $"Points + {boss.OfferedScore}";
    }
}
