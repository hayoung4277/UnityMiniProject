using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonsterSpawner : MonoBehaviour
{

    public GameObject[] monsterPrefabs; // ���� ������ �迭
    public Vector3 spawnPos;
    public int spawnSixCount = 6; // ��ȯ�� ���� ����
    public int spawnEightCount = 8;

    public float spawnInterval = 2f;
    public float spawnTime = 0f;

    private void Update()
    {
        spawnTime += Time.deltaTime;

        if (spawnTime >= spawnInterval)
        {
            SpawnSixRandomMonsters();
            spawnTime = 0f;
        }
    }

    private void SpawnSixRandomMonsters()
    {
        Vector3 currentSpawnPos = spawnPos;
        currentSpawnPos.z = 0f;

        // ���� ������ �� �������� 6�� ����
        List<GameObject> selectedMonsters = GetRandomMonsters(monsterPrefabs, spawnSixCount);

        for(int i  = 0; i < spawnSixCount; i++)
        {
            var monster = selectedMonsters[i];
            
            monster.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            Instantiate(monster, currentSpawnPos, Quaternion.identity);
            currentSpawnPos.x += 0.8f;
        }
    }

    private void SpawnEigthRandomMonster()
    {
        // ���� ������ �� �������� 6�� ����
        List<GameObject> selectedMonsters = GetRandomMonsters(monsterPrefabs, spawnEightCount);

        // ��ȯ ��ġ ���� (���� ����Ʈ���� ���� ����)
        foreach (var monster in selectedMonsters)
        {
            //int randomIndex = Random.Range(0, spawnPoints.Length);
            //Transform spawnPoint = spawnPoints[randomIndex];

            // ���� ��ȯ
            Instantiate(monster, spawnPos, Quaternion.identity);
            spawnPos.x += 0.8f;
        }
    }

    private List<GameObject> GetRandomMonsters(GameObject[] sourceArray, int count)
    {
        List<GameObject> sourceList = new List<GameObject>(sourceArray);
        List<GameObject> selected = new List<GameObject>();

        // �����ϰ� ����
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, sourceList.Count);
            selected.Add(sourceList[randomIndex]);
            //sourceList.RemoveAt(randomIndex); // �ߺ� ����
        }

        return selected;
    }
}
