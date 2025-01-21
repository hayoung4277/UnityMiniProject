using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //DamageCoeff = Data;
        //BulletSpeed = Data.BulletSpeed;
        //BulletEffectName = Data.BulletEffectName;
        //CanDestroyed = Data.CanDestroyed;
        //CanGuided = Data.CanGuided;
        //CanPierce = Data.CanPierce;
        //PierceCount = Data.PierceCount;
    }

    private void Start()
    {
        Fire();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"{DamageCoeff}, {BulletSpeed}");
        }
    }

    public override void Fire()
    {
        rb.velocity = transform.up * BulletSpeed;
    }
}
