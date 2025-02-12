using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PierceShotAbility : Ability
{
    private Transform tf;
    private Minion minion;

    public PierceShotAbility(Minion minion) : base(minion)
    {
        this.minion = minion;
        tf = minion.transform;
        BulletName = minion.BulletName;
        FireRate = minion.FireRate;
        Rairity = minion.Rairity;
    }

    public override void Activate()
    {
        Fire(minion);
    }

    public override void Fire(MonoBehaviour callar)
    {
        callar.StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        while(true)
        {
            var bullet = Resources.Load<GameObject>($"Prefabs/Bullet/{BulletName}");
            if (bullet == null)
            {
                Debug.LogError("Bullet Prefab not Found.");
            }

            GameObject.Instantiate(bullet, tf.position, tf.rotation);

            yield return new WaitForSeconds(FireRate);
        }
    }

    public override void ApplyRarityScaling()
    {
        base.ApplyRarityScaling();
    }
}
