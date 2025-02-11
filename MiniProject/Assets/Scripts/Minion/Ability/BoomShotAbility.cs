using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomShotAbility : Ability
{
    private Transform tf;
    private GameObject boomPrefab;
    private BoomBullet boomBullet;
    private Minion minion;

    private float explosionRadius =3f; // ���� �ݰ�
    private float damage;

    public BoomShotAbility(Minion minion) : base(minion)
    {
        this.minion = minion;
        tf = minion.transform;
        BulletName = minion.BulletName;
        FireRate = minion.FireRate;
        Rairity = minion.Rairity;
        boomPrefab = Resources.Load<GameObject>($"Prefabs/Effect/ExplosionBlue");
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
            var bulletPrefab = Resources.Load<GameObject>($"Prefabs/Bullet/{BulletName}");
            if (bulletPrefab == null)
            {
                Debug.LogError("Bullet Prefab not Found.");
            }

            GameObject bullet = GameObject.Instantiate(bulletPrefab, tf.position, tf.rotation);
            yield return null; // �� ������ ��� (Awake ���� ����)

            BoomBullet boomBullet = bullet.GetComponent<BoomBullet>();
            if (boomBullet != null)
            {
                boomBullet.AbilityInitialize(this); // ���⼭ ���� �Ѱ���!
            }
            else
            {
                Debug.LogError("BoomBullet component not found on bullet!");
            }

            yield return new WaitForSeconds(FireRate);
        }
    }

    public override void UpdateAbility()
    {
        base.UpdateAbility();
    }

    public override void ApplyRarityScaling()
    {
        if(Rairity == 1)
        {
            damage = 100f;
        }
        else if(Rairity == 2)
        {
            damage = 200f;
        }
        else if(Rairity == 3)
        {
            damage = 300f;
        }
        else if(Rairity == 4)
        {
            damage = 500f;
        }
    }

    public void Boom(Vector3 explosionPosition)
    {
        if (boomPrefab != null)
        {
            GameObject.Instantiate(boomPrefab, explosionPosition, Quaternion.identity);
        }

        // ���� ���� �ִ� ��� �ݶ��̴� Ž��
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider hit in colliders)
        {
            // �������� �� ����� �ִ��� Ȯ��
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.OnDamage(Damage);
            }
        }
    }
}
