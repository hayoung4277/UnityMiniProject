using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplierAbility : Ability
{
    private UIManager um;
    private float score;

    public ScoreMultiplierAbility(Minion minion) : base(minion)
    {
        var findUi = GameObject.FindWithTag(GMCT.UI);
        um = findUi.GetComponent<UIManager>();
        Rairity = minion.Rairity;
    }

    public override void Activate()
    {
        OnScoreMuliplier();
        Debug.Log($"Score Add: {score}");
    }

    public void OnScoreMuliplier()
    {
        um.AddScore(score);
    }

    public override void ApplyRarityScaling()
    {
        if(Rairity == 1 || Rairity == 2)
        {
            score = 5000;
        }
        else if(Rairity == 3)
        {
            score = 10000;
        }
        else if(Rairity == 4)
        {
            score = 50000;
        }
    }
}
