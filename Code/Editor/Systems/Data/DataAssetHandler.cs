using System.Collections.Generic;
using CarterGames.Common.Editor;
using CarterGames.Common.General;
using CarterGames.Common.Management.Editor;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace CarterGames.Common.Data.Editor
{
    public sealed class DataAssetHandler : IPreprocessBuildWithReport
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static DataAssetIndex cache;
        private const string AssetFilter = "t:dataasset";

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static DataAssetIndex Index => CacheRef.GetOrAssign(ref cache, ScriptableRef.DataAssetIndex);
        
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
            if (ScriptableRef.HasAllAssets) return;
        }
        

        /// <summary>
        /// Updates the index with all the asset scriptable objects in the project.
        /// </summary>
        [MenuItem("Tools/Carter Games/Common/Data/Update Index", priority = 50)]
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
            
            EditorGUI.BeginChangeCheck();
            Index.SetLookup(foundAssets);
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(Index);
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}