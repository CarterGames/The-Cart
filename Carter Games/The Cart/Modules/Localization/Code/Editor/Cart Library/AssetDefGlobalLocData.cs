using System;
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Modules.Localization.Data.Lookup;
using CarterGames.Cart.Modules.Settings;
using UnityEditor;

namespace CarterGames.Cart.Modules.Localization.Editor
{
    public class AssetDefGlobalLocData : IScriptableAssetDef<DataAssetGlobalLocalizationLookup>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static DataAssetGlobalLocalizationLookup cache;
        private static SerializedObject objCache;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IScriptableAssetDef Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        public Type AssetType => typeof(DataAssetGlobalLocalizationLookup);
        public string DataAssetFileName => "[Cart] [Localization] Global Localization Lookup Data Asset.asset";
        public string DataAssetFilter => $"t:{typeof(DataAssetGlobalLocalizationLookup).FullName}";
        public string DataAssetPath => $"{ScriptableRef.FullPathData}/Modules/{DataAssetFileName}";


        public DataAssetGlobalLocalizationLookup AssetRef => ScriptableRef.GetOrCreateAsset(this, ref cache);
        public SerializedObject ObjectRef => ScriptableRef.GetOrCreateAssetObject(this, ref objCache);


        public void TryCreate()
        {
            ScriptableRef.GetOrCreateAsset(this, ref cache);
        }

        public void OnCreated()
        {
            AssetRef.Initialize();
            EditorUtility.SetDirty(AssetRef);
            AssetDatabase.SaveAssets();
        }
    }
}