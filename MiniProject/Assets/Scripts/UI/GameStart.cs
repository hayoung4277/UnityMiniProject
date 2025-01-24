using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : GenericUI
{
    public Button startButton;
    private GameProgress progress;

    protected override void Awake()
    {
        var findProgress = GameObject.FindWithTag("Progress");
        progress = findProgress.GetComponent<GameProgress>();

        startButton.onClick.AddListener(Close);
    }

    private void Start()
    {
        Open();
    }

    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {
        base.Close();
        progress.enabled = true;
    }
}
