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

        LoadStringTable();
    }
//#if UNITY_EDITOR
//        foreach (var id in DataTableIds.String)
//        {
//            var table = new StringTable();
//            table.Load(id);
//            tables.Add(id, table);
//        }
//#else
//    var table = new StringTable();
//    var stringTableId = DataTableIds.String[(int)Variables.currentLang];
//    table.Load(stringTableId);
//            tables.Add(stringTableId, table);
//#endif
//    }

    private static void LoadStringTable()
    {
        var table = new StringTable();
        var stringTableId = DataTableIds.String[(int)Variables.currentLang];
        table.Load(stringTableId);
        tables[stringTableId] = table;
    }

    public static StringTable StringTable => Get<StringTable>(DataTableIds.String[(int)Variables.currentLang]);
    //{
    //    get
    //    {
    //        return Get<StringTable>(DataTableIds.String[(int)Variables.currentLang]);
    //    }
    //}

    public static BulletTable BulletTable => Get<BulletTable>(DataTableIds.Bullet);
    //{
    //    get
    //    {
    //        return Get<BulletTable>(DataTableIds.Bullet);
    //    }
    //}


    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {
            Debug.LogError("���̺� ����");
            return null;
        }
        return tables[id] as T;
    }

    public static void ReloadStringTable(Languages newLanguage)
    {
        Variables.currentLang = newLanguage;
        LoadStringTable();
        Debug.Log($"String Table reloaded for language: {newLanguage}");
    }
}
