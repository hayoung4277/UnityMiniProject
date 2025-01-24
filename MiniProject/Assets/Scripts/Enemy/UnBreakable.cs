using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnBreakable : LivingEntity
{
    public string NameId { get; protected set; }
    public float HP { get; protected set; }
    public float MoveSpeed { get; protected set; }
    public bool IsHitable { get; protected set; }
    public string SpawnWarningImage_1 { get; protected set; }
    public string SpawnWarningImage_2 { get; protected set; }
    public float SpawnWarningTime { get; protected set; }
    public string ObjectImageName { get; protected set; }
    public string DestroyEffectName { get; protected set; }
    public float DestroyEffectPlayTime { get; protected set; }
    public string DestroySoundName { get; protected set; }
    public float DestroySoundPlayTime { get; protected set; }

    public UnBreakableData Data { get; protected set; }
    public bool IsSpawn {  get; protected set; }

    public virtual void MoveUnBreakable(Rigidbody2D rb)
    {
        rb.velocity = Vector3.down * MoveSpeed;
        IsSpawn = true;
    }

    public override void OnDamage(float damage)
    {
        HP -= damage;
    }

    public virtual void Destroyed()
    {
        Destroy(gameObject);
    }

    public virtual void Initialized(UnBreakableData data)
    {
        NameId = data.NameId;
        HP = data.HP;
        MoveSpeed = data.MoveSpeed;
        IsHitable = data.IsHitable;
        SpawnWarningImage_1 = data.SpawnWarningImage_1;
        SpawnWarningImage_2 = data.SpawnWarningImage_2;
        SpawnWarningTime = data.SpawnWarningTime;
        ObjectImageName = data.ObjectImageName;
        DestroyEffectName = data.DestroyEffectName;
        DestroyEffectPlayTime = data.DestroyEffectPlayTime;
        DestroySoundName = data.DestroySoundName;
        DestroySoundPlayTime = data.DestroySoundPlayTime;
    }
}
