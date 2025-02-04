using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonsterData
{
    public string Id { get; set; }
    public string NameId { get; set; }
    public float HP { get; set; }
    public string SpriteName { get; set; }
    public string DeathEffectName { get; set; }
    public float DeathEffectPlayTime { get; set; }
    public string DeathSoundName { get; set; }
    public float DeathSoundPlayTime { get; set; }
    public string AnimationName { get; set; }
    public float OfferedScore { get; set; }
    public float DropRate { get; set; }

    public override string ToString()
    {
        return $"{Id} / {NameId} / {HP} / {SpriteName} / {DeathEffectName} / {DeathEffectPlayTime} / " +
            $"{DeathSoundName} / {DeathSoundPlayTime} / {AnimationName} / {OfferedScore} / {DropRate}";
    }
}

public class NormalMonsterTable : DataTable
{
    private static readonly Dictionary<string, NormalMonsterData> table = new Dictionary<string, NormalMonsterData>();

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);

        if (textAsset == null)
        {
            Debug.LogError($"NormalMonsterTable file not found at path: {path}");
            return;
        }

        var list = LoadCSV<NormalMonsterData>(textAsset.text);
        table.Clear();

        foreach (var item in list)
        {
            if (table.ContainsKey(item.Id))
            {
                Debug.LogError($"Duplicate NormalMonsterData ID detected: {item.Id}. Skipping this entry");
                continue;
            }

            table.Add(item.Id, item);
        }

        Debug.Log($"Loaded NormalMonsterData with {table.Count} entries from {filename}.");

    }

    public NormalMonsterData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            Debug.LogWarning($"NormalMonsterData with ID '{id}' not found in NormalMonsterTable.");
            return null;
        }

        return table[id];
    }
}
