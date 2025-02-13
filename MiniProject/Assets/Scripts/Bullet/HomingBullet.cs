using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Bullet
{
    public string dataId = "";

    private float rotateSpeed = 200f; // ȸ�� �ӵ�
    private float lifeTime = 5f; // źȯ�� ������� �ð�

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
        Destroy(gameObject, lifeTime); // ���� �ð� �� �ڵ� ����
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
            if (target == null) return; // ���� ������ ���� ����
        }

        // ��ǥ���� ���� ȸ��
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        transform.Rotate(0, 0, -rotateAmount * rotateSpeed * Time.deltaTime);

        // ������ �̵�
        transform.Translate(Vector3.up * BulletSpeed * Time.deltaTime);
    }

    // ���� ����� �� ã��
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
