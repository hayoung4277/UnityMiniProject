using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : NomalMonster
{
    public string dataId = "040001";
    private Rigidbody2D rb;
    private AudioSource audioSource;
    public AudioClip deathSound;

    private bool isInvisible;

    private UIManager ui;

    public event System.Action<Enemy> OnSpawnItem;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Data = DataTableManager.Instance.NormalMonsterTable.Get(dataId);
        audioSource = GetComponent<AudioSource>();

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

        isInvisible = true;
    }

    private void Update()
    {
        MonsterDown(rb);

        if (transform.position.y <= 4.4f)
        {
            isInvisible = false;
        }
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
        if (HP > 0)
        {
            base.OnDamage(damage);
        }
    }

    public override void Die()
    {
        base.Die();
        audioSource.PlayOneShot(deathSound);
        ui.AddScore(OfferedScore);

        float randomValue = Random.Range(0f, 0.5f);

        if (randomValue <= DropRate)
        {
            // 아이템 드롭 이벤트 호출
            OnSpawnItem?.Invoke(this);
            // 모든 이벤트 구독 해제
            OnSpawnItem = null;
        }

        DisableSprite();

        Destroy(gameObject, deathSound.length);
    }
    
    private void DisableSprite()
    {
        // 스프라이트 렌더러 비활성화
        GetComponent<SpriteRenderer>().enabled = false;

        // 콜라이더 비활성화
        GetComponent<PolygonCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isInvisible) return;

        IBullet bullet = collision.GetComponent<IBullet>(); // 한 번만 호출!
        if (bullet != null)
        {
            float bulletDamage = DataTableManager.Instance.BulletTable.GetBulletDamage(bullet.BulletID);
            if (bulletDamage > 0)
            {
                OnDamage(bulletDamage);
            }
        }

        if (collision.gameObject.CompareTag("DestroyBox"))
        {
            Destroy(gameObject);
        }
    }
}
