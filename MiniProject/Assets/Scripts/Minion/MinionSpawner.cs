using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    public GameObject prefab;
    private List<Transform> spawnPositions = new List<Transform>();

    private List<int> usedIndices = new List<int>(); // 사용된 위치 저장
    private Dictionary<int, Minion> activeMinions = new Dictionary<int, Minion>(); // 인덱스별 Minion 저장

    private static readonly List<int> commonProbabilities = new List<int> { 50, 30, 15, 4, 1 }; // 일반 보급상자 확률 (1~5 희귀도)
    private static readonly List<int> legendaryProbabilities = new List<int> { 0, 0, 0, 60, 40 }; // 전설 보급상자 확률 (4~5 희귀도)

    void Start()
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
        int availableIndex = GetAvailableIndex();

        if (availableIndex == -1)
            return;

        int rarity = GetRarityBasedOnItem(itemType); // 아이템 타입에 따른 희귀도 계산
        GameObject prefabInstance = Instantiate(prefab, spawnPositions[availableIndex].position, spawnPositions[availableIndex].rotation);
        Minion minion = prefabInstance.GetComponent<Minion>();
        minion.SpawnIndex = availableIndex;
        minion.OnMinionDestroyed += HandleMinionDestroyed;

        usedIndices.Add(availableIndex);
        activeMinions[availableIndex] = minion;
    }

    private int GetAvailableIndex()
    {
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            if (!usedIndices.Contains(i))
            {
                return i;
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
        }
        Debug.Log($"Minion Destroyed! Index {minion.SpawnIndex} is now free.");
    }

    private int GetRarityBasedOnItem(ItemType itemType)
    {
        List<int> probabilities = itemType == ItemType.Legendary ? legendaryProbabilities : commonProbabilities;

        // 확률을 기준으로 랜덤 값 뽑기
        int total = 0;
        foreach (int prob in probabilities)
        {
            total += prob;
        }

        int randomValue = UnityEngine.Random.Range(0, total);

        int cumulativeProbability = 0;
        for (int i = 0; i < probabilities.Count; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomValue < cumulativeProbability)
            {
                return i + 1; // 희귀도 1부터 시작
            }
        }

        return 1; // 기본적으로 희귀도 1 반환
    }
}

public enum ItemType
{
    Common,   // 일반 보급상자
    Legendary // 전설 보급상자
}

