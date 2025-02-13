using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public AudioSource AudioSource { get; private set; }
    public GameManager Gm { get; private set; }
    public UIManager UIManager { get; private set; }

    private Rigidbody2D rb;
    public string dataId = "01001";
    private float surviveTime;

    [Header("ShieldEffect")]
    public GameObject shieldEffect;
    private GameObject shieldEffectInstance;
    public bool isShield;
    public int shieldCount { get; set; }
    private int shieldMaxCount = 5;
    private float shieldCoolTime;
    private float shieldCoolInterval = 30f;
    private bool isShieldActive => shieldCount > 0;

    public bool isInvisible = false;

    private GameObject spawner;
    public BulletSpawner bulletSpawner;

    public AudioClip hitSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        AudioSource = GetComponent<AudioSource>();

        var findGm = GameObject.FindWithTag(GMCT.GM);
        Gm = findGm.GetComponent<GameManager>();

        var findUI = GameObject.FindWithTag(GMCT.UI);
        UIManager = findUI.GetComponent<UIManager>();

        spawner = gameObject.transform.Find("MinionSpawner").gameObject;
        surviveTime = 0f;

        // PlayerData 가져오기
        Data = DataTableManager.Instance.PlayerTable.Get(dataId);
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

        if (bulletSpawner == null)
        {
            bulletSpawner = GetComponentInChildren<BulletSpawner>(); // 자식에서 찾기
        }

        isShield = true;
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
        Gm.StopGame();
        UIManager.GameOver();

        RemoveShieldEffect();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NormalMonster" || collision.gameObject.tag == "UnBreakable")
        {
            if (isShieldActive)
            {
                ReduceShield();
                if (collision.gameObject.tag == "NormalMonster")
                {
                    var component = collision.gameObject.GetComponent<Enemy>();
                    component.DestroyObject();
                }
                else if(collision.gameObject.tag == "UnBreakable")
                {
                    var component = collision.gameObject.GetComponent<Meteor>();
                    component.DestroyObject();
                }
            }
            else if (!isInvisible)
            {
                Die();
            }
        }

        if (collision.gameObject.tag == "BossBullet")
        {
            if (isShieldActive)
            {
                ReduceShield();
            }
            else if (!isInvisible)
            {
                OnPlayerDamage();
                AudioSource.PlayOneShot(hitSound);
            }
        }

        if (collision.gameObject.tag == "Item")
        {
            var item = collision.GetComponent<IItem>();
            item?.UseItem(spawner);
        }
    }

    public void OnPlayerDamage()
    {
        HP--;
        Destroy(shieldEffectInstance);
        if (HP <= 0)
        {
            HP = 0;
            Die();
        }
    }

    public void IsShieldSetting()
    {
        if (shieldCount >= shieldMaxCount)
        {
            return;
        }

        shieldCount++;

        if (shieldEffectInstance == null)
        {
            shieldEffectInstance = Instantiate(shieldEffect, transform.position, Quaternion.identity);
            shieldEffectInstance.transform.SetParent(transform);
            shieldEffectInstance.transform.localPosition = Vector3.zero;
        }
    }

    public void ReduceShield()
    {
        if (shieldCount <= 0) return;

        shieldCount--;

        if (shieldCount <= 0)
        {
            RemoveShieldEffect();
        }
    }

    private void RemoveShieldEffect()
    {
        if (shieldEffectInstance != null)
        {
            Destroy(shieldEffectInstance);
            shieldEffectInstance = null;
        }
    }
}
