using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RazerPattern : Pattern
{
    private Boss boss;
    private Transform tf;

    private GameObject effectPrefab;
    private GameObject effectInstance;

    public RazerPattern(Boss boss) : base(boss)
    {
        this.boss = boss;
        tf = boss.transform;
        FireRate = 10f;
        TimeBetweenShots = 2f;
        PatternStartTime = 5f;
        BulletPrefabs = Resources.Load<GameObject>("Prefabs/Bullet/BossRazer1");
        effectPrefab = Resources.Load<GameObject>("Prefabs/Effect/ready_attack");
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
            Vector3 effectPos = new Vector3(tf.position.x, tf.position.y - 1f, tf.position.z);
            Vector3 Pos = new Vector3(tf.position.x, tf.position.y - 5.5f, tf.position.z);

            effectInstance = GameObject.Instantiate(effectPrefab, effectPos, tf.rotation);

            yield return new WaitForSeconds(TimeBetweenShots);

            GameObject.Instantiate(BulletPrefabs, Pos, tf.rotation);
            GameObject.Destroy(effectInstance);

            yield return new WaitForSeconds(FireRate);
        }
    }

    private void FollowBoss()
    {
        if (effectInstance != null && boss != null) // 보스와 이펙트가 존재하는 경우만 실행
        {
            Vector3 effectPos = new Vector3(tf.position.x, tf.position.y - 1f, tf.position.z);
            effectInstance.transform.position = effectPos; ; // 보스 위치를 따라감
        }
    }

    public override void UpdatePattern()
    {
        FollowBoss();
    }
}
