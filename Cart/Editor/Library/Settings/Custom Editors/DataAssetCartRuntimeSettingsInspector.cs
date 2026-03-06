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
using System.Reflection;
using CarterGames.Cart.Data.Editor;
using CarterGames.Cart.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Management.Editor
{
    /// <summary>
    /// Handles the custom editor for the runtime settings asset.
    /// </summary>
    [CustomEditor(typeof(DataAssetCoreRuntimeSettings))]
    public sealed class DataAssetCartRuntimeSettingsInspector : InspectorDataAsset
    {
        protected override string[] HideProperties => new string[]
        {
            "m_Script", "variantId", "excludeFromAssetIndex", "isRngExpanded", "rngProviderTypeDef",
            "rngSystemSeed", "rngAleaSeed", "isLoggingExpanded", "loggingUseCartLogs",
            "useLogsInProductionBuilds", "saveMethodTypeDef"
        };
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Editor Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        protected override void DrawInspectorGUI()
        {
            base.DrawInspectorGUI();
            
            EditorGUILayout.Space(2.5f);
            
            DrawSettings();
        }
        
        
        /// <summary>
        /// Overrides the InspectorGUI
        /// </summary>
        public void DrawSettings()
        {
            // EditorGUILayout.LabelField("Core", EditorStyles.boldLabel);
            // GeneralUtilEditor.DrawHorizontalGUILine();
            //
            // foreach (var providerKvp in AssemblyHelper.GetClassesOfType<ISettingsProvider>(new Assembly[1] { Assembly.Load("CarterGames.Cart.Core.Editor") }).OrderBy(t => t.GetType().Name))
            // {
            //     providerKvp.OnInspectorSettingsGUI();
            //     GUILayout.Space(2f);
            // }
            //
            // GUILayout.Space(12.5f);
            //
            // if (AssemblyHelper.CountClassesOfType<ISettingsProvider>(new Assembly[1] { Assembly.Load("CarterGames.Cart.Crates") }) <= 0) return;
            //
            // EditorGUILayout.LabelField("Crates", EditorStyles.boldLabel);
            // GeneralUtilEditor.DrawHorizontalGUILine();
            //
            // foreach (var providerKvp in AssemblyHelper.GetClassesOfType<ISettingsProvider>(new Assembly[1] { Assembly.Load("CarterGames.Cart.Crates") }).OrderBy(t => t.GetType().Name))
            // {
            //     providerKvp.OnInspectorSettingsGUI();
            //     GUILayout.Space(2f);
            // }
        }
    }
}