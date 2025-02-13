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

    // BulletSpawner를 참조하는 변수 추가
    private BulletSpawner bulletSpawner;

    public GameObject[] playerPrefabs;

    private void Awake()
    {
        gm = GameObject.FindWithTag(GMCT.GM).GetComponent<GameManager>();

        // BulletSpawner 참조
        bulletSpawner = GameObject.FindWithTag("BulletSpawner").GetComponent<BulletSpawner>();
    }

    private void Start()
    {
        // 초기 UI 상태 설정
        gameStartUI.Open();
        gameProgressUI.Close();
        gameOverUI.Close();
        gameClearUI.Close();
    }

    public void StartGame()
    {
        if (player == null)
        {
            Debug.LogError("플레이어가 선택되지 않았습니다.");
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

    // 플레이어 교체 - 1, 2, 3 버튼 전부 통합
    public void SpawnPlayer(int playerIndex, Transform spawnPoint)
    {
        if (player != null)
        {
            Destroy(player.gameObject);
        }

        GameObject playerPrefab = playerPrefabs[playerIndex];
        if (playerPrefab == null)
        {
            Debug.LogError($"플레이어 프리팹이 없습니다! playerIndex: {playerIndex}");
            return;
        }

        // 플레이어 생성
        var newPlayerObj = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        player = newPlayerObj.GetComponent<Player>();

        if (player == null)
        {
            Debug.LogError("UIManager: 생성된 플레이어 오브젝트에서 Player 컴포넌트를 찾을 수 없습니다!");
            return;
        }

        // BulletSpawner 가져오기 (플레이어의 자식으로 존재)
        bulletSpawner = player.bulletSpawner;
        if (bulletSpawner == null)
        {
            Debug.LogError("BulletSpawner를 찾을 수 없습니다! 플레이어 프리팹에 BulletSpawner가 있는지 확인하세요.");
            return;
        }

        gm.SetPlayer(player);
        bulletSpawner.UpdatePlayerInfo(player); // 플레이어 정보 반영
    }

    private Player FindPlayer() => GameObject.FindWithTag("Player").GetComponent<Player>();

    public void AddScore(float amount) => gameProgressUI.AddScore(amount);

    public void OnPlayer1Selected()
    {
        SpawnPlayer(0, gameStartUI.playerSpawnPoint); // playerPrefabs[0]이 첫 번째 플레이어
    }

    public void OnPlayer2Selected()
    {
        SpawnPlayer(1, gameStartUI.playerSpawnPoint); // playerPrefabs[1]이 두 번째 플레이어
    }

    public void OnPlayer3Selected()
    {
        SpawnPlayer(2, gameStartUI.playerSpawnPoint); // playerPrefabs[2]이 세 번째 플레이어
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


