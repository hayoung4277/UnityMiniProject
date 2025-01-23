using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
    public string NameId {  get; protected set; }
    public int HP { get; protected set; }
    public float CriticalChance { get; protected set; }
    public float CriticalMultiplier { get; protected set; }
    public float ScoreMultiplier { get; protected set; }
    public string HitAnimEffectName { get; protected set; }
    public string HitSoundName { get; protected set; }
    public string AnimationName { get; protected set; }

    public PlayerData Data { get; protected set; }
    public AudioSource DeathSound { get; protected set; }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
    }

    public virtual void Initialized(PlayerData data)
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
}
