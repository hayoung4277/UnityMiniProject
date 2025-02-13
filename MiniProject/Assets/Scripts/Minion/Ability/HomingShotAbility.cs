using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingShotAbility : Ability
{
    private Transform tf;
    private Minion minion;

    public HomingShotAbility(Minion minion) : base(minion)
    {
        this.minion = minion;
        tf = minion.transform;
        BulletName = minion.BulletName;
        FireRate = minion.FireRate;
        BulletPrefab = Resources.Load<GameObject>($"Prefabs/Bullet/{BulletName}");
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
            if (BulletPrefab == null)
            {
                Debug.LogError("Bullet Prefab not Found.");
            }

            GameObject bullet = GameObject.Instantiate(BulletPrefab, tf.position, tf.rotation);

            yield return new WaitForSeconds(FireRate);
        }
    }

    public override void ApplyRarityScaling()
    {
        base.ApplyRarityScaling();
    }
}
