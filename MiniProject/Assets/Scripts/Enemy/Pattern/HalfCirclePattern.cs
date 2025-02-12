using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfCirclePattern : Pattern
{
    private Transform tf;
    private Boss boss;

    private int bulletCount;
    private float spreadAngle = 180f;

    public HalfCirclePattern(Boss boss) : base(boss)
    {
        this.boss = boss;
        tf = boss.transform;
        BulletPrefabs = Resources.Load<GameObject>("Prefabs/Bullet/BossBullet");
        FireRate = 6f;
        TimeBetweenShots = 0.5f;
        BulletSpeed = 3f;
        bulletCount = 10;
        PatternStartTime = 8f;
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
        yield return new WaitForSeconds(PatternStartTime);

        while (true)
        {
            for (int i = 0; i < 4; i++) // 4�� �߻�
            {
                float startAngle = -spreadAngle / 2; //-90������ ����
                float angleStep = spreadAngle / (bulletCount - 1);
                float angle = startAngle;

                for (int j = 0; j < bulletCount; j++)
                {
                    float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                    float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                    Vector3 bulletDirection = new Vector3(dirX, dirY, 0f);

                    GameObject bullet = GameObject.Instantiate(BulletPrefabs, tf.position, Quaternion.identity); // ���� ����
                    bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * BulletSpeed;

                    angle += angleStep; // ���� źȯ ���� ����
                }
                yield return new WaitForSeconds(TimeBetweenShots); // �߻� ����
            }

            yield return new WaitForSeconds(FireRate);
        }
    }
}
