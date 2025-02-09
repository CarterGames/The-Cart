using System;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Modules.Settings;
using UnityEditor;

namespace CarterGames.Cart.Modules.Hierarchy.Editor
{
    public class AssetDefHierarchySettings : IScriptableAssetDef<DataAssetHierarchySettings>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static DataAssetHierarchySettings cache;
        private static SerializedObject objCache;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IScriptableAssetDef Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        public Type AssetType => typeof(DataAssetHierarchySettings);
        public string DataAssetFileName => "[Cart] [Hierarchy] Settings Data Asset.asset";
        public string DataAssetFilter => $"t:{typeof(DataAssetHierarchySettings).FullName} name={DataAssetFileName}";
        public string DataAssetPath => $"{ScriptableRef.FullPathData}/Modules/{DataAssetFileName}";


        public DataAssetHierarchySettings AssetRef => ScriptableRef.GetOrCreateAsset(this, ref cache);
        public SerializedObject ObjectRef => ScriptableRef.GetOrCreateAssetObject(this, ref objCache);


        public void TryCreate()
        {
            ScriptableRef.GetOrCreateAsset(this, ref cache);
        }


        /// <summary>
        /// Runs when the asset is created.
        /// </summary>
        public void OnCreated()
        {
            ObjectRef.Fp("excludeFromAssetIndex").boolValue = true;
            ObjectRef.ApplyModifiedProperties();
            ObjectRef.Update();
        }
    }
}