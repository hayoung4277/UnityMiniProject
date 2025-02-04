using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform[] spawnPos;

    private List<int> usedIndices = new List<int>(); // 사용된 위치 저장
    private Dictionary<int, Minion> activeMinions = new Dictionary<int, Minion>(); // 인덱스별 Minion 저장

    //private void Update()
    //{
    //    if (Input.GetMouseButtonUp(0) && usedIndices.Count < spawnPos.Length)
    //    {
    //        SpawnMinion();
    //    }
    //}

    public void SpawnMinion()
    {
        int availableIndex = GetAvailableIndex();

        if (availableIndex == -1) 
            return;

        GameObject prefabInstance = Instantiate(prefab, spawnPos[availableIndex].position, spawnPos[availableIndex].rotation);
        Minion minion = prefabInstance.GetComponent<Minion>();
        minion.SpawnIndex = availableIndex;
        minion.OnMinionDestroyed += HandleMinionDestroyed;

        usedIndices.Add(availableIndex);
        activeMinions[availableIndex] = minion;
    }

    private int GetAvailableIndex()
    {
        for (int i = 0; i < spawnPos.Length; i++)
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
}
