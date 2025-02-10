using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationShotAbility : Ability
{
    private Transform tf;
    private int bulletCount;
    private Vector3 startPos;


    public FormationShotAbility(Minion minion) : base(minion)
    {
        tf = minion.transform;
        BulletName = minion.BulletName;
    }

    public override void Activate(int rarity)
    {
        if(rarity >= 4)
            {

        }
        else
        {
            Fire();
        }
    }

    public override void Fire()
    {
        startPos = new Vector3(tf.position.x + 0.5f, -2f, tf.position.z);

        var bulletPrefab = Resources.Load<GameObject>($"Prefabs/Bullet/{BulletName}");
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet Prefab not Found.");
            return;
        }

        for(int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, startPos, tf.rotation);
            bullet.transform.Translate(startPos.x, startPos.y, startPos.z);

            startPos.x += 0.1f;
        }
    }

    private void FormatFire()
    {

    }

    public override void ApplyRarityScaling(int rarity)
    {
        if(rarity == 1)
        {
            bulletCount = 3;
        }
        else if(rarity == 2)
        {
            bulletCount = 4;
        }
        else if(rarity == 3)
        {
            bulletCount = 2;
        }
    }
}
