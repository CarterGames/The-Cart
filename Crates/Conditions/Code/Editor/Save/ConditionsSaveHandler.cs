#if CARTERGAMES_CART_CRATE_CONDITIONS && UNITY_EDITOR

using UnityEditor;

namespace CarterGames.Cart.Crates.Conditions.Editor
{
    public class ConditionsSaveHandler : UnityEditor.AssetModificationProcessor
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Get/Set if the editor is dirty (editor perf).
        /// </summary>
        public static bool EditorIsDirty { get; private set; }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Auto-runs on compile.
        /// </summary>
        [InitializeOnLoadMethod]
        private static void InitializeEditorSaveManager()
        {
            AssemblyReloadEvents.beforeAssemblyReload -= OnPreAssemblyCompile;
            AssemblyReloadEvents.beforeAssemblyReload += OnPreAssemblyCompile;

            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            
            EditorApplication.quitting -= OnEditorQuitting;
            EditorApplication.quitting += OnEditorQuitting;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Ensures the game saves when a script re-compile is about to happen in the editor.
        /// </summary>
        private static void OnPreAssemblyCompile()
        {
            if (!EditorIsDirty)
            {
                return;
            }
            
            EditorSaveConditionChanges();
        }


        /// <summary>
        /// Ensures the game saves when the editor is about to be quitted from.
        /// </summary>
        private static void OnEditorQuitting()
        {
            if (!EditorIsDirty)
            {
                return;
            }
            
            EditorSaveConditionChanges();
        }


        /// <summary>
        /// Ensures the game saves when the editor is exiting edit mode.
        /// </summary>
        /// <param name="change">The state change that occurred.</param>
        private static void OnPlayModeStateChanged(PlayModeStateChange change)
        {
            // Only trigger on enter play mode.
            if (change != PlayModeStateChange.ExitingEditMode) return;
            
            if (!EditorIsDirty)
            {
                return;
            }
            
            EditorSaveConditionChanges();
        }
        
        
        /// <summary>
        /// Ensures the game saves when the editor is saving assets.
        /// </summary>
        /// <param name="paths">Not used</param>
        /// <returns>String[] (Not used)</returns>
        private static string[] OnWillSaveAssets(string[] paths)
        {
            if (!EditorIsDirty)
            {
                return paths;
            }
            
            EditorSaveConditionChanges();
            return paths;
        }


        /// <summary>
        /// Triggers the editor to save when called.
        /// </summary>
        private static void EditorSaveConditionChanges()
        {
            foreach (var entry in ConditionsSoCache.SoLookup)
            {
                entry.ApplyModifiedProperties();
                entry.Update();
            }
            
            EditorIsDirty = false; 
        }
        

        /// <summary>
        /// Tries to set the editor save state as dirty (so it registers as changes made).
        /// </summary>
        public static void TrySetDirty()
        {
            if (EditorIsDirty) return;
            EditorIsDirty = true;
        }


        public static void ForceSaveChanges()
        {
            EditorSaveConditionChanges();
        }
    }
}

#endif