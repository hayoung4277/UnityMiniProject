using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Data = DataTableManager.BulletTable.Get("030001");
        if (Data != null)
        {
            Initialize(Data);
        }
        else
        {
            Debug.LogError("Bullet data with ID '030001' not found.");
        }
    }

    private void Start()
    {
        Fire(rb);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"{DamageCoeff}, {BulletSpeed}");
        }
    }

    public override void Fire(Rigidbody2D rb)
    {
        base.Fire(rb);
    }

    public override void Initialize(BulletData data)
    {
        base.Initialize(data);

        //DamageCoeff = data.DamageCoeff;
        //BulletSpeed = data.BulletSpeed;
        //BulletEffectName = data.BulletEffectName;
        //CanDestroyed = data.CanDestroyed;
        //CanGuided = data.CanGuided;
        //CanPierce = data.CanPierce;
        //PierceCount = data.PierceCount;
    }
}
