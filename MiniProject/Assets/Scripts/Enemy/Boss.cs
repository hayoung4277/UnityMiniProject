using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : LivingEntity
{
    public float HP { get; protected set; }
    public float MoveSpeed { get; protected set; }
    public float OfferedScore { get; protected set; }
    public string BossSpriteName { get; protected set; }
    public string DeathEffectName { get; protected set; }
    public float DeathEffectPlayTime { get; protected set; }
    public string DeathSoundName { get; protected set; }
    public float DeathSoundPlayTime { get; protected set; }

    public PlayerBullet PlayerBullet { get; protected set; }

    public BossData Data { get; protected set; }

    public bool IsInVisible { get; protected set; }

    public override void OnDamage(float damage)
    {
        HP -= damage;
        if(HP <= 0)
        {
            Die();
        }
    }

    public virtual void MoveBoss(Rigidbody2D rb)
    {
        rb.velocity = Vector3.down * MoveSpeed;
    }

    public virtual void Initialized(BossData data)
    {
        HP = data.HP;
        MoveSpeed = data.MoveSpeed;
        OfferedScore = data.OfferedScore;
        BossSpriteName = data.BossSpriteName;
        DeathEffectName = data.DeathEffectName;
        DeathEffectPlayTime = data.DeathEffectPlayTime;
        DeathSoundName = data.DeathSoundName;
        DeathSoundPlayTime = data.DeathSoundPlayTime;

    }
}
