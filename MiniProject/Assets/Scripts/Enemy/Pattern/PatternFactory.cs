using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PatternFactory
{
    public static Pattern CreateAbility(int abilityId, Boss boss)
    {
        switch (abilityId)
        {
            case 1: return new SingleShotPattern(boss); //�̱���(���� ��)
            case 2: return new FanShotPattern(boss); //��ä����(���� ��)
            default:
                Debug.LogWarning($"�� �� ���� Ability ID: {abilityId}");
                return null;
        }
    }
}
