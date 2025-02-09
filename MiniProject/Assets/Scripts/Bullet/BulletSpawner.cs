using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Header("Player Bullet")]
    //public Transform playerTransform;
    private float playerFireRate;
    private float playerCurrentFireTime;

    private Transform tf;
    private Player player;

    private void Awake()
    {
        var findPlayer = GameObject.FindWithTag("Player");
        player = findPlayer.GetComponent<Player>();

        var findPos = GameObject.FindWithTag("BulletPos");
        tf = findPos.transform;

        playerCurrentFireTime = 0f;
    }

    private void Start()
    {
        playerFireRate = player.FireRate;
    }

    private void Update()
    {
        //플레이어의 탄환 생성
        playerCurrentFireTime += Time.deltaTime;

        if (playerCurrentFireTime >= playerFireRate)
        {
            playerCurrentFireTime = 0f;

            SpawnPlayerBullet();
        }
    }

    private void SpawnPlayerBullet()
    {
        var bulletprefab = Resources.Load<GameObject>($"Prefabs/Bullet/{player.BulletName}");
        if (bulletprefab == null)
        {
            Debug.LogError("Bullet Prefab not Found");
            return;
        }

        GameObject bullet = Instantiate(bulletprefab, tf.position, tf.rotation);
    }
}
