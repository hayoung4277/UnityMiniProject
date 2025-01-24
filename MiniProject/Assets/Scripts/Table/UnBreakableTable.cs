using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnBreakableData
{
    public string Id { get; set; }
    public string NameId { get; set; }
    public float HP { get; set; }
    public float MoveSpeed { get; set; }
    public bool IsHitable { get; set; }
    public string SpawnWarningImage_1 { get; set; }
    public string SpawnWarningImage_2 { get; set; }
    public float SpawnWarningTime { get; set; }
    public string ObjectImageName { get; set; }
    public string DestroyEffectName { get; set; }
    public float DestroyEffectPlayTime { get; set; }
    public string DestroySoundName { get; set; }
    public float DestroySoundPlayTime { get; set; }

    public override string ToString()
    {
        return $"{Id} / {NameId} / {HP} / {MoveSpeed} / {IsHitable} / {SpawnWarningImage_1} / {SpawnWarningImage_2} / {SpawnWarningTime} / " +
            $"{ObjectImageName} / {DestroyEffectName} / {DestroyEffectPlayTime} / {DestroySoundName} / {DestroySoundPlayTime}";
    }
}

public class UnBreakableTable : DataTable
{
    private static readonly Dictionary<string, UnBreakableData> table = new Dictionary<string, UnBreakableData>();

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);

        if (textAsset == null)
        {
            Debug.LogError($"UnBreakableTable file not found at path: {path}");
            return;
        }

        var list = LoadCSV<UnBreakableData>(textAsset.text);
        table.Clear();

        foreach (var item in list)
        {
            if (table.ContainsKey(item.Id))
            {
                Debug.LogError($"Duplicate UnBreakableData ID detected: {item.Id}. Skipping this entry");
                continue;
            }

            table.Add(item.Id, item);
        }

        Debug.Log($"Loaded UnBreakableTable with {table.Count} entries from {filename}.");

    }

    public UnBreakableData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            Debug.LogWarning($"UnBreakableData with ID '{id}' not found in UnBreakableTable.");
            return null;
        }

        return table[id];
    }
}
