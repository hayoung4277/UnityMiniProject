using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationShotAbility : Ability
{
    private Transform tf;
    private int bulletCount;
    private Vector3 startPos;
    private Minion minion;

    public FormationShotAbility(Minion minion) : base(minion)
    {
        this.minion = minion;
        tf = minion.transform;
        BulletName = minion.BulletName;
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
        while (true)
        {
            startPos = new Vector3(tf.position.x + 0.5f, -2f, tf.position.z);

            var bulletPrefab = Resources.Load<GameObject>($"Prefabs/Bullet/{BulletName}");
            if (bulletPrefab == null)
            {
                Debug.LogError("Bullet Prefab not Found.");
            }

            for (int i = 0; i < bulletCount; i++)
            {
                GameObject bullet = GameObject.Instantiate(bulletPrefab, startPos, tf.rotation);
                bullet.transform.Translate(startPos.x, startPos.y, startPos.z);

                startPos.x += 0.1f;
            }

            yield return new WaitForSeconds(FireRate);
        }
    }

    private void FormatFire()
    {

    }

    public override void ApplyRarityScaling()
    {
        if (Rairity == 1)
        {
            bulletCount = 3;
        }
        else if (Rairity == 2)
        {
            bulletCount = 4;
        }
        else if (Rairity == 3)
        {
            bulletCount = 2;
        }
    }
}
