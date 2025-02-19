using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //보스 스폰
    [Header("Boss")]
    public GameObject[] bossPrefabs;
    private GameObject bossInstance;
    public Transform parent;
    private int index = 0;
    private int deathCount = 0;
    private bool bossDeathCounted = false;

    private Boss bossComponent;

    public float bossSpawnInterval = 40f;
    public float bossSpawnTime = 0f;

    //일반 적 스폰
    [Header("Normal Enemy")]
    public GameObject[] monsterPrefabs; // 몬스터 프리팹 배열
    public Vector3 spawnPos;
    private int spawnSixCount = 6; // 소환할 몬스터 개수
    private int spawnEightCount = 8;

    public float normalSpawnInterval = 3f;
    private float normalSpawnTime = 0f;

    //파괴불가 적 스폰
    [Header("UnBreakable Enemy")]
    public GameObject unBreakablePrefab;
    public Transform unBSpawnPos;

    public float unBSpawnInterval = 5f;
    private float unBSpawnMinInterval = 7f;
    private float unBSpawnMaxInterval = 15f;
    private float unBSpawnTime = 0f;

    private GameManager gm;
    private UIManager ui;

    private Vector3 bossSpawnPos;
    private Quaternion bossSpawnRotation;

    public static event System.Action<Enemy> OnEnemySpawned;
    public static event System.Action<Boss> OnBossSpawned;

    private void Update()
    {
        if (bossInstance == null)
        {
            bossSpawnTime += Time.deltaTime;
        }

        normalSpawnTime += Time.deltaTime;
        unBSpawnTime += Time.deltaTime;

        gm = GameObject.FindWithTag(GMCT.GM).GetComponent<GameManager>();
        ui = GameObject.FindWithTag(GMCT.UI).GetComponent<UIManager>();

        if (bossSpawnTime >= bossSpawnInterval && index < bossPrefabs.Length)
        {
            StartCoroutine(SpawnBossCoroutine());
            Debug.Log($"{index}");
            index++;

            bossSpawnTime = 0f;
        }

        if (normalSpawnTime >= normalSpawnInterval && bossInstance == null)
        {
            SpawnSixRandomMonsters();

            normalSpawnTime = 0f;
        }

        if (unBSpawnTime >= unBSpawnInterval && bossInstance == null)
        {
            SpawnUnBreakable();

            unBSpawnTime = 0f;
        }

        if (bossInstance != null && bossComponent.IsDead && !bossDeathCounted)
        {
            bossDeathCounted = true; // 중복 카운트 방지
            deathCount++;
            bossInstance = null;
            Debug.Log($"DeathCount: {deathCount}");
        }

        if (deathCount >= bossPrefabs.Length)
        {
            gm.StopGame();
            ui.GameClear();
        }
    }

    private void SpawnBoss()
    {
        var tempBossInstance = Instantiate(bossPrefabs[index], bossPrefabs[index].transform.position, bossPrefabs[index].transform.rotation);
        bossSpawnPos = tempBossInstance.transform.position;
        bossSpawnRotation = tempBossInstance.transform.rotation;
        Destroy(tempBossInstance);

        bossInstance = Instantiate(bossPrefabs[index], bossSpawnPos, bossSpawnRotation);
        bossComponent = bossInstance.GetComponent<Boss>();
        bossComponent.isDead = false;
        bossDeathCounted = false;

        if (bossComponent != null)
        {
            OnBossSpawned?.Invoke(bossComponent); // 이벤트 호출
        }
    }

    private IEnumerator SpawnBossCoroutine()
    {
        ui.OnWarningPopUp();

        yield return new WaitForSeconds(2f);

        SpawnBoss();
    }

    public void SpawnSixRandomMonsters()
    {
        Vector3 currentSpawnPos = spawnPos;
        currentSpawnPos.z = 0f;

        // 몬스터 프리팹 중 랜덤으로 6개 선택
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
                OnEnemySpawned?.Invoke(enemyComponent); // 이벤트 호출
            }
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

    public void SpawnUnBreakable()
    {
        Instantiate(unBreakablePrefab, unBSpawnPos.position, unBSpawnPos.rotation);

        unBSpawnInterval = Random.Range(unBSpawnMinInterval, unBSpawnMaxInterval);
    }
}
