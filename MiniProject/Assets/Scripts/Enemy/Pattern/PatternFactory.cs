using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PatternFactory
{
    public static Pattern CreatePattern(int patternId, Boss boss)
    {
        switch (patternId)
        {
            case 1: return new SingleShotPattern(boss); //싱글형(구현 완)
            case 2: return new FanShotPattern(boss); //부채꼴형(구현 완)
            case 3: return new FormationPattern(boss); //편대현(3발 구현 완) 테이블에 탄환숫자 적으면 탄환 숫자에 따른 패턴 완성 가능성 있음
            case 4: return new HalfCirclePattern(boss); //반원형(구현 완)
            case 5: return new RazerPattern(boss); //레이저형(구현 완)
            case 6: return new SummonMeteoPattern(boss); //메테오 부르기형(구현 완)
            case 7: return new AgroPattern(boss); //적 부르기형(구현 완)
            default:
                Debug.LogWarning($"알 수 없는 Ability ID: {patternId}");
                return null;
        }
    }
}
