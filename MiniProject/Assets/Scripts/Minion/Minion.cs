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

    //public Transform player; // �÷��̾� ����
    private Player player;
    private float followSpeed = 5f; // �̵� �ӵ�

    public float offsetX = 0.7f;
    public float offsetY = 0.3f;// �÷��̾� �������� ������ �Ÿ�
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

        Abilities.Clear(); //���� �ɷ� �ʱ�ȭ

        foreach (var abilityId in data.AbilityIds)
        {
            Ability ability = AbilityFactory.CreateAbility(abilityId, this);
            if (ability != null)
            {
                ability.ApplyRarityScaling(); //���Ƽ�� ���� �ɷ� ��ȭ ����
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
        OnMinionDestroyed?.Invoke(this); //�̺�Ʈ ȣ��
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (!IsDead)
        {
            OnMinionDestroyed?.Invoke(this); //���� ���� ���
        }
    }

    private void FollowPlayer()
    {
        if (player == null)
            return;

        // ��ǥ ��ġ ���� (�÷��̾� �������� ���� �Ÿ� ����)
        Vector3 targetPos = player.transform.position + new Vector3(isRightSide ? offsetX : -offsetX, isUpSide ? offsetY : -offsetY, 0);

        // �ε巯�� �̵� ó��
        transform.position = Vector3.MoveTowards(transform.position, targetPos, followSpeed * Time.deltaTime);
    }

    private void StartBlink()
    {
        StartCoroutine(BlinkBeforeDisappear());
    }

    private IEnumerator BlinkBeforeDisappear()
    {
        float duration = 5f;  // 5�� ���� �����̱�
        float blinkSpeed = 0.2f; // �����̴� �ӵ�
        float time = 0f;


        while (time < duration)
        {
            float alpha = (Mathf.Sin(Time.time * 10) + 1) / 2; // 0 ~ 1 ���� �ݺ�
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;

            time += blinkSpeed;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}