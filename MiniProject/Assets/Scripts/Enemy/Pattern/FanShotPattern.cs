using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanShotPattern : Pattern
{
    private Boss boss;
    private Transform tf;

    public int sectorBulletCount = 5;    // 탄알 개수
    public float sectorSpreadAngle = 30f; // 부채꼴 각도 (도 단위)

    public FanShotPattern(Boss boss) : base(boss)
    {
        this.boss = boss;
        tf = boss.transform;
        FireRate = 3f;
        TimeBetweenShots = 0.5f;
        BulletSpeed = 3f;
        PatternStartTime = 5f;
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
            var bullet = Resources.Load<GameObject>("Prefabs/Bullet/BossBullet");

            for (int i = 0; i < 3; i++) // 3번 발사
            {
                float startAngle = -sectorSpreadAngle - 75f;
                float angleStep = sectorSpreadAngle / (sectorBulletCount - 1);
                float angle = startAngle;

                for (int j = 0; j < sectorBulletCount; j++)
                {
                    float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                    float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                    Vector3 bulletDirection = new Vector3(dirX, dirY, 0f);

                    GameObject.Instantiate(bullet, tf.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * BulletSpeed;

                    angle += angleStep;
                }
                yield return new WaitForSeconds(FireRate); // 발사 간격
            }
        }
    }

    public override void UpdatePattern()
    {
        base.UpdatePattern();
    }
}
