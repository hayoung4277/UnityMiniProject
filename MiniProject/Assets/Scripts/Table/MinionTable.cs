using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinionData
{
    public string Id { get; set; }
    public string NameId { get; set; }
    public int Rairity { get; set; }
    public float Duration { get; set; }
    public float FireRate { get; set; }
    public string AbilityIdsRaw { get; set; }  //CSV의 AbilityIds 원본 문자열
    public List<int> AbilityIds { get; private set; } = new List<int>();
    public string SpriteId { get; set; }
    public string BulletName { get; set; }

    public void ParseAbilityIds()
    {
        if (string.IsNullOrEmpty(AbilityIdsRaw))
        {
            Debug.LogWarning($"AbilityIds is empty for Minion {NameId}");
            return;
        }

        try
        {
            AbilityIds = AbilityIdsRaw.Split(',')
                                      .Select(id => int.Parse(id.Trim()))
                                      .ToList();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error parsing AbilityIds for Minion {NameId}: {e.Message}");
            AbilityIds.Clear();
        }
    }

    public override string ToString()
    {
        return $"{Id} / {NameId} / {Rairity} / {Duration} / {FireRate} / [{string.Join(",", AbilityIds)}] / {SpriteId} / {BulletName}";
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

            item.ParseAbilityIds(); //CSV에서 읽은 AbilityIdsRaw를 List<int>로 변환
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
