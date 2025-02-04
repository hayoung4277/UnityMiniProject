using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameStart gameStartUI;
    public GameProgress gameProgressUI;
    public GameOver gameOverUI;

    public Player player;

    private void Start()
    {
        // 초기 UI 상태 설정
        gameStartUI.Open();
        gameProgressUI.Close();
        gameOverUI.Close();

        // 시작 버튼 이벤트 연결
        gameStartUI.startButton.onClick.AddListener(StartGame);
        gameOverUI.reStartButton.onClick.AddListener(RestartGame);
    }

    private void StartGame()
    {
        gameStartUI.Close();
        gameProgressUI.Open();
    }

    public void GameOver()
    {
        gameProgressUI.Close();
        gameOverUI.Open();

        Debug.Log($"GameProgress Score: {gameProgressUI.Score}");  // 디버그 확인
        Debug.Log($"GameProgress Time: {gameProgressUI.CurrentTime}");

        gameOverUI.scoreText.text = $"Score: {gameProgressUI.Score}";
        gameOverUI.timeText.text = $"Time: {player.SurviveTime.ToString("F2")}";
    }

    private void RestartGame()
    {
        gameOverUI.Close();
        gameStartUI.Open();
    }

    public void AddScore(float amount)
    {
        gameProgressUI.AddScore(amount);
    }
}
