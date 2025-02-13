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
            Debug.Log($"패턴 {i} 활성화 시도: {Patterns[i].GetType().Name}");
            Patterns[i].Activate();
        }

        minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));  // 왼쪽 아래
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));  // 오른쪽 위

        // 스프라이트 크기 고려 (반지름만큼 추가)
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteHalfWidth = spriteRenderer.bounds.extents.x;  // 가로 절반 크기
        }

        maxHP = HP; // 게임 시작 시 최대 HP 저장
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

            // 다음 목표 HP를 30% 더 줄여 설정
            nextTriggerHP -= maxHP * 0.3f;

            // HP가 0 이하로 내려가면 더 이상 이벤트가 발생하지 않도록 함
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
        // 스프라이트 렌더러 비활성화
        GetComponent<SpriteRenderer>().enabled = false;

        // 콜라이더 비활성화
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

        // 이동 적용
        rb.velocity = new Vector2(MoveSpeed, rb.velocity.y);

        // 경계 체크 및 방향 반전
        if (transform.position.x <= leftLimit || transform.position.x >= rightLimit)
        {
            MoveSpeed *= -1; // 이동 방향 반전
            rb.velocity = new Vector2(MoveSpeed, rb.velocity.y); // Rigidbody 속도도 변경
        }
    }

    private void RestrictMovement()
    {
        Vector3 position = transform.position;

        // 화면 경계를 벗어나지 않도록 위치 제한
        position.x = Mathf.Clamp(position.x, minBounds.x + spriteHalfWidth, maxBounds.x - spriteHalfWidth);

        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsInVisible) return;

        IBullet bullet = collision.GetComponent<IBullet>(); // 한 번만 호출!
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

