using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
    public string NameId { get; private set; }
    public int HP { get; private set; }
    public float CriticalChance { get; private set; }
    public float CriticalMultiplier { get; private set; }
    public float ScoreMultiplier { get; private set; }
    public string HitAnimEffectName { get; private set; }
    public string HitSoundName { get; private set; }
    public string AnimationName { get; private set; }
    public float FireRate { get; private set; }
    public float MoveSpeed { get; private set; }
    public string BulletName { get; private set; }

    public float SurviveTime => surviveTime;

    public PlayerData Data { get; private set; }
    public AudioSource DeathSound { get; private set; }
    public GameManager Gm { get; private set; }
    public UIManager UIManager { get; private set; }

    private Rigidbody2D rb;
    public string dataId = "01001";
    private float surviveTime;

    public bool isShield;
    private bool isHit;
    public int shieldCount;

    public bool isInvisible = false;

    private float shieldCoolTime;
    private float shieldCoolInterval = 30f;

    public GameObject spanwer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        DeathSound = GetComponent<AudioSource>();

        var findGm = GameObject.FindWithTag(GMCT.GM);
        Gm = findGm.GetComponent<GameManager>();

        var findUI = GameObject.FindWithTag(GMCT.UI);
        UIManager = findUI.GetComponent<UIManager>();
        surviveTime = 0f;

        // PlayerData 가져오기
        Data = DataTableManager.PlayerTable.Get(dataId);
        if (Data == null)
        {
            Debug.LogError($"Player data with ID '{dataId}' not found.");
            return;
        }

        // PlayerData로 초기화
        if (Data != null)
        {
            Initialized(Data);
        }
        else
        {
            Debug.LogError($"Player data with ID '{dataId}' not found.");
            return;
        }

        isShield = true;
        isHit = false;
        shieldCount = 0;
        shieldCoolTime = 0f;
    }
    private void Initialized(PlayerData data)
    {
        NameId = data.NameId;
        HP = data.HP;
        CriticalChance = data.CriticalChance;
        CriticalMultiplier = data.CriticalMultiplier;
        ScoreMultiplier = data.ScoreMultiplier;
        HitAnimEffectName = data.HitAnimEffectName;
        HitSoundName = data.HitSoundName;
        AnimationName = data.AnimationName;
        FireRate = data.FireRate;
        MoveSpeed = data.MoveSpeed;
        BulletName = data.BulletName;
    }

    private void Update()
    {
        surviveTime += Time.deltaTime;

        if (isShield == false)
        {
            shieldCoolTime += Time.deltaTime;

            if (shieldCoolTime >= shieldCoolInterval)
            {
                isShield = true;
                shieldCoolTime = 0f;
            }
        }
    }

    public override void Die()
    {
        base.Die();
        HP = 0;
        DeathSound.Play();
        Gm.StopGame();
        UIManager.GameOver();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NormalMonster" || collision.gameObject.tag == "UnBreakable")
        {
            if (isInvisible == false)
            {
                Die();
            }
        }

        if (collision.gameObject.tag == "BossBullet")
        {
            if (isInvisible == false && isShield == false)
            {
                OnPlayerDamage();
                isShield = true;
                isHit = true;
                Debug.Log("Shield CoolTime!");
            }

            if (isHit == true && isShield == true)
            {
                isShield = false;
                isHit = false;
                shieldCount = 0;
            }
        }

        if (collision.gameObject.tag == "Item")
        {
            var item = collision.GetComponent<IItem>();
            item?.UseItem(spanwer);
        }
    }

    public void OnPlayerDamage()
    {
        HP--;
        if (HP <= 0)
        {
            HP = 0;
            Die();
        }
    }

    public void IsShieldSetting()
    {
        shieldCount++;
        Debug.Log("OnShield!");
    }
}
