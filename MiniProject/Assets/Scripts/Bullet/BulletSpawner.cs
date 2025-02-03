using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Header("Player Bullet")]
    public Transform playerTransform;
    public float playerFireRate = 0.8f;
    private float playerCurrentFireTime;

    private void Awake()
    {
        playerCurrentFireTime = 0f;
    }

    private void Update()
    {
        //플레이어의 탄환 생성
        playerCurrentFireTime += Time.deltaTime;

        if (playerCurrentFireTime > playerFireRate)
        {
            playerCurrentFireTime = 0f;

            SpawnPlayerBullet();
        }
    }

    private void SpawnPlayerBullet()
    {
        var bulletprefab = Resources.Load<GameObject>("Prefabs/Bullet/PlayerBullet");
        if (bulletprefab == null)
        {
            Debug.LogError("Bullet Prefab not Found");
            return;
        }

        GameObject bullet = Instantiate(bulletprefab, playerTransform.position, playerTransform.rotation);
    }
}
