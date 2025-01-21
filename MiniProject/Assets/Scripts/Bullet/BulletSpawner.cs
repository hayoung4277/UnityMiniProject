using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;

    public float fireRate = 0.8f;
    private float currentFireTime;

    private void Awake()
    {
        currentFireTime = 0f;
    }

    private void Update()
    {
        currentFireTime += Time.deltaTime;

        if (currentFireTime > fireRate)
        {
            currentFireTime = 0f;

            GameObject bullet = Instantiate(prefab, parent.position, parent.rotation);

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Fire();
        }
    }
}
