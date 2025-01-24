using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : GenericUI
{
    public TextMeshProUGUI scoreText;
    public GameObject gameOverText;
    public Button reStartButton;

    private GameProgress progress;

    private void Start()
    {
        Open();
        var findProgress = GameObject.FindWithTag("Progress");
        progress = findProgress.GetComponent<GameProgress>();
    }


    public override void Open()
    {
        progress.SendMessage("Close");
        base.Open();
        scoreText.text = $"Score: {Score}";
    }

    public override void Close()
    {
        base.Close();
    }

    public void Restart()
    {
        Debug.Log("Restart");
    }
}
