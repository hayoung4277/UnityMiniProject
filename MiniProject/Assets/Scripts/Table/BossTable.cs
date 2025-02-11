using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BossData
{
    public string Id { get; set; }
    public float HP { get; set; }
    public float MoveSpeed { get; set; }
    public float OfferedScore { get; set; }
    public string BossSpriteName { get; set; }
    public string DeathEffectName { get; set; }
    public float DeathEffectPlayTime { get; set; }
    public string DeathSoundName { get; set; }
    public float DeathSoundPlayTime { get; set; }
    public string PatternIdsRaw { get; set; }

    public List<int> PatternIds { get; private set; } = new List<int>();

    public void ParsePatternIds()
    {
        if (string.IsNullOrEmpty(PatternIdsRaw))
        {
            Debug.LogWarning($"AbilityIds is empty for Minion {Id}");
            return;
        }

        try
        {
            PatternIds = PatternIdsRaw.Split(',')
                                      .Select(id => int.Parse(id.Trim()))
                                      .ToList();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error parsing AbilityIds for Minion {Id}: {e.Message}");
            PatternIds.Clear();
        }
    }

    public override string ToString()
    {
        return $"{Id} / {HP} / {MoveSpeed} / {OfferedScore} / {BossSpriteName} / {DeathEffectName} / " +
            $"{DeathEffectPlayTime} / {DeathSoundName} / {DeathSoundPlayTime}";
    }
}

public class BossTable : DataTable
{
    private static readonly Dictionary<string, BossData> table = new Dictionary<string, BossData>();

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);

        if (textAsset == null)
        {
            Debug.LogError($"BossTable file not found at path: {path}");
            return;
        }

        var list = LoadCSV<BossData>(textAsset.text);
        table.Clear();

        foreach (var item in list)
        {
            if (table.ContainsKey(item.Id))
            {
                Debug.LogError($"Duplicate BossData ID detected: {item.Id}. Skipping this entry");
                continue;
            }

            table.Add(item.Id, item);
        }

        Debug.Log($"Loaded BulletTable with {table.Count} entries from {filename}.");

    }

    public BossData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            Debug.LogWarning($"BossData with ID '{id}' not found in BossTable.");
            return null;
        }

        return table[id];
    }
}
