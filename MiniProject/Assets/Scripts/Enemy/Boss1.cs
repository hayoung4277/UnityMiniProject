using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Boss
{
    private static readonly string dataId = "06001";
    private Rigidbody2D rb;
    //private SpriteRenderer bossSprite;
    private float spawnTime = 60f;
    private float currentSpawnTime = 0f;

    private Vector2 stopPos = new Vector2(0f, 3.6f);

    private GameManager gm;
    private UIManager ui;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Data = DataTableManager.BossTable.Get(dataId);

        var findGm = GameObject.FindWithTag(GMCT.GM);
        gm = findGm.GetComponent<GameManager>();

        var findUI = GameObject.FindWithTag(GMCT.UI);
        ui = findUI.GetComponent<UIManager>();

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

    private void Update()
    {
        currentSpawnTime += Time.deltaTime;
        if (currentSpawnTime >= spawnTime)
        {
            MoveBoss(rb);
        }

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
        ui.AddScore(2000);
        Destroy(gameObject);
        gm.StopGame();
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
            var playerBullet = collision.gameObject.GetComponent<PlayerBullet>();

            if (playerBullet != null)
            {
                OnDamage(playerBullet.Damage);
                Debug.Log($"Damege {playerBullet.Damage}");
            }
            else
            {
                Debug.LogError("PlayerBullet component not found on the collided object.");
            }
        }
    }
}
