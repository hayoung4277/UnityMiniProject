using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBullet : Bullet
{
    private Rigidbody2D rb;
    public string dataId = "";

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
        Destroy(gameObject, 5f);
    }

    public override void Initialize(BulletData data)
    {
        base.Initialize(data);
    }

    public override void Fire(Rigidbody2D rb)
    {
        base.Fire(rb);
        Destroy(gameObject, 5f); // 일정 시간 후 삭제
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CanPierce)
        {
            if (collision.gameObject.CompareTag("NormalMonster") || collision.gameObject.CompareTag("Boss"))
            {
                if (PierceCount > 0)
                {
                    PierceCount--;
                    if (PierceCount == 0)
                    {
                        CanPierce = false;
                    }
                    return; // Destroy 방지
                }
            }
        }

        if (!CanPierce)
        {
            if (collision.gameObject.CompareTag("NormalMonster") || collision.gameObject.CompareTag("Boss"))
            {
                Destroy(gameObject);
            }
        }
    }
}
