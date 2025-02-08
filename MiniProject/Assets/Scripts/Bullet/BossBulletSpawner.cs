using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletSpawner : MonoBehaviour
{
    [Header("Boss Bullet")]
    public GameObject bossBullet;
    private GameObject bulletPrefab;
    public Transform bossTransform;
    private float bulletSpeed;

    private float startTSTime = 3f;
    private float startSTTime = 5f;
    private float startHCTime = 8f;

    [Header("SectorBulletPattern")]
    public int sectorBulletCount = 5;    // 탄알 개수
    public float sectorSpreadAngle = 30f; // 부채꼴 각도 (도 단위)
    public float sectorBulletSpeed = 3f;  // 탄알 속도
    public float sectorFireInterval = 3f; // 발사 간격
    public float sectorFireRate = 1f;

    [Header("TripleShotPattern")]
    public float TSBulletSpeed = 3f;
    public int TSBulletCount = 3;
    public float TSFireInterval = 3f;
    public float TSFireRate = 0.5f;

    [Header("HalfCircleBulletPattern")]
    public int HCBulletCount = 10;
    public float HCBulletSpeed = 3f;
    public float HCSpreadAngle = 180f;
    public float HCFireInterval = 6f;
    public float HCFireRate = 0.5f;

    //private void Awake()
    //{
    //    bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet/BossBullet");
    //}

    private void Start()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet/BossBullet");

        if (bulletPrefab == null)
        {
            Debug.LogError("bulletPrefab을 Resources.Load에서 불러오지 못했습니다.");
            return;
        }

        GameObject tempBullet = Instantiate(bulletPrefab); // 임시 생성
        EnemyBullet bulletComponent = tempBullet.GetComponent<EnemyBullet>();

        if (bulletComponent != null)
        {
            bulletSpeed = bulletComponent.Speed;
            Debug.Log($"bulletSpeed: {bulletSpeed}");
        }
        else
        {
            Debug.LogError("bulletPrefab에 EnemyBullet 컴포넌트가 없습니다.");
        }

        Destroy(tempBullet);

        StartCoroutine(SectorBulletPatternCoroutine());  // 부채꼴 패턴 시작
        StartCoroutine(TripleShotBulletPatternCoroutine());  // 일직선 패턴 시작
        StartCoroutine(HalfCircleBulletPatternCoroutine());
    }

    // 부채꼴 패턴을 코루틴으로 실행
    private IEnumerator SectorBulletPatternCoroutine()
    {
        yield return new WaitForSeconds(startSTTime);

        while(true)
        {
            yield return new WaitForSeconds(sectorFireInterval);

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

                    GameObject bullet = Instantiate(bulletPrefab, bossTransform.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;

                    angle += angleStep;
                }
                yield return new WaitForSeconds(sectorFireRate); // 발사 간격
            }
        }
    }

    // 일직선 패턴을 코루틴으로 실행
    private IEnumerator TripleShotBulletPatternCoroutine()
    {
        yield return new WaitForSeconds(startTSTime);

        while (true)
        {
            for (int i = 0; i < 3; i++) // 3번 발사
            {
                float startPosX = 0.5f;
                float startPosY = -0.5f;

                for (int j = 0; j < TSBulletCount; j++)
                {
                    GameObject bullet = Instantiate(bulletPrefab, bossTransform.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * bulletSpeed;

                    bullet.transform.Translate(startPosX, startPosY, 0f);

                    startPosX -= 0.5f;
                }

                yield return new WaitForSeconds(TSFireRate); // 발사 간격
            }

            yield return new WaitForSeconds(TSFireInterval);
        }
    }

    // 반원 패턴을 코루틴으로 실행
    private IEnumerator HalfCircleBulletPatternCoroutine()
    {
        yield return new WaitForSeconds(startHCTime);

        while (true)
        {
            for (int i = 0; i < 4; i++) // 4번 발사
            {
                float startAngle = -HCSpreadAngle;
                float angleStep = HCSpreadAngle / (HCBulletCount - 1);
                float angle = startAngle;

                for (int j = 0; j < HCBulletCount; j++)
                {
                    float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                    float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                    Vector3 bulletDirection = new Vector3(dirX, dirY, 0f);

                    GameObject bullet = Instantiate(bulletPrefab, bossTransform.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;

                    angle += angleStep;
                }
                yield return new WaitForSeconds(HCFireRate); // 발사 간격
            }

            yield return new WaitForSeconds(HCFireInterval);
        }
    }
}
