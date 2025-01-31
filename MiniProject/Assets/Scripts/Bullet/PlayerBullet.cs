using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    private Rigidbody2D rb;
    public string dataId = "030001";

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
            Debug.LogError($"Bullet data with ID '{dataId}' not found.");
        }
    }

    private void Start()
    {
        Fire(rb);
        Destroy(gameObject, 5f);
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
        if (collision.gameObject.tag == "NormalMonster")
        {
            Sound.Play();
            if (!Sound.isPlaying)
            {
                Destroy(gameObject);
            }

        }

        if (collision.gameObject.tag == "Boss")
        {
            Sound.Play();
            if (!Sound.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}
