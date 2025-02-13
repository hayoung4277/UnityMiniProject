using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Header("Player Bullet")]
    private float playerFireRate;
    private float playerCurrentFireTime;
    private string bulletName;

    private Transform tf;
    private Player player;

    private void Awake()
    {
        playerCurrentFireTime = 0f;
    }

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player")?.GetComponent<Player>();

            if (player != null)
            {
                playerFireRate = player.FireRate;
                bulletName = player.BulletName;
            }
            else
            {
                Debug.LogError("BulletSpawner: Start()에서 Player를 찾지 못했습니다!");
            }
        }

        if (player != null)
        {
            // 플레이어의 자식 오브젝트 중에서 BulletPos를 찾기
            var bulletPos = player.transform.Find("BulletPos");
            if (bulletPos != null)
            {
                tf = bulletPos;
            }
            else
            {
                Debug.LogError("Player의 자식 오브젝트 'BulletPos'를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogError("Player 오브젝트를 찾을 수 없습니다.");
        }
    }

    private void Update()
    {
        //if (player == null) return;

        // 탄환 발사 주기 체크
        playerCurrentFireTime += Time.deltaTime;

        if (playerCurrentFireTime >= playerFireRate)
        {
            playerCurrentFireTime = 0f;
            SpawnPlayerBullet();
        }
    }

    private void SpawnPlayerBullet()
    {
        if (player == null)
        {
            return;
        }

        if (tf == null)
        {
            return;
        }

        var bulletPrefab = Resources.Load<GameObject>($"Prefabs/Bullet/{bulletName}");
        if (bulletPrefab == null)
        {
            return;
        }

        Instantiate(bulletPrefab, tf.position, tf.rotation);
    }

    public void UpdatePlayerInfo(Player newPlayer)
    {
        if (newPlayer == null)
        {
            Debug.LogError("New player is null. Cannot update player info.");
            return;
        }

        player = newPlayer;
        playerFireRate = player.FireRate;
        bulletName = player.BulletName;

        // 플레이어 변경 시 즉시 반영
        playerCurrentFireTime = 0f; // 초기화하여 바로 발사 가능하게 만듦
    }
}