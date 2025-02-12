using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazerBullet : Bullet
{
    public string dataId = "";

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
        Destroy(gameObject, 0.5f);
    }

    public override void Initialize(BulletData data)
    {
        base.Initialize(data);
    }
}
