using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : LivingEntity
{
    public string ID {  get; set; }
    public float HP { get; set; }
    public string DeathEffectName {  get; set; }
    public float DeathEffectTime { get; set; }
    public string DeathSoundName { get; set; }
    public float DeathSoundTime { get; set; }
    public string AnimationName { get; set; }
    public float Score { get; set; }
    public float MonsterSpeed { get; set; }

    public virtual void MonsterDown(Rigidbody2D rb)
    {
        rb.velocity = Vector3.down * MonsterSpeed;
    }

    public virtual void Initialized(BulletData data)
    {
        
    }

    public override void OnDamage(float damage)
    {
        HP -= damage;
        if(HP <= 0)
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
