using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SingleShotAbility : Ability
{
    private Transform tf;
    private Coroutine burstCoroutine;
    private Minion minion;

    public SingleShotAbility(Minion minion) : base(minion)
    {
        this.minion = minion;
        tf = minion.transform;
        BulletName = minion.BulletName;
        FireRate = minion.FireRate;
    }

    public override void Activate(int rarity)
    {
        if (rarity >= 4)
        {
            if (burstCoroutine == null) // 점사 모드 중복 실행 방지
            {
                BurstFire(minion);
            }
            else
            {
                Fire();
            }
        }
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

    //public void StopBurstFire(MonoBehaviour caller)
    //{
    //    if (burstCoroutine != null)
    //    {
    //        caller.StopCoroutine(burstCoroutine);
    //        burstCoroutine = null;
    //    }
    //}

    private IEnumerator BurstFireCoroutine()
    {
        var bulletPrefab = Resources.Load<GameObject>($"Prefabs/Bullet/{BulletName}");
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet Prefab not Found.");
            yield break;
        }

        int burstCount = 3;
        float burstDelay = 0.08f;

        while (true)
        {
            for (int i = 0; i < burstCount; i++)
            {
                GameObject bullet = GameObject.Instantiate(bulletPrefab, tf.position, tf.rotation);
                yield return new WaitForSeconds(burstDelay);
            }

            yield return new WaitForSeconds(FireRate);
        }
    }

    public override void ApplyRarityScaling(int rarity)
    {
        
    }
}
