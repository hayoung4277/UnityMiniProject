using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanShotAbility : Ability
{
    private float fanBulletCount = 3;
    private float fanSpreadAngle = 30f;
    private float fanSpeed = 3f;

    private Transform tf;

    public FanShotAbility(Minion minion) : base(minion)
    {
        tf = minion.transform;
        BulletName = minion.BulletName;
    }

    public override void Fire()
    {
        var bulletPrefab = Resources.Load<GameObject>($"Prefabs/Bullet/{BulletName}");

        float startAngle = fanSpreadAngle - 30f;
        float angleStep = fanSpreadAngle / (fanBulletCount - 1);

        float angle = startAngle;

        for (int i = 0; i < fanBulletCount; i++)
        {
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 bulletDirection = new Vector3(dirX, dirY, 0f);

            GameObject bullet = GameObject.Instantiate(bulletPrefab, tf.position, tf.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * fanSpeed;

            angle += angleStep;
        }
    }

    public override void UpdateAbility()
    {
        base.UpdateAbility();
    }

    public override void ApplyRarityScaling(int rarity)
    {
    }
}
