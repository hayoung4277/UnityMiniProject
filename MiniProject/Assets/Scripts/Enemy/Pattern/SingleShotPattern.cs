using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotPattern : Pattern
{
    private Transform tf;

    public SingleShotPattern(Boss boss) : base(boss)
    {
        tf = boss.transform;
    }

    public override void Activate()
    {
        base.Activate();
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
                yield return new WaitForSeconds(0.3f);
            }

            yield return new WaitForSeconds(3f);
        }
    }
}
