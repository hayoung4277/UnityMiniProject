using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEffect : Bullet
{
    public string dataId;

    private void Awake()
    {
        Data = DataTableManager.BulletTable.Get(dataId);

        if (Data != null)
        {
            Initialize(Data);
        }
        else
        {
            Debug.LogError($"Bullet data with ID '{dataId}' not found.");
        }
    }

    public override void Initialize(BulletData data)
    {
        base.Initialize(data);
    }
}
