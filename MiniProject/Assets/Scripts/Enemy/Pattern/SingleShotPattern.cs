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
        BulletPrefabs = Resources.Load<GameObject>("Prefabs/Bullet/BossBullet");
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
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject bullet = GameObject.Instantiate(BulletPrefabs, tf.position, tf.rotation);

                // Rigidbody2D 가져오기
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                var enemyBullet = bullet.GetComponent<EnemyBullet>();
                if (rb != null)
                {
                    rb.velocity = Vector3.down * enemyBullet.Speed;
                }

                yield return new WaitForSeconds(TimeBetweenShots);
            }

            yield return new WaitForSeconds(FireRate);
        }
    }
}
