using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBullet : Bullet
{
    private Rigidbody2D rb;
    public string dataId = "";
    private Minion ownerMinion;
    private GameObject boomPrefab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Data = DataTableManager.BulletTable.Get(dataId);

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
        if(collision.gameObject.tag == "NormalMonster" || collision.gameObject.tag == "Boss")
        {
            var targetPos = collision.transform.position;
            Boom(targetPos);
            Destroy(gameObject, 1f);
        }
    }

    private void Boom(Vector3 explosionPosition)
    {
        boomPrefab = Resources.Load<GameObject>($"Prefabs/Effect/ExplosionBlue");

        if (boomPrefab != null)
        {
            GameObject.Instantiate(boomPrefab, explosionPosition, Quaternion.identity);
        }

        Debug.Log("BOOM!!");
    }
}
