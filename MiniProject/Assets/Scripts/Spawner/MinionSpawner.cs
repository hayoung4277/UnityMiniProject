using System.Collections;
using System.Collections.Generic;
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

    public GameObject spawnEffect;

    private int prefabCount = 7;

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
                case 1:
                    SpawnCommonMinion(singleShotPrefabs);
                    break;
                case 2:
                    SpawnCommonMinion(fanShotPrefabs);
                    break;
                case 3:
                    SpawnCommonMinion(shieldPrefabs);
                    break;
                case 4:
                    SpawnCommonMinion(multiplePrefabs);
                    break;
                case 5:
                    SpawnCommonMinion(singleShieldPrefabs);
                    break;
                default:
                    Debug.LogWarning($"알 수 없는 프리팹");
                    break;
            }
        }
        
        if (itemType == ItemType.Legendary)
        {
            switch (randomValue)
            {
                case 1:
                    SpawnLegendMinion(singleShotPrefabs);
                    break;
                case 2:
                    SpawnLegendMinion(fanShotPrefabs);
                    break;
                case 3:
                    SpawnLegendMinion(shieldPrefabs);
                    break;
                case 4:
                    SpawnLegendMinion(multiplePrefabs);
                    break;
                case 5:
                    SpawnLegendMinion(singleShieldPrefabs);
                    break;
                default:
                    Debug.LogWarning($"알 수 없는 프리팹");
                    break;
            }
        }
    }

    private int GetAvailableIndex()
    {
        //사용되지 않은 자리 찾기
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            if (!usedIndices.Contains(i))
            {
                Debug.Log($"Available spawn index found: {i}");
                return i; // 빈자리 반환
            }
        }

        Debug.Log("No available index found. Will replace oldest minion.");
        return -1; // 모든 위치가 사용 중
    }

    private void HandleMinionDestroyed(Minion minion)
    {
        if (usedIndices.Contains(minion.SpawnIndex))
        {
            usedIndices.Remove(minion.SpawnIndex);
            activeMinions.Remove(minion.SpawnIndex);
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

        // 새로운 미니언이 즉시 소환되도록 보장
        if (availableIndex >= 0 && availableIndex < spawnPositions.Count)
        {
            GameObject prefabInstance = Instantiate(prefab[randomPrefabIndex], spawnPositions[availableIndex].position, spawnPositions[availableIndex].rotation);
            Minion minion = prefabInstance.GetComponent<Minion>();
            minion.SpawnIndex = availableIndex;
            minion.OnMinionDestroyed += HandleMinionDestroyed;

            // minionQueue에 즉시 갱신
            usedIndices.Add(availableIndex);
            activeMinions[availableIndex] = minion;
            minionQueue.Enqueue(availableIndex); // FIFO 방식 적용

            Debug.Log($"Common Minion Spawned at index {availableIndex}, Position: {spawnPositions[availableIndex].position}");
        }
        else
        {
            Debug.LogError("Error: Invalid spawn index. Minion not spawned.");
        }
    }

    private void SpawnLegendMinion(GameObject[] prefab)
    {
        int availableIndex = GetAvailableIndex();

        if (availableIndex == -1)
        {
            // 빈자리가 없으면 FIFO 방식으로 가장 오래된 미니언 제거
            int oldestIndex = minionQueue.Dequeue();
            if (activeMinions.ContainsKey(oldestIndex))
            {
                Destroy(activeMinions[oldestIndex].gameObject);
                activeMinions.Remove(oldestIndex);
                usedIndices.Remove(oldestIndex);
                Debug.Log($"Legendary Minion at index {oldestIndex} removed.");
            }
            availableIndex = oldestIndex; // 제거한 자리 재사용
        }

        // 새로운 미니언이 즉시 소환되도록 보장
        if (availableIndex >= 0 && availableIndex < spawnPositions.Count)
        {
            GameObject prefabInstance = Instantiate(prefab[prefab.Length - 1], spawnPositions[availableIndex].position, spawnPositions[availableIndex].rotation);
            Minion minion = prefabInstance.GetComponent<Minion>();
            minion.SpawnIndex = availableIndex;
            minion.OnMinionDestroyed += HandleMinionDestroyed;

            // minionQueue에 즉시 갱신
            usedIndices.Add(availableIndex);
            activeMinions[availableIndex] = minion;
            minionQueue.Enqueue(availableIndex); // FIFO 방식 적용

            Debug.Log($"Legendary Minion Spawned at index {availableIndex}, Position: {spawnPositions[availableIndex].position}");
        }
        else
        {
            Debug.LogError("Error: Invalid spawn index. Minion not spawned.");
        }
    }
}

