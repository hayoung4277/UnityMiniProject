using System.Collections.Generic;
using UnityEngine;

public class StringTable : DataTable
{
    public class Data
    {
        public string Id { get; set; }
        public string String { get; set; }
    }

    private readonly Dictionary<string, string> dictionary = new Dictionary<string, string>();

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);

        if (textAsset == null)
        {
            Debug.LogError($"StringTable file not found at path: {path}");
            return;
        }

        var list = LoadCSV<Data>(textAsset.text);
        dictionary.Clear();

        foreach (var entry in list)
        {
            if (!dictionary.ContainsKey(entry.Id))
            {
                dictionary.Add(entry.Id, entry.String);
            }
            else
            {
                Debug.LogError($"Duplicate key detected in StringTable: {entry.Id}. Skipping this entry.");
            }
        }

        Debug.Log($"StringTable loaded successfully with {dictionary.Count} entries from {filename}.");
    }

    public string Get(string key)
    {
        if (dictionary.TryGetValue(key, out string value))
        {
            return value;
        }

        Debug.LogWarning($"Key '{key}' not found in StringTable. Returning default message.");
        return $"[Missing String: {key}]";
    }
}