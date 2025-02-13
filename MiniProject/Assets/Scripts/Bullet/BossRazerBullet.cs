using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRazerBullet : MonoBehaviour
{
    public float Damage { get; private set; }
    public float Speed { get; private set; }
    public string EffectName { get; private set; }
    public bool CanGuided { get; private set; }

    public EnemyBulletData Data { get; private set; }

    public string dataId = "";

    private void Awake()
    {
        Data = DataTableManager.Instance.EnemyBulletTable.Get(dataId);

        if (Data != null)
        {
            Initialize(Data);
        }
        else
        {
            Debug.LogError($"Bullet data with ID '{dataId}' not found.");
        }
    }

    private void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    private void Initialize(EnemyBulletData data)
    {
        Damage = data.Damage;
        Speed = data.Speed;
        EffectName = data.EffectName;
        CanGuided = data.CanGuided;
    }
}
