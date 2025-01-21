using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.Linq;
using Unity.Collections;

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
        var list = LoadCSV<Data>(textAsset.text);
        dictionary.Clear();
        foreach (var key in list)
        {
            if (!dictionary.ContainsKey(key.Id))
            {
                dictionary.Add(key.Id, key.String);
            }
            else
            {
                Debug.LogError($"키 중복: {key.Id}");
            }
        }
    }

    public string Get(string key)
    {
        if (!dictionary.ContainsKey(key))
        {
            return "키없음.";
        }
        return dictionary[key];
    }
}