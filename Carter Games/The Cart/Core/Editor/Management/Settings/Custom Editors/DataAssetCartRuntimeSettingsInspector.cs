/*
 * Copyright (c) 2025 Carter Games
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
using System.Reflection;
using CarterGames.Cart.Core.Data.Editor;
using CarterGames.Cart.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Management.Editor
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
            EditorGUILayout.LabelField("Core", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            foreach (var providerKvp in AssemblyHelper.GetClassesOfType<ISettingsProvider>(new Assembly[1] { Assembly.Load("CarterGames.Cart.Core.Editor") }).OrderBy(t => t.GetType().Name))
            {
                providerKvp.OnInspectorSettingsGUI();
                GUILayout.Space(2f);
            }
            
            GUILayout.Space(12.5f);
            
            if (AssemblyHelper.CountClassesOfType<ISettingsProvider>(new Assembly[1] { Assembly.Load("CarterGames.Cart.Modules") }) <= 0) return;
            
            EditorGUILayout.LabelField("Modules", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            foreach (var providerKvp in AssemblyHelper.GetClassesOfType<ISettingsProvider>(new Assembly[1] { Assembly.Load("CarterGames.Cart.Modules") }).OrderBy(t => t.GetType().Name))
            {
                providerKvp.OnInspectorSettingsGUI();
                GUILayout.Space(2f);
            }
        }
    }
}