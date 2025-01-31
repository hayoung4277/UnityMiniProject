using System;
using UnityEngine;

public static class AbilityFactory
{
    public static Ability CreateAbility(int abilityId, Minion minion)
    {
        switch (abilityId)
        {
            case 1: return new SingleShotAbility(minion);
            case 2: return new FanShotAbility(minion);
            case 3: return new ShieldAbility(minion);
            case 4: return new ScoreMultiplierAbility(minion);
            default:
                Debug.LogWarning($"알 수 없는 Ability ID: {abilityId}");
                return null;
        }
    }
}