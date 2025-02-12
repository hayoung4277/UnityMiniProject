using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PatternFactory
{
    public static Pattern CreateAbility(int abilityId, Boss boss)
    {
        switch (abilityId)
        {
            case 1: return new SingleShotPattern(boss); //싱글형(구현 완)
            default:
                Debug.LogWarning($"알 수 없는 Ability ID: {abilityId}");
                return null;
        }
    }
}
