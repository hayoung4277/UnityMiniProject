using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletData
{
    public string Id { get; set; }
    public float Damage { get; set; }

    public override string ToString()
    {
        return $"{Id} / {Damage}";
    }

    public class EnemyBulletTable : DataTable
    {
        private static readonly Dictionary<string, EnemyBulletData> table = new Dictionary<string, EnemyBulletData>();

        public override void Load(string filename)
        {
            var path = string.Format(FormatPath, filename);
            var textAsset = Resources.Load<TextAsset>(path);

            if (textAsset == null)
            {
                Debug.LogError($"EnemyBulletTable file not found at path: {path}");
                return;
            }

            var list = LoadCSV<EnemyBulletData>(textAsset.text);
            table.Clear();

            foreach (var item in list)
            {
                if (table.ContainsKey(item.Id))
                {
                    Debug.LogError($"Duplicate EnemyBulletData ID detected: {item.Id}. Skipping this entry");
                    continue;
                }

                table.Add(item.Id, item);
            }

            Debug.Log($"Loaded EnemyBulletTable with {table.Count} entries from {filename}.");

        }

        public EnemyBulletData Get(string id)
        {
            if (!table.ContainsKey(id))
            {
                Debug.LogWarning($"EnemyBulletData with ID '{id}' not found in EnemyBulletTable.");
                return null;
            }

            return table[id];
        }
    }
}
