using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Bullet
{
    public string dataId = "";

    private float rotateSpeed = 200f; // 회전 속도
    private float lifeTime = 5f; // 탄환이 사라지는 시간

    private Transform target;

    private void Awake()
    {
        Data = DataTableManager.Instance.BulletTable.Get(dataId);

        if (Data != null)
        {
            Initialize(Data);
        }
        else
        {
            Debug.LogError($"Bullet data with ID '{dataId}' not found.");
        }
    }

    private void Start()
    {
        target = FindClosestEnemy();
        Destroy(gameObject, lifeTime); // 일정 시간 후 자동 삭제
    }

    public override void Initialize(BulletData data)
    {
        base.Initialize(data);
    }

    private void Update()
    {
        if (target == null)
        {
            target = FindClosestEnemy();
            if (target == null) return; // 적이 없으면 유도 중지
        }

        // 목표물을 향해 회전
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        transform.Rotate(0, 0, -rotateAmount * rotateSpeed * Time.deltaTime);

        // 앞으로 이동
        transform.Translate(Vector3.up * BulletSpeed * Time.deltaTime);
    }

    // 가장 가까운 적 찾기
    private Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("NormalMonster");
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }

        return closest;
    }
}
