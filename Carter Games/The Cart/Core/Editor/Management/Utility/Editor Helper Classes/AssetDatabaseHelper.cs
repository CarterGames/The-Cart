using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Management.Editor
{
    public static class AssetDatabaseHelper
    {
        public static bool FileIsInProject<T>(string path)
        {
            Debug.Log($"{typeof(T)} | {AssetDatabase.LoadAssetAtPath(path, typeof(T))}");
            return AssetDatabase.LoadAssetAtPath(path, typeof(T)) != null;
        }
    }
}