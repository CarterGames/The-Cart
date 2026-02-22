#if CARTERGAMES_CART_CRATE_MULTISCENE && UNITY_EDITOR

using System;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Management.Editor;
using UnityEditor;

namespace CarterGames.Cart.Crates.MultiScene.Editor
{
    public sealed class MultiSceneSettingsAssetDef : IScriptableAssetDef<MultiSceneSettings>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static MultiSceneSettings cache;
        private static SerializedObject objCache;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IScriptableAssetDef Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        
        public Type AssetType => typeof(MultiSceneSettings);
        public string DataAssetFileName => "[Cart] [MultiScene] Settings Data Asset.asset";
        public string DataAssetFilter => $"t:{typeof(MultiSceneSettings).FullName} name={DataAssetFileName}";
        public string DataAssetPath => $"{ScriptableRef.FullPathData}/Crates/{DataAssetFileName}";
        public MultiSceneSettings AssetRef => ScriptableRef.GetOrCreateAsset(this, ref cache);
        public SerializedObject ObjectRef => ScriptableRef.GetOrCreateAssetObject(this, ref objCache);

        
        public void TryCreate()
        {
            ScriptableRef.GetOrCreateAsset(this, ref cache);
        }

        public void OnCreated()
        { }
    }
}

#endif