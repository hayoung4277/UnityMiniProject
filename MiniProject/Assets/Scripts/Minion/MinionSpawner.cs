using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    public GameObject prefab;
    private List<Transform> spawnPositions = new List<Transform>();

    private List<int> usedIndices = new List<int>(); // ���� ��ġ ����
    private Dictionary<int, Minion> activeMinions = new Dictionary<int, Minion>(); // �ε����� Minion ����

    private static readonly List<int> commonProbabilities = new List<int> { 50, 30, 15, 4, 1 }; // �Ϲ� ���޻��� Ȯ�� (1~5 ��͵�)
    private static readonly List<int> legendaryProbabilities = new List<int> { 0, 0, 0, 60, 40 }; // ���� ���޻��� Ȯ�� (4~5 ��͵�)

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            foreach (Transform child in player.transform)
            {
                if (child.CompareTag("SpawnPos")) // "SpawnPos" �±װ� �ִ� �ڽĸ� �߰�
                {
                    spawnPositions.Add(child);
                }
            }

            Debug.Log($"SpawnPos ����: {spawnPositions.Count}");
        }
    }

    public void SpawnMinion(ItemType itemType)
    {
        int availableIndex = GetAvailableIndex();

        if (availableIndex == -1)
            return;

        int rarity = GetRarityBasedOnItem(itemType); // ������ Ÿ�Կ� ���� ��͵� ���
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
        return -1; // ��� ��ġ�� ��� ��
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

        // Ȯ���� �������� ���� �� �̱�
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
                return i + 1; // ��͵� 1���� ����
            }
        }

        return 1; // �⺻������ ��͵� 1 ��ȯ
    }
}

public enum ItemType
{
    Common,   // �Ϲ� ���޻���
    Legendary // ���� ���޻���
}

