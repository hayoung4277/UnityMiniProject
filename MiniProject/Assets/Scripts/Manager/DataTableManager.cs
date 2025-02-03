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

        var playerTable = new PlayerTable();
        playerTable.Load(DataTableIds.Player);
        tables.Add(DataTableIds.Player, playerTable);

        var normalMonsterTable = new NormalMonsterTable();
        normalMonsterTable.Load(DataTableIds.NormalMonster);
        tables.Add(DataTableIds.NormalMonster, normalMonsterTable);

        var unBreakableTable = new UnBreakableTable();
        unBreakableTable.Load(DataTableIds.UnBreakable);
        tables.Add(DataTableIds.UnBreakable, unBreakableTable);

        var bossTable = new BossTable();
        bossTable.Load(DataTableIds.Boss);
        tables.Add(DataTableIds.Boss, bossTable);

        //LoadStringTable();
    }

    private static void LoadStringTable()
    {
        var table = new StringTable();
        var stringTableId = DataTableIds.String[(int)Variables.currentLang];
        table.Load(stringTableId);
        tables[stringTableId] = table;
    }

    public static StringTable StringTable => Get<StringTable>(DataTableIds.String[(int)Variables.currentLang]);

    public static BulletTable BulletTable => Get<BulletTable>(DataTableIds.Bullet);

    public static PlayerTable PlayerTable => Get<PlayerTable>(DataTableIds.Player);

    public static NormalMonsterTable NormalMonsterTable => Get<NormalMonsterTable>(DataTableIds.NormalMonster);

    public static UnBreakableTable UnBreakableTable => Get<UnBreakableTable>(DataTableIds.UnBreakable);
    public static BossTable BossTable => Get<BossTable>(DataTableIds.Boss);

    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {
            Debug.LogError("테이블 없음");
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
