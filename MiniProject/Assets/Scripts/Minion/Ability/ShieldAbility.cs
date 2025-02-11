using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbility : Ability
{
    private Player player;

    public ShieldAbility(Minion minion) : base(minion)
    {
        var findGo = GameObject.FindWithTag("Player");
        player = findGo.GetComponent<Player>();
        Rairity = minion.Rairity;
    }

    public override void Activate()
    {
        if(player != null && player.shieldCount == 0)
        {
            player.IsShieldSetting();
        }
    }
}
