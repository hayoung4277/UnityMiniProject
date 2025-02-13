using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBullet : Bullet
{
    private Rigidbody2D rb;
    public string dataId = "";
    private GameObject boomPrefab;

    private bool isExploded = false; // 폭발 체크 변수
    private bool hasExploded = false; // 중복 방지 변수

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Data = DataTableManager.Instance.BulletTable.Get(dataId);

        if (Data != null)
        {
            Initialize(Data);
        }
        else
        {
            Debug.LogError($"Bullet data with ID '{dataId}' not found.");
        }
    }

    private void Start()
    {
        Fire(rb);
        boomPrefab = Resources.Load<GameObject>($"Prefabs/Effect/ExplosionBlue");
    }

    public override void Initialize(BulletData data)
    {
        base.Initialize(data);
    }

    public override void Fire(Rigidbody2D rb)
    {
        base.Fire(rb);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isExploded) return; // 중복 실행 방지!

        if (collision.gameObject.CompareTag("NormalMonster") || collision.gameObject.CompareTag("Boss"))
        {
            isExploded = true;
            Boom(collision.transform.position);
            Destroy(gameObject); // 조금만 딜레이 후 삭제
        }
    }

    private void Boom(Vector3 explosionPosition)
    {
        if (hasExploded) return; // 중복 실행 방지
        hasExploded = true;

        if (boomPrefab != null)
        {
            GameObject.Instantiate(boomPrefab, explosionPosition, Quaternion.identity);
        }
    }
}
