using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBullet : Bullet
{
    private Rigidbody2D rb;
    public string dataId = "";

    [Header("HitEffectPrefab")]
    public GameObject hitEffectPrefab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Data = DataTableManager.BulletTable.Get(dataId);

        if(Data != null)
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
        if (collision.gameObject.tag == "NormalMonster")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Boss")
        {
            Destroy(gameObject);
        }
    }

    //private void SpawnHitEffect()
    //{
    //    if (hitEffectPrefab != null)
    //    {
    //        Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
    //    }
    //}
}
