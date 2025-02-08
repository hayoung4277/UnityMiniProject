using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    public GameObject prefab;
    //public Transform[] spawnPos;
    private List<Transform> spawnPositions = new List<Transform>();

    private List<int> usedIndices = new List<int>(); // ���� ��ġ ����
    private Dictionary<int, Minion> activeMinions = new Dictionary<int, Minion>(); // �ε����� Minion ����

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

    public void SpawnMinion()
    {
        int availableIndex = GetAvailableIndex();

        if (availableIndex == -1) 
            return;

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
}
