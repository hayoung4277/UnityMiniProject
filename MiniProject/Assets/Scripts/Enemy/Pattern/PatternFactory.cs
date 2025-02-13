using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PatternFactory
{
    public static Pattern CreatePattern(int patternId, Boss boss)
    {
        switch (patternId)
        {
            case 1: return new SingleShotPattern(boss); //�̱���(���� ��)
            case 2: return new FanShotPattern(boss); //��ä����(���� ��)
            case 3: return new FormationPattern(boss); //�����(3�� ���� ��) ���̺� źȯ���� ������ źȯ ���ڿ� ���� ���� �ϼ� ���ɼ� ����
            case 4: return new HalfCirclePattern(boss); //�ݿ���(���� ��)
            case 5: return new RazerPattern(boss); //��������(���� ��)
            case 6: return new SummonMeteoPattern(boss); //���׿� �θ�����(���� ��)
            case 7: return new AgroPattern(boss); //�� �θ�����(���� ��)
            default:
                Debug.LogWarning($"�� �� ���� Ability ID: {patternId}");
                return null;
        }
    }
}
