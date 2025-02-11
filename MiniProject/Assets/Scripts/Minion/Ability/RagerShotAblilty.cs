using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagerShotAblilty : Ability
{
    private Minion minion;
    private Transform tf;

    public RagerShotAblilty(Minion minion) : base(minion)
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
            Vector3 Pos = new Vector3(tf.position.x, tf.position.y + 4.5f, tf.position.z);

            var bulletPrefab = Resources.Load<GameObject>($"Prefabs/Bullet/{BulletName}");
            if (bulletPrefab == null)
            {
                Debug.LogError("Bullet Prefab not Found.");
            }

            var bullet = GameObject.Instantiate(bulletPrefab, Pos, tf.rotation);

            yield return new WaitForSeconds(FireRate);
        }
    }

    public override void ApplyRarityScaling()
    {
        base.ApplyRarityScaling();
    }
}
