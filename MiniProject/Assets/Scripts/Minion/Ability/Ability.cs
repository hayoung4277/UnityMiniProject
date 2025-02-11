using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability
{
    public string BulletName { get; set; }
    public float FireRate { get; set; }
    public float Damage { get; set; }
    public int Rairity { get; set; }

    protected Minion owner;
    protected float fireRate;       // �߻� �ӵ� (�ʴ� �߻� Ƚ��)
    private float fireTimer;

    public Ability(Minion minion)
    {
        owner = minion;
        fireRate = minion.FireRate;
        fireTimer = 0f;
    }

    public virtual void Activate() { }
    public virtual void Fire(MonoBehaviour callar) { }
    public virtual void UpdateAbility()
    {
        fireTimer += Time.deltaTime;

        if(fireTimer >= fireRate)
        {
            //Activate();
            fireTimer = 0f;
        }
    }

    public virtual void ApplyRarityScaling() { }
}
