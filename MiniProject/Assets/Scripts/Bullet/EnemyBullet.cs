using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float Damage { get; private set; }

    public EnemyBulletData Data { get; private set; }

    public string dataId = "070001";

    private void Awake()
    {
        Data = DataTableManager.EnemyBulletTable.Get(dataId);
        if(Data == null )
        {
            Debug.LogError($"EnemyBullet data with ID '{dataId}' not found.");
            return;
        }

        if(Data != null)
        {
            Initialized(Data);
        }
        else
        {
            Debug.LogError($"EnemyBullet data with ID '{dataId}' not found.");
            return;
        }
    }

    private void Initialized(EnemyBulletData data)
    {
        Damage = data.Damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "DestroyBox")
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
