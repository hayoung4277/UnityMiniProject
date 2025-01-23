using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunny : Player
{
    private Rigidbody2D rb;
    private static readonly string dataId = "01001";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        DeathSound = GetComponent<AudioSource>();

        // PlayerData ��������
        Data = DataTableManager.PlayerTable.Get(dataId);
        if (Data == null)
        {
            Debug.LogError($"Player data with ID '{dataId}' not found.");
            return;
        }

        // PlayerData�� BulletData�� �ʱ�ȭ
        if (Data != null)
        {
            Initialized(Data);
        }
        else
        {
            Debug.LogError($"Player data with ID '{dataId}' not found.");
            return;
        }
    }

    public override void Initialized(PlayerData data)
    {
        base.Initialized(data);
    }

    public override void Die()
    {
        base.Die();
        HP = 0;
        DeathSound.Play();
        Destroy(gameObject);
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NormalMonster" || collision.gameObject.tag == "UnBreakable")
        {
            Die();
        }
    }
}