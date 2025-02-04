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
    public float SurviveTime => surviveTime;

    public PlayerData Data { get; private set; }
    public AudioSource DeathSound { get; private set; }
    public GameManager Gm { get; private set; }
    public UIManager UIManager { get; private set; }

    private Rigidbody2D rb;
    public string dataId = "01001";
    private float surviveTime;

    public bool isInvisible = false;

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
    }

    private void Update()
    {
        surviveTime += Time.deltaTime;
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
    }

    public override void OnDamage(float damage)
    {
        HP--;
    }
}
