using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameStart gameStartUI;
    public GameProgress gameProgressUI;
    public GameOver gameOverUI;
    public GameClaer gameClearUI;

    private Player player;
    private GameManager gm;

    // BulletSpawner�� �����ϴ� ���� �߰�
    private BulletSpawner bulletSpawner;

    public GameObject[] playerPrefabs;

    private void Awake()
    {
        gm = GameObject.FindWithTag(GMCT.GM).GetComponent<GameManager>();

        // BulletSpawner ����
        bulletSpawner = GameObject.FindWithTag("BulletSpawner").GetComponent<BulletSpawner>();
    }

    private void Start()
    {
        // �ʱ� UI ���� ����
        gameStartUI.Open();
        gameProgressUI.Close();
        gameOverUI.Close();
        gameClearUI.Close();
    }

    public void StartGame()
    {
        if (player == null)
        {
            Debug.LogError("�÷��̾ ���õ��� �ʾҽ��ϴ�.");
            return;
        }

        gameStartUI.Close();
        gameProgressUI.Open();
        gameProgressUI.PlaySound();
    }

    public void GameOver()
    {
        player = FindPlayer();

        gameProgressUI.StopSound();
        gameProgressUI.Close();
        gameOverUI.Open();

        gameOverUI.scoreText.text = $"{gameProgressUI.Score}";
        gameOverUI.timeText.text = $"{player.SurviveTime.ToString("F2")}";

        var totalScore = gameProgressUI.Score + player.SurviveTime;

        gameOverUI.totalScoreText.text = $"{totalScore.ToString("F2")}";
    }

    public void GameClear()
    {
        player = FindPlayer();

        gameProgressUI.Close();
        gameClearUI.Open();

        gameClearUI.scoreText.text = $"{gameProgressUI.Score}";
        gameClearUI.timeText.text = $"{player.SurviveTime.ToString("F2")}";

        var totalScore = gameProgressUI.Score + player.SurviveTime;

        gameClearUI.totalScoreText.text = $"{totalScore.ToString("F2")}";
    }

    public void RestartGame()
    {
        gameOverUI.Close();
        gameClearUI.Close();
        gameStartUI.Open();
    }

    public void UnPauseGame() => gameProgressUI.UnPauseGame();
    public void PauseGame() => gameProgressUI.PauseGame();

    public void OpenPlayerSelect() => gameStartUI.OpenPlayerSelect();
    public void ClosePlayerSelect() => gameStartUI.ClosePlayerSelect();

    // �÷��̾� ��ü - 1, 2, 3 ��ư ���� ����
    public void SpawnPlayer(int playerIndex, Transform spawnPoint)
    {
        if (player != null)
        {
            Destroy(player.gameObject);
        }

        GameObject playerPrefab = playerPrefabs[playerIndex];
        if (playerPrefab == null)
        {
            Debug.LogError($"�÷��̾� �������� �����ϴ�! playerIndex: {playerIndex}");
            return;
        }

        // �÷��̾� ����
        var newPlayerObj = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        player = newPlayerObj.GetComponent<Player>();

        if (player == null)
        {
            Debug.LogError("UIManager: ������ �÷��̾� ������Ʈ���� Player ������Ʈ�� ã�� �� �����ϴ�!");
            return;
        }

        // BulletSpawner �������� (�÷��̾��� �ڽ����� ����)
        bulletSpawner = player.bulletSpawner;
        if (bulletSpawner == null)
        {
            Debug.LogError("BulletSpawner�� ã�� �� �����ϴ�! �÷��̾� �����տ� BulletSpawner�� �ִ��� Ȯ���ϼ���.");
            return;
        }

        gm.SetPlayer(player);
        bulletSpawner.UpdatePlayerInfo(player); // �÷��̾� ���� �ݿ�
    }

    private Player FindPlayer() => GameObject.FindWithTag("Player").GetComponent<Player>();

    public void AddScore(float amount) => gameProgressUI.AddScore(amount);

    public void OnPlayer1Selected()
    {
        SpawnPlayer(0, gameStartUI.playerSpawnPoint); // playerPrefabs[0]�� ù ��° �÷��̾�
    }

    public void OnPlayer2Selected()
    {
        SpawnPlayer(1, gameStartUI.playerSpawnPoint); // playerPrefabs[1]�� �� ��° �÷��̾�
    }

    public void OnPlayer3Selected()
    {
        SpawnPlayer(2, gameStartUI.playerSpawnPoint); // playerPrefabs[2]�� �� ��° �÷��̾�
    }
    public void ToggleFPS()
    {
        if (gameProgressUI.fpsText.enabled == false)
        {
            gameProgressUI.fpsText.enabled = true;
        }
        else
        {
            gameProgressUI.fpsText.enabled = false;
        }
    }
}


