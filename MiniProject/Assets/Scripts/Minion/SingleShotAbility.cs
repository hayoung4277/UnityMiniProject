using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SingleShotAbility : Ability
{
    private Transform tf;
    //private float bulletSpeed = 3f;

    public SingleShotAbility(Minion minion) : base(minion)
    {
        tf = minion.transform;
    }

    public override void Fire()
    {
        var bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet/PlayerBullet");
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet Prefab not Found.");
            return;
        }

        GameObject bullet = GameObject.Instantiate(bulletPrefab, tf.position, tf.rotation);
    }

    public override void UpdateAbility()
    {
        base.UpdateAbility(); 
    }

    public override void ApplyRarityScaling(int rarity)
    {
        base.ApplyRarityScaling(rarity);
    }
}
