using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject commonDrop;
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

        GameObject itemInstance;
        ItemSupplyDrop itemSupplyDrop;

        if (Random.value > 0.9f) // 10% 확률로 전설 아이템
        {
            itemInstance = Instantiate(legendaryDrop, spawnPosition, Quaternion.identity);
            itemSupplyDrop = itemInstance.GetComponent<ItemSupplyDrop>();
            if (itemSupplyDrop != null)
            {
                itemSupplyDrop.Type = ItemType.Legendary;
            }
        }
        else // 90% 확률로 일반 아이템
        {
            itemInstance = Instantiate(commonDrop, spawnPosition, Quaternion.identity);
            itemSupplyDrop = itemInstance.GetComponent<ItemSupplyDrop>();
            if (itemSupplyDrop != null)
            {
                itemSupplyDrop.Type = ItemType.Common;
            }
        }
    }
}
