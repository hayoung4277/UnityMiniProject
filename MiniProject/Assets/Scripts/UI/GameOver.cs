using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : GenericUI
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public GameObject gameOverText;
    public Button reStartButton;

    //private GameProgress progress;

    //private void Start()
    //{
    //    Open();
    //    var findProgress = GameObject.FindWithTag("Progress");
    //    progress = findProgress.GetComponent<GameProgress>();
    //}

    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }
}
