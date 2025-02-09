using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability
{
    public string BulletName { get; set; }
    public float FireRate { get; set; }

    protected Minion owner;
    protected float fireRate;       // 발사 속도 (초당 발사 횟수)
    private float fireTimer;

    public Ability(Minion minion)
    {
        owner = minion;
        fireRate = minion.FireRate;
        fireTimer = 0f;
    }

    public virtual void Activate(int rarity) { }
    public virtual void Fire() { }
    public virtual void UpdateAbility()
    {
        fireTimer += Time.deltaTime;

        if(fireTimer >= fireRate)
        {
            Fire();
            fireTimer = 0f;
        }
    }

    public virtual void ApplyRarityScaling(int rarity) { }
}
