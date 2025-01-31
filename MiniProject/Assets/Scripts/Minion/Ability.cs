using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability
{
    protected Minion owner;

    public Ability(Minion minion)
    {
        owner = minion;
    }

    public abstract void Activate();
    public virtual void UpdateAbility() { }
}
