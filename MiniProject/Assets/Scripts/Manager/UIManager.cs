using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameStart gameStartUI;
    public GameProgress gameProgressUI;
    public GameOver gameOverUI;
    public GameClaer gameClearUI;

    private Player player;
    private GameManager gm;

    private void Awake()
    {
        var findGm = GameObject.FindWithTag(GMCT.GM);
        gm = findGm.GetComponent<GameManager>();
    }

    private void Start()
    {
        // 초기 UI 상태 설정
        gameStartUI.Open();
        gameProgressUI.Close();
        gameOverUI.Close();
        gameClearUI.Close();

        // 시작 버튼 이벤트 연결
        //gameStartUI.startButton.onClick.AddListener(StartGame);
        //gameOverUI.reStartButton.onClick.AddListener(RestartGame);
    }

    public void StartGame()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (player == null)
        {
            return;
        }

        gameStartUI.Close();
        gameProgressUI.Open();
        gameProgressUI.PlaySound();

    }

    public void GameOver()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        gameProgressUI.StopSound();
        gameProgressUI.Close();
        gameOverUI.Open();

        gameOverUI.scoreText.text = $"{gameProgressUI.Score}";
        gameOverUI.timeText.text = $"{player.SurviveTime.ToString("F2")}";
    }

    public void GameClear()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        gameProgressUI.Close();
        gameClearUI.Open();

        gameClearUI.scoreText.text = $"{gameProgressUI.Score}";
        gameClearUI.timeText.text = $"{player.SurviveTime.ToString("F2")}";
    }

    public void RestartGame()
    {
        gameOverUI.Close();
        gameClearUI.Close();
        gameStartUI.Open();
    }

    public void UnPauseGame()
    {
        gameProgressUI.UnPauseGame();
    }

    public void PauseGame()
    {
        gameProgressUI.PauseGame();
    }

    public void OpenPlayerSelect()
    {
        gameStartUI.OpenPlayerSelect();
    }

    public void SpawnPlayer1()
    {
        gameStartUI.SpawnPlayer1();
    }

    public void SpawnPlayer2()
    {
        gameStartUI.SpawnPlayer2();
    }

    public void SpawnPlayer3()
    {
        gameStartUI.SpawnPlayer3();
    }

    public void ClosePlayerSelect()
    {
        gameStartUI.ClosePlayerSelect();
    }

    public void AddScore(float amount)
    {
        gameProgressUI.AddScore(amount);
    }

    //public void ToggleFPS()
    //{
    //    if (gameProgressUI.fpsText.enabled == false)
    //    {
    //        gameProgressUI.fpsText.enabled = true;
    //    }
    //    else
    //    {
    //        gameProgressUI.fpsText.enabled = false;
    //    }
    //}
}
