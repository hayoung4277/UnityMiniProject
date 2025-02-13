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
    public Slider volumeSlider;
    public TextMeshProUGUI volumeValueText;

    private static Transform canvasTransform;

    private Transform child2;
    private Transform child4;
    private Transform child5;

    private float fps;
    private Player player;

    private float currentTime;
    private float score;
    public float Score => score;
    public float CurrentTime => currentTime;
    public bool isPause;

    public AudioClip waringClip;

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
            child5 = canvasTransform.GetChild(4);

            // 처음에는 두 번째 자식(메뉴 UI 등)을 비활성화
            child2.gameObject.SetActive(false);
            child4.gameObject.SetActive(false);
            child5.gameObject.SetActive(false);
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
            timeText.text = $"Distance: {CurrentTime.ToString("F2")}";
            fps = 1.0f / Time.deltaTime;
        }

        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 1f;

        if (volumeSlider != null)
        {
            volumeSlider.value = audioSource.volume; // 초기 볼륨 설정
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        fpsText.enabled = true;
    }

    private void Update()
    {
        currentTime += Time.deltaTime * 100f;

        timeText.text = $"Distance: {CurrentTime.ToString("F2")}";

        fps = 1.0f / Time.deltaTime; // FPS 계산
        if (fpsText != null)
        {
            //fpsText.text = $"FPS: {Mathf.Round(fps)}";
            fpsText.text = $"FPS: {fps.ToString("F2")}";
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

    public void OnWarningPopUp()
    {
        audioSource.PlayOneShot(waringClip);

        child5.gameObject.SetActive(true);

        // 깜빡임 코루틴 호출
        StartCoroutine(FlashUI());
    }

    private IEnumerator FlashUI()
    {
        float duration = 2f; // 깜빡임 시간 (2초)
        float elapsedTime = 0f;

        // 2초 동안 반복하면서 깜빡이게 설정
        while (elapsedTime < duration)
        {
            child5.gameObject.SetActive(!child5.gameObject.activeSelf); // 활성화/비활성화 토글
            elapsedTime += 0.5f; // 깜빡임 주기 (0.5초 간격)
            yield return new WaitForSeconds(0.5f); // 0.5초 기다림
        }

        // 최종적으로 UI를 다시 활성화 상태로 설정
        child5.gameObject.SetActive(true);
    }

    public void DisAbleWarningPopUp()
    {
        child5.gameObject.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        volumeValueText.text = $"{(int)(volume * 100)}%";
    }
}
