using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : NomalMonster
{
    private static readonly string dataId = "040001";
    private Rigidbody2D rb;

    private GameManager gm;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Data = DataTableManager.NormalMonsterTable.Get(dataId);

        var findGo = GameObject.FindWithTag("PlayerBullet");
        PlayerBullet = findGo.GetComponent<PlayerBullet>();

        var findGm = GameObject.FindWithTag(GMCT.GM);
        gm = findGm.GetComponent<GameManager>();

        DeathSound = GetComponent<AudioSource>();

        if(Data != null)
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
        DeathSound.Play();
        gm.AddScore(OfferedScore);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            OnDamage(PlayerBullet.Damage);
        }
        
        if(collision.gameObject.tag == "DestroyBox")
        {
            Destroy(gameObject);
        }
    }
}
