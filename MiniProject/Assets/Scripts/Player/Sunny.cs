using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunny : Player
{
    private Rigidbody2D rb;
    private CircleCollider2D circle;
    private static readonly string dataId = "01001";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        DeathSound = GetComponent<AudioSource>();

        Data = DataTableManager.PlayerTable.Get(dataId);
        if (Data != null)
        {
            Initialized(Data);
        }
        else
        {
            Debug.LogError($"Player data with ID '{dataId}' not found.");
        }
    }

    private void Update()
    {

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

        if (collision.gameObject.tag == "NormalMonster")
        {
            Die();
        }

        if(collision.gameObject.tag == "UnBreakable")
        {
            Die();
        }
    }
}
