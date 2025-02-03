using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : Boss
{
    private static readonly string dataId = "06001";
    private Rigidbody2D rb;

    private Vector2 stopPos = new Vector2(0f, 3.6f);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Data = DataTableManager.BossTable.Get(dataId);

        IsInVisible = true;

        if (Data != null)
        {
            Initialized(Data);
        }
        else
        {
            Debug.LogError($"Boss1 data with ID '{dataId}' not found.");
        }
    }

    private void Start()
    {
        MoveBoss(rb);
    }

    private void Update()
    {
        if (transform.position.y <= stopPos.y)
        {
            StopMove(rb);
        }
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
    }

    public override void Die()
    {
        base.Die();
        HP = 0f;
        Destroy(gameObject);
    }

    public override void MoveBoss(Rigidbody2D rb)
    {
        base.MoveBoss(rb);
    }

    public override void Initialized(BossData data)
    {
        base.Initialized(data);
    }

    private void StopMove(Rigidbody2D rb)
    {
        rb.velocity = Vector3.zero;
        IsInVisible = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" && !IsInVisible)
        {
            var findGo = GameObject.FindWithTag("PlayerBullet");
            PlayerBullet = findGo.GetComponent<PlayerBullet>();

            OnDamage(PlayerBullet.Damage);
            Debug.Log($"Damege {PlayerBullet.Damage}");
        }
    }
}
