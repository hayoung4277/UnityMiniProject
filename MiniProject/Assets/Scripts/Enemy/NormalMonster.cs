using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalMonster : LivingEntity
{
    public string NameId { get; protected set; }
    public float HP { get; protected set; }
    public string SpriteName { get; protected set; }
    public string DeathEffectName { get; protected set; }
    public float DeathEffectPlayTime { get; protected set; }
    public string DeathSoundName { get; protected set; }
    public float DeathSoundPlayTime { get; protected set; }
    public string AnimationName { get; protected set; }
    public float OfferedScore { get; protected set; }
    public float MonsterSpeed { get; protected set; }

    public Player Player { get; protected set; }
    public NormalMonsterData Data { get; protected set; }
    public AudioSource DeathSound { get; protected set; }

    public virtual void MonsterDown(Rigidbody2D rb)
    {
        rb.velocity = Vector3.down * MonsterSpeed;
    }

    public virtual void Initialized(NormalMonsterData data)
    {
        NameId = data.NameId;
        HP = data.HP;
        SpriteName = data.SpriteName;
        DeathEffectName = data.DeathEffectName;
        DeathEffectPlayTime = data.DeathEffectPlayTime;
        DeathSoundName = data.DeathSoundName;
        DeathSoundPlayTime = data.DeathSoundPlayTime;
        AnimationName = data.AnimationName;
        OfferedScore = data.OfferedScore;
        MonsterSpeed = data.MonsterSpeed;
    }

    public override void OnDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        base.Die();
        HP = 0;
    }
}
