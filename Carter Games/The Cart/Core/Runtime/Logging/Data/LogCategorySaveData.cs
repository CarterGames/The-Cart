using System;
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Cart.Core.Logs
{
    [Serializable]
    public class LogCategorySaveData
    {
        [SerializeField] private List<SerializableKeyValuePair<string, bool>> data = new List<SerializableKeyValuePair<string, bool>>();
        
        public List<SerializableKeyValuePair<string, bool>> Data => data;

        
        public LogCategorySaveData() {}

        public LogCategorySaveData(SerializableDictionary<string, bool> value)
        {
            data = new List<SerializableKeyValuePair<string, bool>>();

            foreach (var entry in value)
            {
                data.Add(entry);
            }
        }
    }
}