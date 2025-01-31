using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionData
{
    public string Id { get; set; }
    public string NameId { get; set; }
    public int Rairity { get; set; }
    public float Duration { get; set; }
    public int UseBulletId_1 { get; set; }
    public int UseBulletId_2 { get; set; }
    public float FireRate { get; set; }
    public int AbilityId_1 { get; set; }
    public int AbilityId_2 { get; set; }
    public int SpriteId { get; set; }

    public override string ToString()
    {
        return $"{Id} / {NameId} / {Rairity} / {Duration} / {UseBulletId_1} / " +
            $"{UseBulletId_2} / {FireRate} / {AbilityId_1} / {AbilityId_2} / {SpriteId}";
    }
}

public class MinionTable : DataTable
{
    private static readonly Dictionary<string, MinionData> table = new Dictionary<string, MinionData>();

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);

        if (textAsset == null)
        {
            Debug.LogError($"MinionTable file not found at path: {path}");
            return;
        }

        var list = LoadCSV<MinionData>(textAsset.text);
        table.Clear();

        foreach (var item in list)
        {
            if (table.ContainsKey(item.Id))
            {
                Debug.LogError($"Duplicate MinionTable ID detected: {item.Id}. Skipping this entry");
                continue;
            }

            table.Add(item.Id, item);
        }

        Debug.Log($"Loaded MinionData with {table.Count} entries from {filename}.");

    }

    public MinionData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            Debug.LogWarning($"MinionData with ID '{id}' not found in MinionTable.");
            return null;
        }

        return table[id];
    }
}
