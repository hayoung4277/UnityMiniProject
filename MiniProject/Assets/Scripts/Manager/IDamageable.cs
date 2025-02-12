using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    float Damage { get; }

    void OnDamage(float damage);
}
