using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonsterSpawner : MonoBehaviour
{

    public GameObject[] monsterPrefabs; // 몬스터 프리팹 배열
    public Vector3 spawnPos;
    public int spawnSixCount = 6; // 소환할 몬스터 개수
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

        // 몬스터 프리팹 중 랜덤으로 6개 선택
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
        // 몬스터 프리팹 중 랜덤으로 6개 선택
        List<GameObject> selectedMonsters = GetRandomMonsters(monsterPrefabs, spawnEightCount);

        // 소환 위치 설정 (스폰 포인트에서 랜덤 선택)
        foreach (var monster in selectedMonsters)
        {
            //int randomIndex = Random.Range(0, spawnPoints.Length);
            //Transform spawnPoint = spawnPoints[randomIndex];

            // 몬스터 소환
            Instantiate(monster, spawnPos, Quaternion.identity);
            spawnPos.x += 0.8f;
        }
    }

    private List<GameObject> GetRandomMonsters(GameObject[] sourceArray, int count)
    {
        List<GameObject> sourceList = new List<GameObject>(sourceArray);
        List<GameObject> selected = new List<GameObject>();

        // 랜덤하게 선택
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, sourceList.Count);
            selected.Add(sourceList[randomIndex]);
            //sourceList.RemoveAt(randomIndex); // 중복 방지
        }

        return selected;
    }
}
