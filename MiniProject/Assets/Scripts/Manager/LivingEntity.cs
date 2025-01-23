using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public event Action onDeath;
    public bool IsDead { get; protected set; }

    public virtual void OnDamage(float damage)
    {
    }

    public virtual void Die()
    {
        onDeath?.Invoke();
        IsDead = true;
    }
}
