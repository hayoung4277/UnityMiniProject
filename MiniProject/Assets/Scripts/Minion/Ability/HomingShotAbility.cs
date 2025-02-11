using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingShotAbility : Ability
{
    private Transform tf;
    private Minion minion;
    private float damage = 100f;

    public HomingShotAbility(Minion minion) : base(minion)
    {
        this.minion = minion;
        tf = minion.transform;
        BulletName = minion.BulletName;
        FireRate = minion.FireRate;
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
            var bulletPrefab = Resources.Load<GameObject>($"Prefabs/Bullet/{BulletName}");
            if (bulletPrefab == null)
            {
                Debug.LogError("Bullet Prefab not Found.");
            }

            GameObject bullet = GameObject.Instantiate(bulletPrefab, tf.position, tf.rotation);

            Vector3 endPos = new Vector3(tf.position.x, tf.position.y + 10f, tf.position.z);

            Collider[] colliders = Physics.OverlapCapsule(tf.position, endPos, 1.5f);

            foreach(Collider hit in colliders)
            {
                IDamageable damageable = hit.GetComponent<IDamageable>();
                if(damageable != null)
                {
                    damageable.OnDamage(damage);
                }
            }

            yield return new WaitForSeconds(FireRate);
        }
    }

    public override void ApplyRarityScaling()
    {
        base.ApplyRarityScaling();
    }
}
