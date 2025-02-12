using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pattern
{
    public float FireRate { get; set; } //발사 간격
    public float TimeBetweenShots { get; set; } //탄환 간 간격
    public float BulletSpeed { get; set; }
    public float PatternStartTime { get; set; }

    protected Boss owner;

    public Pattern(Boss boss)
    {
        owner = boss;
    }

    public virtual void Activate() { }
    public virtual void Fire(MonoBehaviour callar) { }
    public virtual void UpdatePattern() { }
}
