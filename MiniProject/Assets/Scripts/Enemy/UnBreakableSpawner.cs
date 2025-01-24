using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnBreakableSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;

    public float spawnInteval = 5f;
    public float spawnTime = 0f;

    private void Update()
    {
        spawnTime += Time.deltaTime;

        if(spawnTime >= spawnInteval)
        {
            SpawnUnBreakable();

            spawnTime = 0f;
        }
    }

    private void SpawnUnBreakable()
    {
        Instantiate(prefab, parent.position, parent.rotation);
    }
}
