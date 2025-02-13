using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroPattern : Pattern
{
    private Boss boss;
    private EnemySpawner spawner;

    public AgroPattern(Boss boss) : base(boss)
    {
        this.boss = boss;
        spawner = GameObject.FindWithTag("EnemySpanwer").GetComponent<EnemySpawner>();
    }

    public override void Activate()
    {
        Fire(boss);
    }

    public override void Fire(MonoBehaviour callar)
    {
        callar.StartCoroutine(FireCoroutine());
    }

    public override void StopFire(MonoBehaviour callar)
    {
        callar.StopCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        yield return new WaitForSeconds(PatternStartTime);

        while (true)
        {
            FireRate = Random.Range(2f, 7f);

            spawner.SpawnSixRandomMonsters();

            yield return new WaitForSeconds(FireRate);
        }
    }
}
