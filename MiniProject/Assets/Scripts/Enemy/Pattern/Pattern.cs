using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pattern
{
    public float FireRate { get; set; } //�߻� ����
    public float TimeBetweenShots { get; set; } //źȯ �� ����
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
