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
        Rairity = minion.Rairity;
    }

    public override void Activate()
    {
        if (Rairity >= 4)
        {
            BurstFire(minion);
        }
        else
        {
            Fire(minion);
        }
    }

    public override void Fire(MonoBehaviour callar)
    {
        callar.StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        while(true)
        {
            var bulletPrefab = Resources.Load<GameObject>($"Prefabs/Bullet/{BulletName}");
            if (bulletPrefab == null)
            {
                Debug.LogError("Bullet Prefab not Found.");
            }

            GameObject bullet = GameObject.Instantiate(bulletPrefab, tf.position, tf.rotation);

            yield return new WaitForSeconds(FireRate);
        }
    }

    public override void UpdateAbility()
    {
        base.UpdateAbility();
    }

    public void BurstFire(MonoBehaviour caller)
    {
        caller.StartCoroutine(BurstFireCoroutine());
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

    public override void ApplyRarityScaling()
    {

    }
}
