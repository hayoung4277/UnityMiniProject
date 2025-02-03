using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NomalMonster
{
    public string dataId = "040001";
    private Rigidbody2D rb;

    private UIManager ui;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Data = DataTableManager.NormalMonsterTable.Get(dataId);

        var findUI = GameObject.FindWithTag(GMCT.UI);
        ui = findUI.GetComponent<UIManager>();

        DeathSound = GetComponent<AudioSource>();

        if (Data != null)
        {
            Initialized(Data);
        }
        else
        {
            Debug.LogError($"Enemy1 data with ID '{dataId}' not found.");
        }
    }

    private void Update()
    {
        MonsterDown(rb);
    }

    public override void MonsterDown(Rigidbody2D rb)
    {
        base.MonsterDown(rb);
    }

    public override void Initialized(NormalMonsterData data)
    {
        base.Initialized(data);
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
    }

    public override void Die()
    {
        base.Die();
        ui.AddScore(OfferedScore);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            var playerBullet = collision.gameObject.GetComponent<PlayerBullet>();

            if (playerBullet != null)
            {
                OnDamage(playerBullet.Damage);
            }
            else
            {
                Debug.LogError("PlayerBullet component not found on the collided object.");
            }

        }

        if (collision.gameObject.tag == "DestroyBox")
        {
            Destroy(gameObject);
        }
    }
}
