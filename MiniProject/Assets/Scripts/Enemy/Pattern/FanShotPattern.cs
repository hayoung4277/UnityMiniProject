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
        PatternStartTime = 5f;
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

    public override void StopFire(MonoBehaviour callar)
    {
        callar.StopCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        yield return new WaitForSeconds(PatternStartTime);

        while (true)
        {
            for (int i = 0; i < 3; i++) // 3번 발사
            {
                float startAngle = -sectorSpreadAngle / 2f - 90f; // 시작 각도 중심을 기준으로 배치
                float angleStep = sectorSpreadAngle / (sectorBulletCount - 1);

                for (int j = 0; j < sectorBulletCount; j++)
                {
                    float angle = startAngle + (angleStep * j); // 탄환별 개별 각도 설정
                    float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                    float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                    Vector3 bulletDirection = new Vector3(dirX, dirY, 0f);

                    // 탄환 인스턴스 생성
                    GameObject bullet = GameObject.Instantiate(BulletPrefabs, tf.position, Quaternion.Euler(0, 0, angle));

                    // Rigidbody2D 가져오기
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    var enemyBullet = bullet.GetComponent<EnemyBullet>();
                    if (rb != null)
                    {
                        rb.velocity = bulletDirection * enemyBullet.Speed;
                    }

                    // Debug 용 로그 (나중에 삭제 가능)
                    Debug.Log($"Bullet {j} - Angle: {angle}, Direction: {bulletDirection}");
                }
                yield return new WaitForSeconds(FireRate); // 발사 간격
            }
        }
    }
}
