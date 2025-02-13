using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanShotPattern : Pattern
{
    private Boss boss;
    private Transform tf;

    public int sectorBulletCount = 5;    // ź�� ����
    public float sectorSpreadAngle = 30f; // ��ä�� ���� (�� ����)

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
            for (int i = 0; i < 3; i++) // 3�� �߻�
            {
                float startAngle = -sectorSpreadAngle / 2f - 90f; // ���� ���� �߽��� �������� ��ġ
                float angleStep = sectorSpreadAngle / (sectorBulletCount - 1);

                for (int j = 0; j < sectorBulletCount; j++)
                {
                    float angle = startAngle + (angleStep * j); // źȯ�� ���� ���� ����
                    float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                    float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                    Vector3 bulletDirection = new Vector3(dirX, dirY, 0f);

                    // źȯ �ν��Ͻ� ����
                    GameObject bullet = GameObject.Instantiate(BulletPrefabs, tf.position, Quaternion.Euler(0, 0, angle));

                    // Rigidbody2D ��������
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    var enemyBullet = bullet.GetComponent<EnemyBullet>();
                    if (rb != null)
                    {
                        rb.velocity = bulletDirection * enemyBullet.Speed;
                    }

                    // Debug �� �α� (���߿� ���� ����)
                    Debug.Log($"Bullet {j} - Angle: {angle}, Direction: {bulletDirection}");
                }
                yield return new WaitForSeconds(FireRate); // �߻� ����
            }
        }
    }
}
