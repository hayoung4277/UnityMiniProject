using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameClaer : GenericUI
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI totalScoreText;
    public Button reStartButton;

    private AudioSource audioSource;
    public AudioClip clearSound;

    protected override void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void Open()
    {
        base.Open();
        audioSource.PlayOneShot(clearSound);
        //audioSource.Play();
    }

    public override void Close()
    {
        base.Close();
        //audioSource.Stop();
    }
}
