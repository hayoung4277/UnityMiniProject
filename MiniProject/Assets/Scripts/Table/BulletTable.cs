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

        if(textAsset == null)
        {
            Debug.LogError($"BulletTable file not found at path: {path}");
            return;
        }

        var list = LoadCSV<BulletData>(textAsset.text);
        table.Clear();

        foreach (var item in list)
        {
            if(table.ContainsKey(item.Id))
            {
                Debug.LogError($"Duplicate BulletData ID detected: {item.Id}. Skipping this entry");
                continue;
            }

            table.Add(item.Id, item);
        }

        Debug.Log($"Loaded BulletTable with {table.Count} entries from {filename}.");

    }

    public BulletData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            Debug.LogWarning($"BulletData with ID '{id}' not found in BulletTable.");
            return null;
        }

        return table[id];
    }
}
