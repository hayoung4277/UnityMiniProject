using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SingleShotAbility : Ability
{
    private Transform tf;
    private Coroutine burstCoroutine;
    private Minion minion;
    //private float bulletSpeed = 3f;

    public SingleShotAbility(Minion minion) : base(minion)
    {
        this.minion = minion;
        tf = minion.transform;
        BulletName = minion.BulletName;
        FireRate = minion.FireRate;
    }

    public override void Fire()
    {
        var bulletPrefab = Resources.Load<GameObject>($"Prefabs/Bullet/{BulletName}");
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

    public void BurstFire(MonoBehaviour caller)
    {
        if (burstCoroutine == null)
        {
            burstCoroutine = caller.StartCoroutine(BurstFireCoroutine());
        }
    }

    public void StopBurstFire(MonoBehaviour caller)
    {
        if (burstCoroutine != null)
        {
            caller.StopCoroutine(burstCoroutine);
            burstCoroutine = null;
        }
    }

    private IEnumerator BurstFireCoroutine()
    {
        var bulletPrefab = Resources.Load<GameObject>($"Prefabs/Bullet/{BulletName}");
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet Prefab not Found.");
            yield break;
        }

        int burstCount = 3;
        float burstDelay = 0.1f;
        float burstCooldown = 0.5f;

        while (true)
        {
            for (int i = 0; i < burstCount; i++)
            {
                GameObject bullet = GameObject.Instantiate(bulletPrefab, tf.position, tf.rotation);
                yield return new WaitForSeconds(burstDelay);
            }

            yield return new WaitForSeconds(burstCooldown);
        }
    }

    public override void ApplyRarityScaling(int rarity)
    {
        if (rarity >= 4)
        {
            BurstFire(minion);
        }
        else
        {
            Fire();
        }
    }
}
