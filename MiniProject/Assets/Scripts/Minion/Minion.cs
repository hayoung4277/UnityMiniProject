using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public string NameId { get; private set; }
    public int Rairity { get; private set; }
    public float Duration { get; private set; }
    public float FireRate { get; private set; }
    public string SpriteId { get; private set; }
    public string BulletName { get; private set; }

    public List<Ability> Abilities { get; private set; } = new List<Ability>();

    public MinionData Data { get; private set; }
    public int SpawnIndex { get; set; }
    public bool IsDead { get; private set; } = false;

    public string dataId = "10001";

    private float destroyTime = 0f;
    public event Action<Minion> OnMinionDestroyed;

    //public Transform player; // 플레이어 참조
    private Player player;
    private float followSpeed = 5f; // 이동 속도

    private float offsetX = 1.2f;  // 플레이어 기준으로 떨어질 거리
    private bool isRightSide = true;

    private void Awake()
    {
        Data = DataTableManager.MinionTable.Get(dataId);
        if (Data == null)
        {
            Debug.LogError($"Minion data with ID '{dataId}' not found.");
            return;
        }

        if (Data != null)
        {
            Initialized(Data);
        }
        else
        {
            Debug.LogError($"Minion data with ID '{dataId}' not found.");
            return;
        }

        var findPlayer = GameObject.FindWithTag("Player");
        player = findPlayer.GetComponent<Player>();
    }

    private void Start()
    {
        foreach (var ab in Abilities)
        {
            ab.Activate();
        }
    }

    public void Initialized(MinionData data)
    {
        NameId = data.NameId;
        Rairity = data.Rairity;
        Duration = data.Duration;
        FireRate = data.FireRate;
        SpriteId = data.SpriteId;
        BulletName = data.BulletName;

        Abilities.Clear(); //기존 능력 초기화

        foreach (var abilityId in data.AbilityIds)
        {
            Ability ability = AbilityFactory.CreateAbility(abilityId, this);
            if (ability != null)
            {
                ability.ApplyRarityScaling(Rairity); //레어리티에 따른 능력 강화 적용
                Abilities.Add(ability);
            }
            else
            {
                Debug.LogWarning($"Ability with ID {abilityId} not found for Minion {NameId}");
            }
        }
    }

    private void Update()
    {
        destroyTime += Time.deltaTime;

        foreach (var ability in Abilities)
        {
            ability.UpdateAbility();
        }

        if (destroyTime >= Duration)
        {
            Die();
        }

        FollowPlayer();
    }

    public void Die()
    {
        IsDead = true;
        OnMinionDestroyed?.Invoke(this); //이벤트 호출
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (!IsDead)
        {
            OnMinionDestroyed?.Invoke(this); //강제 삭제 대비
        }
    }

    private void FollowPlayer()
    {
        if (player == null)
            return;

        // 플레이어의 이동 방향 확인
        float direction = player.transform.position.x - transform.position.x;

        if (direction > 0)
        {
            isRightSide = false;
        }
        else
        {
            isRightSide = true;
        }

        // 목표 위치 설정 (플레이어 기준으로 일정 거리 유지)
        Vector3 targetPos = player.transform.position + new Vector3(isRightSide ? offsetX : -offsetX, 0, 0);

        transform.position = Vector3.MoveTowards(transform.position, targetPos, followSpeed * Time.deltaTime);
    }

    //private float ApplyRarityBonus(float baseValue, int rarity)
    //{
    //    float multiplier = 1f + (rarity * 0.1f); // 레어리티가 높을수록 증가 (예: 레어리티 3이면 1.3배)
    //    return baseValue * multiplier;
    //}
}
