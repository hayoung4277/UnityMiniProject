using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //���� ����
    [Header("Boss")]
    public GameObject[] bossPrefabs;
    private GameObject bossInstance;
    public Transform parent;
    private int index = 0;

    public float bossSpawnInterval = 60f;
    private float bossSpawnTime = 0f;

    //�Ϲ� �� ����
    [Header("Normal Enemy")]
    public GameObject[] monsterPrefabs; // ���� ������ �迭
    public Vector3 spawnPos;
    private int spawnSixCount = 6; // ��ȯ�� ���� ����
    private int spawnEightCount = 8;

    public float normalSpawnInterval = 3f;
    private float normalSpawnTime = 0f;

    //�ı��Ұ� �� ����
    [Header("UnBreakable Enemy")]
    public GameObject unBreakablePrefab;
    public Transform unBSpawnPos;

    public float unBSpawnInterval = 5f;
    private float unBSpawnMinInterval = 7f;
    private float unBSpawnMaxInterval = 15f;
    private float unBSpawnTime = 0f;

    private GameManager gm;

    public static event System.Action<Enemy> OnEnemySpawned;

    private void Update()
    {
        bossSpawnTime += Time.deltaTime;
        normalSpawnTime += Time.deltaTime;
        unBSpawnTime += Time.deltaTime;

        gm = GameObject.FindWithTag(GMCT.GM).GetComponent<GameManager>();

        if (bossSpawnTime >= bossSpawnInterval && index < bossPrefabs.Length)
        {
            SpawnBoss();

            bossSpawnTime = 0f;
            index++;
        }

        if(normalSpawnTime >= normalSpawnInterval && bossInstance == null)
        {
            SpawnSixRandomMonsters();

            normalSpawnTime = 0f;
        }

        if(unBSpawnTime >= unBSpawnInterval && bossInstance == null)
        {
            SpawnUnBreakable();

            unBSpawnTime = 0f;
        }

        if (index >= bossPrefabs.Length)
        {
            gm.StopGame();
        }
    }

    private void SpawnBoss()
    {
        bossInstance = Instantiate(bossPrefabs[index], parent.position, parent.rotation);
        Debug.Log($"Boss Length: {bossPrefabs.Length}");
    }

    private void SpawnSixRandomMonsters()
    {
        Vector3 currentSpawnPos = spawnPos;
        currentSpawnPos.z = 0f;

        // ���� ������ �� �������� 6�� ����
        List<GameObject> selectedMonsters = GetRandomMonsters(monsterPrefabs, spawnSixCount);

        for (int i = 0; i < spawnSixCount; i++)
        {
            var monster = selectedMonsters[i];

            monster.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            GameObject instance = Instantiate(monster, currentSpawnPos, Quaternion.identity);
            currentSpawnPos.x += 0.8f;

            Enemy enemyComponent = instance.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                OnEnemySpawned?.Invoke(enemyComponent); // �̺�Ʈ ȣ��
            }
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

    private void SpawnUnBreakable()
    {
        Instantiate(unBreakablePrefab, unBSpawnPos.position, unBSpawnPos.rotation);

        unBSpawnInterval = Random.Range(unBSpawnMinInterval, unBSpawnMaxInterval);
    }
}
