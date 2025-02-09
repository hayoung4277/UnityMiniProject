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

    private float offsetX = 1.2f;  // �÷��̾� �������� ������ �Ÿ�
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

        Abilities.Clear(); //���� �ɷ� �ʱ�ȭ

        foreach (var abilityId in data.AbilityIds)
        {
            Ability ability = AbilityFactory.CreateAbility(abilityId, this);
            if (ability != null)
            {
                ability.ApplyRarityScaling(Rairity); //���Ƽ�� ���� �ɷ� ��ȭ ����
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

        // �÷��̾��� �̵� ���� Ȯ��
        float direction = player.transform.position.x - transform.position.x;

        if (direction > 0)
        {
            isRightSide = false;
        }
        else
        {
            isRightSide = true;
        }

        // ��ǥ ��ġ ���� (�÷��̾� �������� ���� �Ÿ� ����)
        Vector3 targetPos = player.transform.position + new Vector3(isRightSide ? offsetX : -offsetX, 0, 0);

        transform.position = Vector3.MoveTowards(transform.position, targetPos, followSpeed * Time.deltaTime);
    }

    //private float ApplyRarityBonus(float baseValue, int rarity)
    //{
    //    float multiplier = 1f + (rarity * 0.1f); // ���Ƽ�� �������� ���� (��: ���Ƽ 3�̸� 1.3��)
    //    return baseValue * multiplier;
    //}
}
