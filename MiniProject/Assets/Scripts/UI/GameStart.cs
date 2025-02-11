using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : GenericUI
{
    public Button startButton;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void Open()
    {
        base.Open();
        audioSource.Play();
    }

    public override void Close()
    {
        base.Close();
        audioSource.Stop();
    }
}
