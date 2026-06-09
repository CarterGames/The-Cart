using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace CarterGames.Cart.Editor
{
    public static class EditorUIToolkitStyleHandler
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static Dictionary<string, StyleSheet> CacheLookup = new Dictionary<string, StyleSheet>();
        private static List<StyleSheet> CacheDefines = null;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public static StyleSheet GetStyleSheet(string fileName)
        {
            if (!CacheLookup.IsEmptyOrNull()) return CacheLookup[fileName];

            CacheLookup = new Dictionary<string, StyleSheet>();
            CacheDefines = AssetDatabaseHelper.GetAllInstancesInProject<StyleSheet>().ToList();
  
            foreach (var entry in CacheDefines)
            {
                if (CacheLookup.ContainsKey(entry.name.Replace(".uss", string.Empty))) continue;
                CacheLookup.Add(entry.name.Replace(".uss", string.Empty), entry);
            }

            return CacheLookup[fileName];
        }
    }
}