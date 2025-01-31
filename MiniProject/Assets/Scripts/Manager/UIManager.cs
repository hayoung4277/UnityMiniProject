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
        // �ʱ� UI ���� ����
        gameStartUI.Open();
        gameProgressUI.Close();
        gameOverUI.Close();

        // ���� ��ư �̺�Ʈ ����
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

        Debug.Log($"GameProgress Score: {gameProgressUI.Score}");  // ����� Ȯ��
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
