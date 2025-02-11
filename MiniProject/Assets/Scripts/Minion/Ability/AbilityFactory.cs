using System;
using UnityEngine;

public static class AbilityFactory
{
    public static Ability CreateAbility(int abilityId, Minion minion)
    {
        switch (abilityId)
        {
            case 1: return new SingleShotAbility(minion); //싱글형(구현 완)
            case 2: return new FanShotAbility(minion); //부채꼴형(구현 완)
            case 3: return new ShieldAbility(minion); //보호막형(구현 완)
            case 4: return new ScoreMultiplierAbility(minion); //점수 제공형(구현 완)
            case 5: return new FormationShotAbility(minion); //편대형(구현 완)
            case 6: return new HomingShotAbility(minion); //유도형(구현 중)
            case 7: return new BoomShotAbility(minion); //폭발형(구현 전)
            case 8: return new RagerShotAblilty(minion); //레이저형(구현 전)
            case 9: return new PierceShotAbility(minion); //관통형(구현 전)
            case 10: return new AgroAblility(minion); //적부르기형(구현 전)
            default:
                Debug.LogWarning($"알 수 없는 Ability ID: {abilityId}");
                return null;
        }
    }
}