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

            Collider[] colliders = Physics.OverlapSphere(tf.position, 1f);
            //Collider[] colliders1 = Physics.OverlapCapsule(tf.position, endPos);

            yield return new WaitForSeconds(FireRate);
        }
    }

    public override void ApplyRarityScaling()
    {
        base.ApplyRarityScaling();
    }
}
