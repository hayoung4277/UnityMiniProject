using System.Collections.Generic;
using UnityEngine;
using static EnemyBulletData;

public class DataTableManager : MonoBehaviour
{
    private static DataTableManager instance;

    public static DataTableManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("DataTableManager");
                instance = obj.AddComponent<DataTableManager>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    private readonly Dictionary<string, DataTable> tables = new Dictionary<string, DataTable>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadTables();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadTables()
    {
        AddTable(new BulletTable(), DataTableIds.Bullet);
        AddTable(new PlayerTable(), DataTableIds.Player);
        AddTable(new NormalMonsterTable(), DataTableIds.NormalMonster);
        AddTable(new UnBreakableTable(), DataTableIds.UnBreakable);
        AddTable(new BossTable(), DataTableIds.Boss);
        AddTable(new MinionTable(), DataTableIds.Minion);
        AddTable(new EnemyBulletTable(), DataTableIds.EnemyBullet);

        //LoadStringTable();
    }

    private void AddTable(DataTable table, string id)
    {
        table.Load(id);
        tables[id] = table;
    }

    //private void LoadStringTable()
    //{
    //    var table = new StringTable();
    //    var stringTableId = DataTableIds.String[(int)Variables.currentLang];
    //    table.Load(stringTableId);
    //    tables[stringTableId] = table;
    //}

    public BulletTable BulletTable => Get<BulletTable>(DataTableIds.Bullet);
    public PlayerTable PlayerTable => Get<PlayerTable>(DataTableIds.Player);
    public NormalMonsterTable NormalMonsterTable => Get<NormalMonsterTable>(DataTableIds.NormalMonster);
    public UnBreakableTable UnBreakableTable => Get<UnBreakableTable>(DataTableIds.UnBreakable);
    public BossTable BossTable => Get<BossTable>(DataTableIds.Boss);
    public MinionTable MinionTable => Get<MinionTable>(DataTableIds.Minion);
    public EnemyBulletTable EnemyBulletTable => Get<EnemyBulletTable>(DataTableIds.EnemyBullet);
    public StringTable StringTable => Get<StringTable>(DataTableIds.String[(int)Variables.currentLang]);

    public T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {
            Debug.LogError($"테이블 {id} 없음");
            return null;
        }
        return tables[id] as T;
    }
}

//    public void ReloadStringTable(Languages newLanguage)
//    {
//        Variables.currentLang = newLanguage;
//        LoadStringTable();
//        Debug.Log($"String Table reloaded for language: {newLanguage}");
//    }
//}