using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    public GameObject[] singleShotPrefabs;
    public GameObject[] fanShotPrefabs;
    public GameObject[] shieldPrefabs;
    public GameObject[] multiplePrefabs;
    public GameObject[] singleShieldPrefabs;
    public GameObject[] boomPrefabs;
    public GameObject[] razerPrefabs;
    public GameObject[] FormationPrefabs;
    //public GameObject[] homingPrefabs;
    //public GameObject[] piercePrefabs;
    //public GameObject[] agroPrefabs;

    public GameObject spawnEffect;
    private GameObject effectInstance;

    private int prefabCount = 10;

    private List<Transform> spawnPositions = new List<Transform>();

    private List<int> usedIndices = new List<int>(); // 사용된 위치 저장
    private Dictionary<int, Minion> activeMinions = new Dictionary<int, Minion>(); // 인덱스별 Minion 저장

    private Queue<int> minionQueue = new Queue<int>(); // FIFO 순서 저장

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            foreach (Transform child in player.transform)
            {
                if (child.CompareTag("SpawnPos")) // "SpawnPos" 태그가 있는 자식만 추가
                {
                    spawnPositions.Add(child);
                }
            }

            Debug.Log($"SpawnPos 개수: {spawnPositions.Count}");
        }
    }

    public void SpawnMinion(ItemType itemType)
    {
        int randomValue = Random.Range(1, prefabCount);

        if (itemType == ItemType.Common)
        {
            switch (randomValue)
            {
                case 1: SpawnCommonMinion(singleShotPrefabs); break;
                case 2: SpawnCommonMinion(fanShotPrefabs); break;
                case 3: SpawnCommonMinion(shieldPrefabs); break;
                case 4: SpawnCommonMinion(multiplePrefabs); break;
                case 5: SpawnCommonMinion(singleShieldPrefabs); break;
                case 6: SpawnCommonMinion(boomPrefabs); break;
                case 7: SpawnCommonMinion(razerPrefabs); break;
                case 8: SpawnCommonMinion(FormationPrefabs); break;
                //case 9: SpawnCommonMinion(homingPrefabs); break;
                //case 9: SpawnCommonMinion(piercePrefabs); break;
                //case 10: SpawnCommonMinion(agroPrefabs); break;
                default: Debug.LogWarning("알 수 없는 Common 프리팹"); break;
            }
        }

        if (itemType == ItemType.Legendary)
        {
            switch (randomValue)
            {
                case 1: SpawnLegendMinion(singleShotPrefabs); break;
                case 2: SpawnLegendMinion(fanShotPrefabs); break;
                case 3: SpawnLegendMinion(shieldPrefabs); break;
                case 4: SpawnLegendMinion(multiplePrefabs); break;
                case 5: SpawnLegendMinion(singleShieldPrefabs); break;
                case 6: SpawnLegendMinion(boomPrefabs); break;
                case 7: SpawnLegendMinion(razerPrefabs); break;
                case 8: SpawnLegendMinion(FormationPrefabs); break;
                //case 9: SpawnLegendMinion(homingPrefabs); break;
                //case 9: SpawnLegendMinion(piercePrefabs); break;
                //case 10: SpawnLegendMinion(agroPrefabs); break;
                default: Debug.LogWarning("알 수 없는 Legendary 프리팹"); break;
            }
        }
    }

    private int GetAvailableIndex()
    {
        // 사용되지 않은 자리 찾기
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            if (!usedIndices.Contains(i))
            {
                return i; // 빈자리 반환
            }
        }

        return -1; // 모든 위치가 사용 중
    }

    private void HandleMinionDestroyed(Minion minion)
    {
        if (usedIndices.Contains(minion.SpawnIndex))
        {
            usedIndices.Remove(minion.SpawnIndex);
            activeMinions.Remove(minion.SpawnIndex);
            minionQueue = new Queue<int>(minionQueue.Where(index => index != minion.SpawnIndex)); // 큐에서도 제거
        }
        Debug.Log($"Minion Destroyed! Index {minion.SpawnIndex} is now free.");
    }

    private void SpawnCommonMinion(GameObject[] prefab)
    {
        int availableIndex = GetAvailableIndex();
        int randomPrefabIndex = Random.Range(0, prefab.Length);

        if (availableIndex == -1)
        {
            // 빈자리가 없으면 FIFO 방식으로 가장 오래된 미니언 제거
            int oldestIndex = minionQueue.Dequeue();
            if (activeMinions.ContainsKey(oldestIndex))
            {
                Destroy(activeMinions[oldestIndex].gameObject);
                activeMinions.Remove(oldestIndex);
                usedIndices.Remove(oldestIndex);
                Debug.Log($"Minion at index {oldestIndex} removed.");
            }
            availableIndex = oldestIndex; // 제거한 자리 재사용
        }

        // 새로운 미니언 소환
        GameObject prefabInstance = Instantiate(prefab[randomPrefabIndex], spawnPositions[availableIndex].position, spawnPositions[availableIndex].rotation);
        Minion minion = prefabInstance.GetComponent<Minion>();
        effectInstance = Instantiate(spawnEffect, spawnPositions[availableIndex].position, spawnPositions[availableIndex].rotation);
        Destroy(effectInstance, 0.2f);

        minion.SpawnIndex = availableIndex;
        minion.OnMinionDestroyed += HandleMinionDestroyed;

        usedIndices.Add(availableIndex);
        activeMinions[availableIndex] = minion;

        // 새로 소환된 경우에만 큐에 추가
        if (!minionQueue.Contains(availableIndex))
        {
            minionQueue.Enqueue(availableIndex);
        }

        Debug.Log($"Common Minion Spawned at index {availableIndex}, Position: {spawnPositions[availableIndex].position}");
    }

    private void SpawnLegendMinion(GameObject[] prefab)
    {
        int availableIndex = GetAvailableIndex();

        if (availableIndex == -1)
        {
            int oldestIndex = minionQueue.Dequeue();
            if (activeMinions.ContainsKey(oldestIndex))
            {
                Destroy(activeMinions[oldestIndex].gameObject);
                activeMinions.Remove(oldestIndex);
                usedIndices.Remove(oldestIndex);
                Debug.Log($"Legendary Minion at index {oldestIndex} removed.");
            }
            availableIndex = oldestIndex;
        }

        GameObject prefabInstance = Instantiate(prefab[prefab.Length - 1], spawnPositions[availableIndex].position, spawnPositions[availableIndex].rotation);
        Minion minion = prefabInstance.GetComponent<Minion>();
        effectInstance = Instantiate(spawnEffect, spawnPositions[availableIndex].position, spawnPositions[availableIndex].rotation);
        Destroy(effectInstance, 0.2f);

        minion.SpawnIndex = availableIndex;
        minion.OnMinionDestroyed += HandleMinionDestroyed;

        usedIndices.Add(availableIndex);
        activeMinions[availableIndex] = minion;

        if (!minionQueue.Contains(availableIndex))
        {
            minionQueue.Enqueue(availableIndex);
        }

        Debug.Log($"Legendary Minion Spawned at index {availableIndex}, Position: {spawnPositions[availableIndex].position}");
    }
}