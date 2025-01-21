using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData
{
    public string Id { get; set; }
    public float DamageCoeff { get; set; }
    public float BulletSpeed { get; set; }
    public string BulletEffectName { get; set; }
    public bool CanDestroyed { get; set; }
    public bool CanGuided { get; set; }
    public bool CanPierce { get; set; }
    public int PierceCount { get; set; }

    public override string ToString()
    {
        return $"{Id} / {DamageCoeff} / {BulletSpeed} / {BulletEffectName} / {CanDestroyed} / " +
            $"{CanDestroyed} / {CanGuided} / {CanPierce} / {PierceCount}";
    }

    //public string StringName
    //{
    //    get
    //    {
    //        return DataTableManager.StringTable.Get(Name);
    //    }
    //}

    //public string StringDesc
    //{
    //    get
    //    {
    //        return DataTableManager.StringTable.Get(Desc);
    //    }
    //}
}

public class BulletTable : DataTable
{
    private static readonly Dictionary<string, BulletData> table = new Dictionary<string, BulletData>();

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<BulletData>(textAsset.text);
        table.Clear();
        foreach (var item in list)
        {
            table.Add(item.Id, item);
        }
    }

    public BulletData Get(string id)
    {
        if (!table.ContainsKey(id))
            return null;
        return table[id];
    }
}
