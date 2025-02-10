using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingShotAbility : Ability
{
    public HomingShotAbility(Minion minion) : base(minion)
    {
    }

    public override void Activate(int rarity)
    {
        base.Activate(rarity);
    }

    public override void Fire()
    {
        
    }

    public override void ApplyRarityScaling(int rarity)
    {
        base.ApplyRarityScaling(rarity);
    }
}
