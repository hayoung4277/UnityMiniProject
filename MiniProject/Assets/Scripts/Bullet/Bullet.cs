using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage { get; protected set; } //피해량
    public float BulletSpeed { get; protected set; } //탄속
    public string BulletEffectName { get; protected set; } //투사체 이펙트
    public bool CanDestroyed { get; protected set; } //파괴 가능 여부
    public bool CanGuided { get; protected set; } //유도 가능 여부
    public bool CanPierce {  get; protected set; } //관통 가능 여부
    public int PierceCount { get; protected set; } //최대 관통횟수 여부
    public BulletData Data { get; protected set; }
    public AudioSource Sound { get; protected set; }

    public virtual void Fire(Rigidbody2D rb)
    {
        rb.velocity = transform.up * BulletSpeed;
    }

    public virtual void Initialize(BulletData data)
    {
        Damage = data.Damage;
        BulletSpeed = data.BulletSpeed;
        BulletEffectName = data.BulletEffectName;
        CanDestroyed = data.CanDestroyed;
        CanGuided = data.CanGuided;
        CanPierce = data.CanPierce;
        PierceCount = data.PierceCount;
    }
}
