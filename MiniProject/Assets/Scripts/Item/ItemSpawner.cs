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

        // ������ ��� Ȯ�� ����
        if (Random.value > 0.9f) // 10% Ȯ���� ���� ������
        {
            Instantiate(legendaryDrop, spawnPosition, Quaternion.identity);
        }
        else // 90% Ȯ���� �Ϲ� ������
        {
            Instantiate(normalDrop, spawnPosition, Quaternion.identity);
        }

        //int availableIndex = GetAvailableIndex();
        //if (availableIndex == -1) return;

        //GameObject prefabInstance = Instantiate(prefab, spawnPos[availableIndex].position, spawnPos[availableIndex].rotation);
        //Minion minion = prefabInstance.GetComponent<Minion>();
        //minion.SpawnIndex = availableIndex;
        //minion.OnMinionDestroyed += HandleMinionDestroyed;

        //usedIndices.Add(availableIndex);
        //activeMinions[availableIndex] = minion;
    }
}
