using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunny : Player
{
    private Rigidbody2D rb;
    private static readonly string dataId = "01001";

    private bool isInvisible = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        DeathSound = GetComponent<AudioSource>();

        var findGm = GameObject.FindWithTag(GMCT.GM);
        Gm = findGm.GetComponent<GameManager>();

        // PlayerData 가져오기
        Data = DataTableManager.PlayerTable.Get(dataId);
        if (Data == null)
        {
            Debug.LogError($"Player data with ID '{dataId}' not found.");
            return;
        }

        // PlayerData와 BulletData로 초기화
        if (Data != null)
        {
            Initialized(Data);
        }
        else
        {
            Debug.LogError($"Player data with ID '{dataId}' not found.");
            return;
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetInvisible();
        }
    }

    public override void Initialized(PlayerData data)
    {
        base.Initialized(data);
    }

    public override void Die()
    {
        base.Die();
        HP = 0;
        DeathSound.Play();
        Gm.StopGame();
        Destroy(gameObject);
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NormalMonster" || collision.gameObject.tag == "UnBreakable")
        {
            if (isInvisible == false)
            {
                Die();
            }
        }
    }

    private void SetInvisible()
    {
        isInvisible = !isInvisible;
        Debug.Log($"SetInVisible {isInvisible}");
    }
}