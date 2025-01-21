using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float DamageCoeff { get; protected set; } //피해량 계수

    public float BulletSpeed { get; protected set; } //탄속

    public string BulletEffectName { get; protected set; } //투사체 이펙트

    public bool CanDestroyed { get; protected set; } //파괴 가능 여부

    public bool CanGuided { get; protected set; } //유도 가능 여부

    public bool CanPierce {  get; protected set; } //관통 가능 여부

    public int PierceCount { get; protected set; } //최대 관통횟수 여부

    public BulletTable Data { get; protected set; }
    public BulletData data;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        BulletSpeed = 3f;
        Data = DataTableManager.BulletTable;
    }

    private void Start()
    {
        Fire();
    }

    public virtual void Fire()
    {
        rb.velocity = transform.up * BulletSpeed;
        //rb.AddForce(transform.up * BulletSpeed);
    }
}
