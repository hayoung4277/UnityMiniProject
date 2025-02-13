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
        EnemySpawner.OnBossSpawned += RegisterBoss;
    }

    private void OnDisable()
    {
        EnemySpawner.OnEnemySpawned -= RegisterEnemy;
        EnemySpawner.OnBossSpawned -= RegisterBoss;
    }

    private void RegisterEnemy(Enemy enemy)
    {
        enemy.OnSpawnItem += SpawnItem;
    }

    private void RegisterBoss(Boss boss)
    {
        boss.OnSpawnCommonItem += SpawnCommonItem;
        boss.OnSpawnLegendaryItem += SpawnLegendaryItem;
    }

    private void SpawnItem(Enemy enemy)
    {
        Vector3 spawnPosition = enemy.transform.position;

        GameObject itemInstance;
        ItemSupplyDrop itemSupplyDrop;

        var randomValue = Random.Range(0f, 1f);

        if (randomValue > 0.9f) // 10% 확률로 전설 아이템
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

    private void SpawnCommonItem(Boss boss)
    {
        Vector3 spawnPosition = boss.transform.position;

        GameObject itemInstance;
        ItemSupplyDrop itemSupplyDrop;

        itemInstance = Instantiate(commonDrop, spawnPosition, Quaternion.identity);
        itemSupplyDrop = itemInstance.GetComponent<ItemSupplyDrop>();
        if(itemSupplyDrop != null)
        {
            itemSupplyDrop.Type = ItemType.Common;
        }
    }

    private void SpawnLegendaryItem(Boss boss)
    {
        Vector3 spawnPosition = boss.transform.position;

        GameObject itemInstance;
        ItemSupplyDrop itemSupplyDrop;

        itemInstance = Instantiate(legendaryDrop, spawnPosition, Quaternion.identity);
        itemSupplyDrop = itemInstance.GetComponent<ItemSupplyDrop>();
        if (itemSupplyDrop != null)
        {
            itemSupplyDrop.Type = ItemType.Legendary;
        }
    }
}
