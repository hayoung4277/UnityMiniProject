using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOver : GenericUI
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI totalScoreText;
    public Button reStartButton;

    private AudioSource audioSource;
    public AudioClip gameOverSound;

    protected override void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void Open()
    {
        base.Open();

        audioSource.PlayOneShot(gameOverSound);
    }

    public override void Close()
    {
        base.Close();
        if(audioSource != null)
        {
            audioSource.Play();
        }
    }
}
