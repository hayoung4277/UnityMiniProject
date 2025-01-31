using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletSpawner : MonoBehaviour
{
    [Header("Boss Bullet")]
    public GameObject bossBullet;
    public Transform bossTransform;
    private float startTime = 5;

    [Header("SectorBulletPattern")]
    public int sectorBulletCount = 5;    // ź�� ����
    public float sectorSpreadAngle = 30f; // ��ä�� ���� (�� ����)
    public float sectorBulletSpeed = 3f;  // ź�� �ӵ�
    public float sectorFireInterval = 5f; // �߻� ����
    private float sectorFireRate = 1f;

    [Header("ThreeStraightPattern")]
    public float TSBulletSpeed = 3f;
    public int TSBulletCount = 3;
    public float TSFireInterval = 3f;
    private float TSFireRate = 0.5f;

    [Header("HalfCircleBulletPattern")]
    public int HCBulletCount = 10;
    public float HCBulletSpeed = 3f;
    public float HCSpreadAngle = 180f;
    public float HCFireInterval = 5f;
    private float HCFireRate = 1f;

    private void Start()
    {
        StartBulletPatterns();
    }


    // ��ä�� ������ �ڷ�ƾ���� ����
    private IEnumerator SectorBulletPatternCoroutine()
    {
        for (int i = 0; i < 3; i++) // 3�� �߻�
        {
            float startAngle = -sectorSpreadAngle - 60f;
            float angleStep = sectorSpreadAngle / (sectorBulletCount - 1);
            float angle = startAngle;

            for (int j = 0; j < sectorBulletCount; j++)
            {
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 bulletDirection = new Vector3(dirX, dirY, 0f);

                GameObject bullet = Instantiate(bossBullet, bossTransform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * sectorBulletSpeed;

                angle += angleStep;
            }
            yield return new WaitForSeconds(sectorFireInterval); // �߻� ����
        }
    }

    // ������ ������ �ڷ�ƾ���� ����
    private IEnumerator ThreeStraightBulletPatternCoroutine()
    {
        for (int i = 0; i < 3; i++) // 3�� �߻�
        {
            float startPosX = 0.15f;
            float startPosY = -0.5f;

            for (int j = 0; j < TSBulletCount; j++)
            {
                GameObject bullet = Instantiate(bossBullet, bossTransform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * TSBulletSpeed;

                bullet.transform.Translate(startPosX, startPosY, 0f);

                startPosX -= 0.5f;
            }

            yield return new WaitForSeconds(TSFireInterval); // �߻� ����
        }
    }

    // �ݿ� ������ �ڷ�ƾ���� ����
    private IEnumerator HalfCircleBulletPatternCoroutine()
    {
        for (int i = 0; i < 4; i++) // 4�� �߻�
        {
            float startAngle = -HCSpreadAngle - 60f;
            float angleStep = HCSpreadAngle / (HCBulletCount - 1);
            float angle = startAngle;

            for (int j = 0; j < HCBulletCount; j++)
            {
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 bulletDirection = new Vector3(dirX, dirY, 0f);

                GameObject bullet = Instantiate(bossBullet, bossTransform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * HCBulletSpeed;

                angle += angleStep;
            }
            yield return new WaitForSeconds(HCFireInterval); // �߻� ����
        }
    }

    // �� ������ �����ϴ� �Լ�
    public void StartBulletPatterns()
    {
        StartCoroutine(SectorBulletPatternCoroutine());  // ��ä�� ���� ����
        StartCoroutine(ThreeStraightBulletPatternCoroutine());  // ������ ���� ����
        StartCoroutine(HalfCircleBulletPatternCoroutine());  // �ݿ� ���� ����
    }
}
