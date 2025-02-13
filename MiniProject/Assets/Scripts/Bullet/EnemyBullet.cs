using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float Damage { get; private set; }
    public float Speed { get; private set; }
    public string EffectName { get; private set; }
    public bool CanGuided { get; private set; }

    public EnemyBulletData Data { get; private set; }

    public string dataId = "70001";

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Data = DataTableManager.Instance.EnemyBulletTable.Get(dataId);
        if (Data == null )
        {
            Debug.LogError($"EnemyBullet data with ID '{dataId}' not found.");
            return;
        }

        if(Data != null)
        {
            Initialized(Data);
        }
        else
        {
            Debug.LogError($"EnemyBullet data with ID '{dataId}' not found.");
            return;
        }
    }

    //private void Start()
    //{
    //    Move();
    //}

    private void Initialized(EnemyBulletData data)
    {
        Damage = data.Damage;
        Speed = data.Speed;
        EffectName = data.EffectName;
        CanGuided = data.CanGuided;
    }

    //private void Move()
    //{
    //    //rb.velocity = Vector3.down * Speed;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "DestroyBox")
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
