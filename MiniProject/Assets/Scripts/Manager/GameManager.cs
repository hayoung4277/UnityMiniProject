using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //private float speedUpTime = 0f;
    //private float speedUpInterval = 10f;
    private AudioSource audioSource;
    public Slider volumeSlider;

    public AudioClip buttonSound;

    public event System.Action onSpeedUp;
    public event System.Action onStopGame;

    private Player player;

    private void Awake()
    {
        Time.timeScale = 0f;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        if (volumeSlider != null)
        {
            volumeSlider.value = audioSource.volume; // 초기 볼륨 설정
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    private void Update()
    {
        //speedUpTime += Time.deltaTime;

        //if (speedUpTime >= speedUpInterval)
        //{
        //    onSpeedUp.Invoke();
        //    speedUpTime = 0f;
        //}
    }

    public void StopGame()
    {
        Time.timeScale = 0f;
        onStopGame.Invoke();
    }

    public void StartGame()
    {
        var findPlayer = GameObject.FindWithTag("Player");
        player = findPlayer.GetComponent<Player>();

        if (player == null)
            return;

        Time.timeScale = 1f;

    }

    public void SetPlayer(Player newPlayer)
    {
        player = newPlayer;
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void ButtonClickSound()
    {
        audioSource.PlayOneShot(buttonSound);
    }
}
