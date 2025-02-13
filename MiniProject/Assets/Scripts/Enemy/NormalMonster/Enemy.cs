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

    public GameObject deathEffect;

    public event System.Action<Enemy> OnSpawnItem;

    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

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

        // 부모-자식 관계 내의 모든 SpriteRenderer 추가
        spriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>(true));

        foreach (Transform child in transform)
        {
            if (child.CompareTag("EnemyPart"))
            {
                SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    spriteRenderers.Add(sr);
                }
            }
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

        // **적의 위치에서 데스 이펙트 생성 (부모 영향 X)**
        GameObject effect = Instantiate(deathEffect);
        effect.gameObject.transform.Translate(transform.position);


        // 이펙트를 월드 공간 기준으로 이동 (부모의 영향을 받지 않도록)
        effect.transform.SetParent(null);

        Destroy(effect, 1f);

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

        //Destroy(deathEffect, deathSound.length);
        Destroy(gameObject, deathSound.length);
    }
    
    private void DisableSprite()
    {
        foreach (var sr in spriteRenderers)
        {
            if (sr != null)
                sr.enabled = false;
        }

        // 콜라이더 비활성화
        GetComponent<EdgeCollider2D>().enabled = false;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
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
