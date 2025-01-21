using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float DamageCoeff { get; protected set; } //���ط� ���

    public float BulletSpeed { get; protected set; } //ź��

    public string BulletEffectName { get; protected set; } //����ü ����Ʈ

    public bool CanDestroyed { get; protected set; } //�ı� ���� ����

    public bool CanGuided { get; protected set; } //���� ���� ����

    public bool CanPierce {  get; protected set; } //���� ���� ����

    public int PierceCount { get; protected set; } //�ִ� ����Ƚ�� ����

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
