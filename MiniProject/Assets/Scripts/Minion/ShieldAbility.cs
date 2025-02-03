using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbility : Ability
{
    private Player player;
    private int count = 1;

    public ShieldAbility(Minion minion) : base(minion)
    {
        var findGo = GameObject.FindWithTag("Player");
        player = findGo.GetComponent<Player>();
    }

    public override void Activate()
    {
        if(count ==1 && player != null)
        {
            player.HPAdd();
        }
    }
}
