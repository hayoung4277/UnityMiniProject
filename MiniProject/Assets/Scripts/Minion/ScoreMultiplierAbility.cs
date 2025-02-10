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
    }

    public override void Activate(int rarity)
    {
        OnScoreMuliplier();
        Debug.Log($"Score Add: {score}");
    }

    public void OnScoreMuliplier()
    {
        um.AddScore(score);
    }

    public override void ApplyRarityScaling(int rarity)
    {
        if(rarity == 1 || rarity == 2)
        {
            score = 5000;
        }
        else if(rarity == 3)
        {
            score = 10000;
        }
        else if(rarity == 4)
        {
            score = 50000;
        }
    }
}
