#if CARTERGAMES_CART_CRATE_DATAVALUES && UNITY_EDITOR

/*
 * The Cart
 * Copyright (c) 2026 Carter Games
 *
 * This program is free software: you can redistribute it and/or modify it under the terms of the
 * GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version. 
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
 *
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <https://www.gnu.org/licenses/>. 
 */

using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace CarterGames.Cart.Crates.DataValues.Editor
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

        [MenuItem("Tools/Carter Games/The Cart/[Data Values] Reset All To Default Values", priority = 1301)]
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