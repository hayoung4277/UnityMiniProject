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

    private List<int> usedIndices = new List<int>(); // ���� ��ġ ����
    private Dictionary<int, Minion> activeMinions = new Dictionary<int, Minion>(); // �ε����� Minion ����

    private Queue<int> minionQueue = new Queue<int>(); // FIFO ���� ����

    private void Start()
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
                default: Debug.LogWarning("�� �� ���� Common ������"); break;
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
                default: Debug.LogWarning("�� �� ���� Legendary ������"); break;
            }
        }
    }

    private int GetAvailableIndex()
    {
        // ������ ���� �ڸ� ã��
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            if (!usedIndices.Contains(i))
            {
                return i; // ���ڸ� ��ȯ
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
            minionQueue = new Queue<int>(minionQueue.Where(index => index != minion.SpawnIndex)); // ť������ ����
        }
        Debug.Log($"Minion Destroyed! Index {minion.SpawnIndex} is now free.");
    }

    private void SpawnCommonMinion(GameObject[] prefab)
    {
        int availableIndex = GetAvailableIndex();
        int randomPrefabIndex = Random.Range(0, prefab.Length);

        if (availableIndex == -1)
        {
            // ���ڸ��� ������ FIFO ������� ���� ������ �̴Ͼ� ����
            int oldestIndex = minionQueue.Dequeue();
            if (activeMinions.ContainsKey(oldestIndex))
            {
                Destroy(activeMinions[oldestIndex].gameObject);
                activeMinions.Remove(oldestIndex);
                usedIndices.Remove(oldestIndex);
                Debug.Log($"Minion at index {oldestIndex} removed.");
            }
            availableIndex = oldestIndex; // ������ �ڸ� ����
        }

        // ���ο� �̴Ͼ� ��ȯ
        GameObject prefabInstance = Instantiate(prefab[randomPrefabIndex], spawnPositions[availableIndex].position, spawnPositions[availableIndex].rotation);
        Minion minion = prefabInstance.GetComponent<Minion>();
        effectInstance = Instantiate(spawnEffect, spawnPositions[availableIndex].position, spawnPositions[availableIndex].rotation);
        Destroy(effectInstance, 0.2f);

        minion.SpawnIndex = availableIndex;
        minion.OnMinionDestroyed += HandleMinionDestroyed;

        usedIndices.Add(availableIndex);
        activeMinions[availableIndex] = minion;

        // ���� ��ȯ�� ��쿡�� ť�� �߰�
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