using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonMeteoPattern : Pattern
{
    private Boss boss;
    private EnemySpawner spawner;

    public SummonMeteoPattern(Boss boss) : base(boss)
    {
        this.boss = boss;
        spawner = GameObject.FindWithTag("EnemySpanwer").GetComponent<EnemySpawner>();
        PatternStartTime = 6f;
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
            FireRate = Random.Range(5f, 12f);
            int randomValue = Random.Range(1, 6);

            for (int i = 0; i < randomValue; i++)
            {
                int randomIntervalTime = Random.Range(1, 6);

                spawner.SpawnUnBreakable();

                yield return new WaitForSeconds(randomIntervalTime);
            }


            yield return new WaitForSeconds(FireRate);
        }
    }

    public override void UpdatePattern()
    {
        base.UpdatePattern();
    }
}
