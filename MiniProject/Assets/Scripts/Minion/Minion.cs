using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public string NameId { get; private set; }
    public int Rairity { get; private set; }
    public float Duration { get; private set; }
    public int UseBulletId_1 { get; private set; }
    public int UseBulletId_2 { get; private set; }
    public float FireRate { get; private set; }
    public int AbilityId_1 { get; private set; }
    public int AbilityId_2 { get; private set; }
    public int SpriteId { get; private set; }

    public MinionData Data { get; private set; }

    public string dataId = "10001";

    private void Awake()
    {
        Data = DataTableManager.MinionTable.Get(dataId);
        if (Data == null)
        {
            Debug.LogError($"Minion data with ID '{dataId}' not found.");
            return;
        }

        if(Data != null)
        {
            Initialized(Data);
        }
        else
        {
            Debug.LogError($"Minion data with ID '{dataId}' not found.");
            return;
        }
    }

    public void Initialized(MinionData data)
    {
        NameId = data.NameId;
        Rairity = data.Rairity;
        Duration = data.Duration;
        UseBulletId_1 = data.UseBulletId_1;
        UseBulletId_2 = data.UseBulletId_2;
        FireRate = data.FireRate;
        AbilityId_1 = data.AbilityId_1;
        AbilityId_2 = data.AbilityId_2;
        SpriteId = data.SpriteId;
    }
}
