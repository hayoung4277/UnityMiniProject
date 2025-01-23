using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon2 : NomalMonster
{
    private static readonly string dataId = "040002";
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Data = DataTableManager.NormalMonsterTable.Get(dataId);
        var findGo = GameObject.FindWithTag("Player");
        Player = findGo.GetComponent<Player>();
        DeathSound = GetComponent<AudioSource>();

        if (Data != null)
        {
            Initialized(Data);
        }
        else
        {
            Debug.LogError($"Bullet data with ID '{dataId}' not found.");
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
        DeathSound.Play();
        AddScore(OfferedScore);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            OnDamage(Player.Data.Damage);
            Debug.Log($"Damege {Player.Data.Damage}");
        }
    }

    public override void AddScore(float amount)
    {
        base.AddScore(amount);
    }
}
