using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationPattern : Pattern
{
    private Boss boss;
    private Transform tf;

    private int bulletCount = 3;

    public FormationPattern(Boss boss) : base(boss)
    {
        this.boss = boss;
        tf = boss.transform;
        FireRate = 3f;
        TimeBetweenShots = 0.5f;
        BulletSpeed = 3f;
        BulletPrefabs = Resources.Load<GameObject>($"Prefabs/Bullet/BossBullet");
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
            for (int i = 0; i < 3; i++) // 3번 발사
            {
                float startPosX = 0.5f;
                float startPosY = -0.5f;

                for (int j = 0; j < bulletCount; j++)
                {
                    var bulletInstance = GameObject.Instantiate(BulletPrefabs, tf.position, Quaternion.identity);
                    bulletInstance.GetComponent<Rigidbody2D>().velocity = Vector3.down * BulletSpeed;

                    bulletInstance.transform.Translate(startPosX, startPosY, 0f);

                    startPosX -= 0.5f;
                }

                yield return new WaitForSeconds(TimeBetweenShots); // 발사 간격
            }

            yield return new WaitForSeconds(FireRate);
        }
    }
}
