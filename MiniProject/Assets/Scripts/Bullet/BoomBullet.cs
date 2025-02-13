using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBullet : Bullet
{
    private Rigidbody2D rb;
    public string dataId = "";
    private GameObject boomPrefab;

    private bool isExploded = false; // ���� üũ ����
    private bool hasExploded = false; // �ߺ� ���� ����

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
        if (isExploded) return; // �ߺ� ���� ����!

        if (collision.gameObject.CompareTag("NormalMonster") || collision.gameObject.CompareTag("Boss"))
        {
            isExploded = true;
            Boom(collision.transform.position);
            Destroy(gameObject); // ���ݸ� ������ �� ����
        }
    }

    private void Boom(Vector3 explosionPosition)
    {
        if (hasExploded) return; // �ߺ� ���� ����
        hasExploded = true;

        if (boomPrefab != null)
        {
            GameObject.Instantiate(boomPrefab, explosionPosition, Quaternion.identity);
        }
    }
}
