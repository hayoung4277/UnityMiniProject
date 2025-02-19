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

    public float offsetX = 0.7f;
    public float offsetY = 0.3f;// 플레이어 기준으로 떨어질 거리
    private bool isRightSide = true;
    private bool isUpSide = true;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        Data = DataTableManager.Instance.MinionTable.Get(dataId);
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

        Vector3 playerPos = player.transform.position;
        isRightSide = transform.position.x > playerPos.x;
        isUpSide = transform.position.y > playerPos.y;
    }

    private void Start()
    {
        for (int i = 0; i < Abilities.Count; i++)
        {
            Abilities[i].Activate();
        }

        spriteRenderer = GetComponent<SpriteRenderer>();

        //fireCoroutine = StartCoroutine(FireRoutine());
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
                ability.ApplyRarityScaling(); //레어리티에 따른 능력 강화 적용
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

        if (destroyTime >= 25f)
        {
            StartBlink();
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

        // 목표 위치 설정 (플레이어 기준으로 일정 거리 유지)
        Vector3 targetPos = player.transform.position + new Vector3(isRightSide ? offsetX : -offsetX, isUpSide ? offsetY : -offsetY, 0);

        // 부드러운 이동 처리
        transform.position = Vector3.MoveTowards(transform.position, targetPos, followSpeed * Time.deltaTime);
    }

    private void StartBlink()
    {
        StartCoroutine(BlinkBeforeDisappear());
    }

    private IEnumerator BlinkBeforeDisappear()
    {
        float duration = 5f;  // 5초 동안 깜빡이기
        float blinkSpeed = 0.2f; // 깜빡이는 속도
        float time = 0f;


        while (time < duration)
        {
            float alpha = (Mathf.Sin(Time.time * 10) + 1) / 2; // 0 ~ 1 사이 반복
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;

            time += blinkSpeed;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}