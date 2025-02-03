using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    private Rigidbody2D rb;
    private static readonly string dataId = "030001";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Data = DataTableManager.BulletTable.Get(dataId);
        Sound = GetComponent<AudioSource>();

        if (Data != null)
        {
            Initialize(Data);
        }
        else
        {
            Debug.LogError("Bullet data with ID '030001' not found.");
        }
    }

    private void Start()
    {
        Fire(rb);
    }

    public override void Fire(Rigidbody2D rb)
    {
        base.Fire(rb);
    }

    public override void Initialize(BulletData data)
    {
        base.Initialize(data);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "NormalMonster")
        {
            Sound.Play();
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Boss")
        {
            Sound.Play();
            Destroy(gameObject);
        }
    }
}
