using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject normalDrop;
    public GameObject legendaryDrop;

    private void OnEnable()
    {
        EnemySpawner.OnEnemySpawned += RegisterEnemy;
    }

    private void OnDisable()
    {
        EnemySpawner.OnEnemySpawned -= RegisterEnemy;
    }

    private void RegisterEnemy(Enemy enemy)
    {
        enemy.OnSpawnItem += SpawnItem;
    }

    private void SpawnItem(Enemy enemy)
    {
        Vector3 spawnPosition = enemy.transform.position;

        // 아이템 드롭 확률 적용
        if (Random.value > 0.9f) // 10% 확률로 전설 아이템
        {
            Instantiate(legendaryDrop, spawnPosition, Quaternion.identity);
        }
        else // 90% 확률로 일반 아이템
        {
            Instantiate(normalDrop, spawnPosition, Quaternion.identity);
        }
    }
}
