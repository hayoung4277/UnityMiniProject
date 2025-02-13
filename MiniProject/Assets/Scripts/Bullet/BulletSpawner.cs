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
                Debug.LogError("BulletSpawner: Start()���� Player�� ã�� ���߽��ϴ�!");
            }
        }

        if (player != null)
        {
            // �÷��̾��� �ڽ� ������Ʈ �߿��� BulletPos�� ã��
            var bulletPos = player.transform.Find("BulletPos");
            if (bulletPos != null)
            {
                tf = bulletPos;
            }
            else
            {
                Debug.LogError("Player�� �ڽ� ������Ʈ 'BulletPos'�� ã�� �� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogError("Player ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    private void Update()
    {
        //if (player == null) return;

        // źȯ �߻� �ֱ� üũ
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

        // �÷��̾� ���� �� ��� �ݿ�
        playerCurrentFireTime = 0f; // �ʱ�ȭ�Ͽ� �ٷ� �߻� �����ϰ� ����
    }
}