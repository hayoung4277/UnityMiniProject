using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotPattern : Pattern
{
    private Transform tf;
    private Boss boss;

    public SingleShotPattern(Boss boss) : base(boss)
    {
        this.boss = boss;
        tf = boss.transform;
        FireRate = 3f;
        TimeBetweenShots = 0.3f;
    }

    public override void Activate()
    {
        Fire(boss);
    }

    public override void Fire(MonoBehaviour callar)
    {
        callar.StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            var bullet = Resources.Load<GameObject>("Prefabs/Bullet/BossBullet");

            for (int i = 0; i < 3; i++)
            {
                GameObject.Instantiate(bullet, tf.position, tf.rotation);
                yield return new WaitForSeconds(TimeBetweenShots);
            }

            yield return new WaitForSeconds(FireRate);
        }
    }
}
