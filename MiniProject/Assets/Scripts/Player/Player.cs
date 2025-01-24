using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
    public string NameId {  get; private set; }
    public int HP { get; private set; }
    public float CriticalChance { get; private set; }
    public float CriticalMultiplier { get; private set; }
    public float ScoreMultiplier { get; private set; }
    public string HitAnimEffectName { get; private set; }
    public string HitSoundName { get; private set; }
    public string AnimationName { get; private set; }

    public PlayerData Data { get; private set; }
    public AudioSource DeathSound { get; private set; }
    public GameManager Gm { get; private set; }

    private Rigidbody2D rb;
    public string dataId = "01001";

    public bool isInvisible = false;

    private GameOver gameOver;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        DeathSound = GetComponent<AudioSource>();

        var findGm = GameObject.FindWithTag(GMCT.GM);
        Gm = findGm.GetComponent<GameManager>();

        var findOver = GameObject.FindWithTag("GameOver");
        gameOver = findOver.GetComponent<GameOver>();

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
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetInvisible();
        }
    }

    public override void Die()
    {
        base.Die();
        HP = 0;
        DeathSound.Play();
        Gm.StopGame();
        Destroy(gameObject);
        gameOver.enabled = true;
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

    private void SetInvisible()
    {
        isInvisible = !isInvisible;
        Debug.Log($"SetInVisible {isInvisible}");
    }

    public override void OnDamage(float damage)
    {
        HP--;
    }
}
