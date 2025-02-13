using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : LivingEntity
{
    public float HP { get; set; }
    public float MoveSpeed { get; set; }
    public float OfferedScore { get; set; }
    public string BossSpriteName { get; set; }
    public string DeathEffectName { get; set; }
    public float DeathEffectPlayTime { get; set; }
    public string DeathSoundName { get; set; }
    public float DeathSoundPlayTime { get; set; }
    public bool IsMoveDown { get; set; }

    public List<Pattern> Patterns { get; private set; } = new List<Pattern>();

    public PlayerBullet PlayerBullet { get; set; }
    public AudioSource AudioSource { get; set; }

    public BossData Data { get; set; }

    public bool IsInVisible { get; set; }
    public string Rank { get; set; }
    public float Slaytime { get; set; }

    public string dataId = "06002";
    private Rigidbody2D rb;

    private float maxHP;
    private float nextTriggerHP;

    private Vector2 stopPos = new Vector2(0.35f, 3.6f);
    private bool isStop;
    public bool isMove;
    public bool isDead;

    private Camera mainCamera;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    private float spriteHalfWidth;

    private UIManager ui;

    public GameObject deathEffect;
    public AudioClip deathSound;

    public event System.Action<Boss> OnSpawnCommonItem;
    public event System.Action<Boss> OnSpawnLegendaryItem;

    private float slayTime = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Data = DataTableManager.Instance.BossTable.Get(dataId);
        mainCamera = Camera.main;
        AudioSource = GetComponent<AudioSource>();


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

        isStop = false;
        isDead = false;
        Slaytime = 0f;
    }

    private void Start()
    {
        for (int i = 0; i < Patterns.Count; i++)
        {
            Debug.Log($"���� {i} Ȱ��ȭ �õ�: {Patterns[i].GetType().Name}");
            Patterns[i].Activate();
        }

        minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));  // ���� �Ʒ�
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));  // ������ ��

        // ��������Ʈ ũ�� ��� (��������ŭ �߰�)
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteHalfWidth = spriteRenderer.bounds.extents.x;  // ���� ���� ũ��
        }

        maxHP = HP; // ���� ���� �� �ִ� HP ����
        nextTriggerHP = maxHP * 0.7f;
    }

    public void Initialized(BossData data)
    {
        HP = data.HP;
        MoveSpeed = data.MoveSpeed;
        OfferedScore = data.OfferedScore;
        BossSpriteName = data.BossSpriteName;
        DeathEffectName = data.DeathEffectName;
        DeathEffectPlayTime = data.DeathEffectPlayTime;
        DeathSoundName = data.DeathSoundName;
        DeathSoundPlayTime = data.DeathSoundPlayTime;
        IsMoveDown = data.IsMoveDown;

        Patterns.Clear();

        foreach (var patternId in data.PatternIds)
        {
            Pattern pattern = PatternFactory.CreatePattern(patternId, this);
            if (pattern != null)
            {
                Patterns.Add(pattern);
            }
            else
            {
                Debug.LogWarning($"Ability with ID {patternId} not found for Minion {data.Id}");
            }
        }
    }

    private void Update()
    {
        slayTime += Time.deltaTime;

        foreach (var pattern in Patterns)
        {
            pattern.UpdatePattern();
        }

        if (HP <= nextTriggerHP)
        {
            OnSpawnCommonItem?.Invoke(this);

            // ���� ��ǥ HP�� 30% �� �ٿ� ����
            nextTriggerHP -= maxHP * 0.3f;

            // HP�� 0 ���Ϸ� �������� �� �̻� �̺�Ʈ�� �߻����� �ʵ��� ��
            if (nextTriggerHP <= 0)
            {
                nextTriggerHP = 0;
            }
        }

        if (IsInVisible && IsMoveDown)
        {
            MoveDownBoss(rb);
        }
        else
        {
            MoveRightBoss(rb);
        }

        if (transform.position.y <= stopPos.y && IsMoveDown)
        {
            StopMove(rb);
        }
        
        if (transform.position.x >= stopPos.x && !IsMoveDown)
        {
            StopMove(rb);
        }

        if (isStop && isMove)
        {
            SideMovement(rb);
            RestrictMovement();
        }
    }

    public override void OnDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        base.Die();
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        OfferScore();
        IsDead = true;
        AudioSource.PlayOneShot(deathSound);
        HP = 0f;
        ui.AddScore(OfferedScore);
        ui.OnRankText();

        DisableSprite();

        OnSpawnLegendaryItem?.Invoke(this);
        OnSpawnLegendaryItem = null;
        OnSpawnCommonItem = null;


        Destroy(gameObject, deathSound.length);
    }

    private void DisableSprite()
    {
        // ��������Ʈ ������ ��Ȱ��ȭ
        GetComponent<SpriteRenderer>().enabled = false;

        // �ݶ��̴� ��Ȱ��ȭ
        if (TryGetComponent<Collider2D>(out var collider))
        {
            collider.enabled = false;
        }
    }

    public void MoveDownBoss(Rigidbody2D rb)
    {
        rb.velocity = Vector3.down * MoveSpeed;
    }

    private void MoveRightBoss(Rigidbody2D rb)
    {
        rb.velocity = transform.right * MoveSpeed;
    }

    private void StopMove(Rigidbody2D rb)
    {
        rb.velocity = Vector3.zero;
        IsInVisible = false;
        isStop = true;
    }

    private void OfferScore()
    {
        if (Slaytime <= 5f)
        {
            OfferedScore *= 5f;
            Rank = "S";
        }
        else if (Slaytime > 5f && Slaytime <= 12.5f)
        {
            OfferedScore *= 3.5f;
            Rank = "A";
        }
        else if (Slaytime > 12.5f && Slaytime <= 30f)
        {
            OfferedScore *= 1.5f;
            Rank = "B";
        }
        else if (Slaytime > 30.1f)
        {
            OfferedScore *= 1f;
            Rank = "C";
        }

        Debug.Log($"SlayScore: {OfferedScore}!");
    }

    private void SideMovement(Rigidbody2D rb)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float leftLimit = Camera.main.ViewportToWorldPoint(Vector3.zero).x + (spriteRenderer.bounds.size.x / 2);
        float rightLimit = Camera.main.ViewportToWorldPoint(Vector3.one).x - (spriteRenderer.bounds.size.x / 2);

        // �̵� ����
        rb.velocity = new Vector2(MoveSpeed, rb.velocity.y);

        // ��� üũ �� ���� ����
        if (transform.position.x <= leftLimit || transform.position.x >= rightLimit)
        {
            MoveSpeed *= -1; // �̵� ���� ����
            rb.velocity = new Vector2(MoveSpeed, rb.velocity.y); // Rigidbody �ӵ��� ����
        }
    }

    private void RestrictMovement()
    {
        Vector3 position = transform.position;

        // ȭ�� ��踦 ����� �ʵ��� ��ġ ����
        position.x = Mathf.Clamp(position.x, minBounds.x + spriteHalfWidth, maxBounds.x - spriteHalfWidth);

        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsInVisible) return;

        IBullet bullet = collision.GetComponent<IBullet>(); // �� ���� ȣ��!
        if (bullet != null)
        {
            float bulletDamage = DataTableManager.Instance.BulletTable.GetBulletDamage(bullet.BulletID);
            if (bulletDamage > 0)
            {
                OnDamage(bulletDamage);
            }
        }
    }
}

