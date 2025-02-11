using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pattern
{
    protected Boss owner;

    public Pattern(Boss boss)
    {
        owner = boss;
    }

    public virtual void Activate() { }
    public virtual void Fire(MonoBehaviour callar) { }
    public virtual void UpdatePattern() { }
}
