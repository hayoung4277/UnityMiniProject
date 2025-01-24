using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage { get; protected set; } //���ط�
    public float BulletSpeed { get; protected set; } //ź��
    public string BulletEffectName { get; protected set; } //����ü ����Ʈ
    public bool CanDestroyed { get; protected set; } //�ı� ���� ����
    public bool CanGuided { get; protected set; } //���� ���� ����
    public bool CanPierce {  get; protected set; } //���� ���� ����
    public int PierceCount { get; protected set; } //�ִ� ����Ƚ�� ����
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
