using System;
using UnityEngine;

public static class AbilityFactory
{
    public static Ability CreateAbility(int abilityId, Minion minion)
    {
        switch (abilityId)
        {
            case 1: return new SingleShotAbility(minion); //�̱���(���� ��)
            case 2: return new FanShotAbility(minion); //��ä����(���� ��)
            case 3: return new ShieldAbility(minion); //��ȣ����(���� ��)
            case 4: return new ScoreMultiplierAbility(minion); //���� ������(���� ��)
            case 5: return new FormationShotAbility(minion); //�����(���� ��)
            case 6: return new HomingShotAbility(minion); //������(���� ��)
            case 7: return new BoomShotAbility(minion); //������(���� ��)
            case 8: return new RagerShotAblilty(minion); //��������(���� ��)
            case 9: return new PierceShotAbility(minion); //������(���� ��)
            case 10: return new AgroAblility(minion); //���θ�����(���� ��)
            default:
                Debug.LogWarning($"�� �� ���� Ability ID: {abilityId}");
                return null;
        }
    }
}