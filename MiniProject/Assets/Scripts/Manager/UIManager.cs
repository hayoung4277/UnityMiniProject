using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameStart gameStartUI;
    public GameProgress gameProgressUI;
    public GameOver gameOverUI;
    public GameClaer gameClearUI;

    public Player player;

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
        gameStartUI.Close();
        gameProgressUI.Open();
    }

    public void GameOver()
    {
        gameProgressUI.Close();
        gameOverUI.Open();

        gameOverUI.scoreText.text = $"{gameProgressUI.Score}";
        gameOverUI.timeText.text = $"{player.SurviveTime.ToString("F2")}";
    }

    public void GameClear()
    {
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
