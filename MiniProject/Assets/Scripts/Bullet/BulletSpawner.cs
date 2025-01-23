using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public Transform parent;

    public float fireRate = 0.8f;
    private float currentFireTime;

    private void Awake()
    {
        currentFireTime = 0f;
    }

    private void Update()
    {
        currentFireTime += Time.deltaTime;

        if (currentFireTime > fireRate)
        {
            currentFireTime = 0f;

            SpawnBullet();
        }
    }

    private void SpawnBullet()
    {
        var bulletprefab = Resources.Load<GameObject>("Prefabs/Bullet/PlayerBullet");
        if (bulletprefab == null)
        {
            Debug.LogError("Bullet Prefab not Found");
            return;
        }

        GameObject bullet = Instantiate(bulletprefab, parent.position, parent.rotation);
    }
}
