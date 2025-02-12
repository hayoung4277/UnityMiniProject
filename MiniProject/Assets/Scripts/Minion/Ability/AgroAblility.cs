using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroAblility : Ability
{
    private EnemySpawner spawner;
    private Minion minion;

    public AgroAblility(Minion minion) : base(minion)
    {
        this.minion = minion;
        var findSpanwer = GameObject.FindWithTag("EnemySpawner");
        spawner = findSpanwer.GetComponent<EnemySpawner>();

        FireRate = minion.FireRate;
    }

    public override void Activate()
    {
        Fire(minion);
    }

    public override void Fire(MonoBehaviour callar)
    {
        callar.StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        while(true)
        {
            AgroEnemy();

            yield return new WaitForSeconds(FireRate);
        }
    }

    private void AgroEnemy()
    {
        spawner.SpawnSixRandomMonsters();
    }
}
