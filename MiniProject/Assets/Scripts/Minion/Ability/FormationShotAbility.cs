using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationShotAbility : Ability
{
    private Transform tf;
    private int bulletCount;
    private Vector3 startPos;
    private Minion minion;
    private float bulletOffsetX;

    public FormationShotAbility(Minion minion) : base(minion)
    {
        this.minion = minion;
        tf = minion.transform;
        BulletName = minion.BulletName;
        Rairity = minion.Rairity;
        FireRate = minion.FireRate;
        BulletPrefab = Resources.Load<GameObject>($"Prefabs/Bullet/{BulletName}");
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
            // 보조기체 앞쪽에서 탄환 발사 (앞쪽 0.5 위치)
            startPos = tf.position + tf.forward * 0.8f;

            if (BulletPrefab == null)
            {
                Debug.LogError("Bullet Prefab not Found.");
                yield break;
            }

            // 탄환이 중앙을 기준으로 좌우 대칭되도록 시작 위치 조정
            float totalWidth = (bulletCount - 1) * bulletOffsetX;  // 전체 폭 계산
            Vector3 firstBulletPos = startPos - tf.right * (totalWidth / 2); // 중앙 기준 왼쪽으로 이동

            for (int i = 0; i < bulletCount; i++)
            {
                GameObject bullet = GameObject.Instantiate(BulletPrefab, firstBulletPos, tf.rotation);

                // 다음 탄환 위치를 오른쪽으로 이동
                firstBulletPos += tf.right * bulletOffsetX;
            }

            yield return new WaitForSeconds(FireRate);
        }
    }

    public override void ApplyRarityScaling()
    {
        if (Rairity == 1)
        {
            bulletCount = 2;
            bulletOffsetX = 0.5f;
        }
        else if (Rairity == 2)
        {
            bulletCount = 3;
            bulletOffsetX = 0.2f;
        }
        else if (Rairity == 3)
        {
            bulletCount = 5;
            bulletOffsetX = 0.2f;
        }
    }
}
