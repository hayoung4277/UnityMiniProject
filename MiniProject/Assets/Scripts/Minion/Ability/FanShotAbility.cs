using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanShotAbility : Ability
{
    private float fanBulletCount;
    private float fanSpreadAngle = 30f;
    private float fanSpeed = 3f;
    private Minion minion;

    private Transform tf;

    public FanShotAbility(Minion minion) : base(minion)
    {
        this.minion = minion;
        tf = minion.transform;
        BulletName = minion.BulletName;
        FireRate = minion.FireRate;
        Rairity = minion.Rairity;
        

        if (Rairity == 1)
        {
            fanBulletCount = 3;
        }
        else if (Rairity == 2)
        {
            fanBulletCount = 5;
        }
        else if (Rairity == 3)
        {
            fanBulletCount = 2;
        }
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
            BulletPrefab = Resources.Load<GameObject>($"Prefabs/Bullet/{BulletName}");

            if (BulletPrefab == null)
            {
                Debug.LogError($"Bullet Prefab '{BulletName}' not found!");
            }

            float startAngle = -fanSpreadAngle / 2f; // ���� ������ ����
            float angleStep = fanSpreadAngle / (fanBulletCount - 1);

            for (int i = 0; i < fanBulletCount; i++)
            {
                float angle = startAngle + (angleStep * i);

                // �̴Ͼ��� �ٶ󺸴� ������ �������� ȸ�� ����
                Quaternion rotation = Quaternion.Euler(0, 0, angle) * tf.rotation;
                Vector3 bulletDirection = rotation * Vector3.right; // ���� ȸ�� �������� �̵�

                GameObject bullet = GameObject.Instantiate(BulletPrefab, tf.position, rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * fanSpeed;
            }

            yield return new WaitForSeconds(FireRate);
        }
    }
}
