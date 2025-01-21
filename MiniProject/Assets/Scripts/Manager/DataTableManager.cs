using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> tables = new Dictionary<string, DataTable>();

    static DataTableManager()
    {
        var bulletTable = new BulletTable();
        bulletTable.Load(DataTableIds.Bullet);
        tables.Add(DataTableIds.Bullet, bulletTable);

#if UNITY_EDITOR
        foreach (var id in DataTableIds.String)
        {
            var table = new StringTable();
            table.Load(id);
            tables.Add(id, table);
        }
#else
    var table = new StringTable();
    var stringTableId = DataTableIds.String[(int)Variables.currentLang];
    table.Load(stringTableId);
            tables.Add(stringTableId, table);
#endif
    }

    public static StringTable StringTable
    {
        get
        {
            return Get<StringTable>(DataTableIds.String[(int)Variables.currentLang]);
        }
    }

    public static BulletTable BulletTable
    {
        get
        {
            return Get<BulletTable>(DataTableIds.Bullet);
        }
    }


    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {
            Debug.LogError("테이블 없음");
            return null;
        }
        return tables[id] as T;
    }
}
