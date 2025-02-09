using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string Id { get; set; }
    public string NameId { get; set; }
    public int HP { get; set; }
    public float CriticalChance { get; set; }
    public float CriticalMultiplier { get; set; }
    public float ScoreMultiplier { get; set; }
    public string HitAnimEffectName { get; set; }
    public string HitSoundName { get; set; }
    public string AnimationName { get; set; }
    public float FireRate { get; set; }
    public float MoveSpeed { get; set; }
    public string BulletName { get; set; }

    public override string ToString()
    {
        return $"{Id} / {NameId} / {HP} / {CriticalChance} / {CriticalMultiplier} / " +
            $"{ScoreMultiplier} / {HitAnimEffectName} / {HitSoundName} / {AnimationName}" +
            $"{FireRate} / {MoveSpeed} / {BulletName}";
    }
}

public class PlayerTable : DataTable
{
    private static readonly Dictionary<string, PlayerData> table = new Dictionary<string, PlayerData>();

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);

        if (textAsset == null)
        {
            Debug.LogError($"PlayerTable file not found at path: {path}");
            return;
        }

        var list = LoadCSV<PlayerData>(textAsset.text);
        table.Clear();

        foreach (var item in list)
        {
            if (table.ContainsKey(item.Id))
            {
                Debug.LogError($"Duplicate PlayerData ID detected: {item.Id}. Skipping this entry");
                continue;
            }

            table.Add(item.Id, item);
        }

        Debug.Log($"Loaded playerTable with {table.Count} entries from {filename}.");

    }

    public PlayerData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            Debug.LogWarning($"PlayerData with ID '{id}' not found in PlayerData.");
            return null;
        }

        return table[id];
    }
}
