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

    public TextMeshProUGUI volumeValueText;

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
            volumeSlider.value = audioSource.volume; // �ʱ� ���� ����
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

        if (Input.GetKeyDown(KeyCode.Escape)) // �ȵ���̵��� �ڷ� ���� ��ư ����
        {
            QuitGame();
        }
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
        volumeValueText.text = $"{(int)(volume * 100)}%";
    }

    public void ButtonClickSound()
    {
        audioSource.PlayOneShot(buttonSound);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ����Ƽ �����Ϳ��� ���� ����
#else
        Application.Quit();
#endif
    }

    // �ܺο��� ������ �����ϴ� �Լ� (��: �ٸ� ��ũ��Ʈ�� UI ��ư ��)
    //public void SetAudioVolumeExternally(float volume)
    //{
    //    if (audioSource != null)
    //    {
    //        audioSource.volume = volume;
    //        // �����̴��� ���� ��� ���� ����ȭ
    //        if (volumeSlider != null)
    //        {
    //            volumeSlider.value = volume;
    //            volumeValueText.text = $"{volume}%";
    //        }
    //    }
    //}
}
