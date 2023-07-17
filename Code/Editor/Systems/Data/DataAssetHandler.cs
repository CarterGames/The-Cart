using System.Collections.Generic;
using Scarlet.Editor;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Scarlet.Data.Editor
{
    public sealed class DataAssetHandler : IPreprocessBuildWithReport
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static DataAssetIndex cache;
        private const string AssetFilter = "t:dataasset";
        private const string IndexFilter = "t:dataassetindex";

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static DataAssetIndex Index
        {
            get
            {
                if (cache != null) return cache;
                TryMakeIndex();
                return cache;
            }
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IPreprocessBuildWithReport Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The order this script is processed in, in this case its the default.
        /// </summary>
        public int callbackOrder => 0;


        /// <summary>
        /// Runs before a build is executed.
        /// </summary>
        /// <param name="report">The report about the build (I don't need it, but its a param for the method).</param>
        public void OnPreprocessBuild(BuildReport report)
        {
            UpdateIndex();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Initializes the event subscription needed for this to work in editor.
        /// </summary>
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            EditorApplication.update -= OnEditorUpdate;
            EditorApplication.update += OnEditorUpdate;
        }


        /// <summary>
        /// Runs when the editor has updated.
        /// </summary>
        private static void OnEditorUpdate()
        {
            // If the user is about to enter play-mode, update the index, otherwise leave it be. 
            if (!EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isPlaying) return;
            TryMakeIndex();
            UpdateIndex();
        }


        /// <summary>
        /// Tries to make the asset if it doesn't already exist in the resources folder.
        /// </summary>
        private static void TryMakeIndex()
        {
            if (cache != null) return;
            
            cache = (DataAssetIndex) FileEditorUtil.GetFileViaFilter(typeof(DataAssetIndex), IndexFilter);

            if (cache == null)
            {
                cache = FileEditorUtil.CreateScriptableObject<DataAssetIndex>("Assets/Resources/Scarlet Library/Data Asset Index.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
        

        /// <summary>
        /// Updates the index with all the asset scriptable objects in the project.
        /// </summary>
        [MenuItem("Tools/Scarlet Library/Data/Update Index", priority = 50)]
        private static void UpdateIndex()
        {
            var foundAssets = new List<DataAsset>();
            var asset = AssetDatabase.FindAssets(AssetFilter, null);

            if (asset == null || asset.Length <= 0) return;

            foreach (var assetInstance in asset)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(assetInstance);
                var assetObj = (DataAsset)AssetDatabase.LoadAssetAtPath(assetPath, typeof(DataAsset));

                // Doesn't include editor only or the index itself.
                if (assetObj == null) continue;
                if (assetObj.GetType() == typeof(DataAssetIndex)) continue;
                foundAssets.Add((DataAsset)AssetDatabase.LoadAssetAtPath(assetPath, typeof(DataAsset)));
            }
            
            Index.SetLookup(foundAssets);
            
            EditorUtility.SetDirty(Index);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}