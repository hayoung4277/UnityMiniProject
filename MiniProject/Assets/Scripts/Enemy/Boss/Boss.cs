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

    public List<Pattern> Patterns { get; private set; } = new List<Pattern>();

    public PlayerBullet PlayerBullet { get; set; }
    public AudioSource AudioSource { get; set; }

    public BossData Data { get; set; }

    public bool IsInVisible { get; set; }

    public string dataId = "06002";
    private Rigidbody2D rb;

    private Vector2 stopPos = new Vector2(0f, 3.6f);
    private bool isStop;
    public bool isMove;
    public bool isDead;

    private Camera mainCamera;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    private float spriteHalfWidth;
    private float spriteHalfHeight;

    private GameManager gm;
    private UIManager ui;

    public AudioClip deathSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Data = DataTableManager.BossTable.Get(dataId);
        mainCamera = Camera.main;
        AudioSource = GetComponent<AudioSource>();

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

        isStop = false;
        isDead = false;
    }

    private void Start()
    {
        foreach (var pattern in Patterns)
        {
            pattern.Activate();
        }

        minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));  // ���� �Ʒ�
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));  // ������ ��

        // ��������Ʈ ũ�� ��� (��������ŭ �߰�)
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteHalfWidth = spriteRenderer.bounds.extents.x;  // ���� ���� ũ��
            spriteHalfHeight = spriteRenderer.bounds.extents.y; // ���� ���� ũ��
        }
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

        Patterns.Clear();

        foreach (var patternId in data.PatternIds)
        {
            Pattern pattern = PatternFactory.CreateAbility(patternId, this);
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
        foreach (var pattern in Patterns)
        {
            pattern.UpdatePattern();
        }

        if (IsInVisible)
        {
            MoveBoss(rb);
        }

        if (transform.position.y <= stopPos.y)
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
        AudioSource.PlayOneShot(deathSound);
        HP = 0f;
        ui.AddScore(OfferedScore);
        Destroy(gameObject);
        //gm.StopGame();
        //ui.GameClear();
    }

    public void MoveBoss(Rigidbody2D rb)
    {
        rb.velocity = Vector3.down * MoveSpeed;
    }

    private void StopMove(Rigidbody2D rb)
    {
        rb.velocity = Vector3.zero;
        IsInVisible = false;
        isStop = true;
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
        if (collision.gameObject.tag == "PlayerBullet" && !IsInVisible)
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

        if (collision.gameObject.tag == "MinionBullet" && !IsInVisible)
        {
            var minionBullet = collision.gameObject.GetComponent<MinionBullet>();

            if (minionBullet != null)
            {
                OnDamage(minionBullet.Damage);
            }
            else
            {
                Debug.LogError("MinionBullet component not found on the collided object.");
            }
        }

        if (collision.gameObject.tag == "RazerBullet" && !IsInVisible)
        {
            var minionBullet = collision.gameObject.GetComponent<RazerBullet>();

            if (minionBullet != null)
            {
                OnDamage(minionBullet.Damage);
            }
            else
            {
                Debug.LogError("MinionBullet component not found on the collided object.");
            }
        }

        if (collision.gameObject.tag == "BoomEffect" && !IsInVisible)
        {
            var boomEffect = collision.gameObject.GetComponent<BoomEffect>();
            if (boomEffect != null)
            {
                OnDamage(boomEffect.Damage);
            }
            else
            {
                Debug.LogError("MinionBullet component not found on the collided object.");
            }
        }
    }
}

