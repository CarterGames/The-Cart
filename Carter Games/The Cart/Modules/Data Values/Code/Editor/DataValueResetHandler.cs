#if CARTERGAMES_CART_MODULE_DATAVALUES && UNITY_EDITOR

/*
 * Copyright (c) 2024 Carter Games
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace CarterGames.Cart.Modules.DataValues.Editor
{
    /// <summary>
    /// Handles resetting values in the editor.
    /// </summary>
    public sealed class DataValueResetHandler : IPreprocessBuildWithReport
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public int callbackOrder { get; }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IPreprocessBuildWithReport Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public void OnPreprocessBuild(BuildReport report)
        {
            ResetValuesOnPreBuild();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Menu Items
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [MenuItem("Tools/Carter Games/The Cart/Modules/Data Values/Reset All To Default Values", priority = 302)]
        public static void ResetValuesInEditor()
        {
            ResetAllValues();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [InitializeOnLoadMethod]
        private static void EditorInitialize()
        {
            EditorApplication.playModeStateChanged -= OnPlayStateChanged;
            EditorApplication.playModeStateChanged += OnPlayStateChanged;
        }
        
        
        private static void OnPlayStateChanged(PlayModeStateChange change)
        {
            switch (change)
            {
                case PlayModeStateChange.EnteredEditMode:
                    ResetValuesOnExitPlayMode();
                    break;
                case PlayModeStateChange.ExitingEditMode:
                    ResetValuesOnEnterPlayMode();
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                case PlayModeStateChange.ExitingPlayMode:
                default:
                    break;
            }
        }
        
        
        private static void ResetValuesOnEnterPlayMode()
        {
            foreach (var asset in DataValueAccess.AllValues.Where(t => t.ValidStates.HasFlag(DataValueResetState.EnterPlay)))
            {
                asset.ResetAsset();
            }
        }
        
        
        private static void ResetValuesOnExitPlayMode()
        {
            foreach (var asset in DataValueAccess.AllValues.Where(t => t.ValidStates.HasFlag(DataValueResetState.ExitPlay)))
            {
                asset.ResetAsset();
            }
        }
        
        
        private static void ResetValuesOnPreBuild()
        {
            foreach (var asset in DataValueAccess.AllValues.Where(t => t.ValidStates.HasFlag(DataValueResetState.PreBuild)))
            {
                asset.ResetAsset();
            }
        }
        
        
        private static void ResetAllValues()
        {
            foreach (var asset in DataValueAccess.AllValues)
            {
                asset.ResetAsset();
            }
        }
    }
}

#endif