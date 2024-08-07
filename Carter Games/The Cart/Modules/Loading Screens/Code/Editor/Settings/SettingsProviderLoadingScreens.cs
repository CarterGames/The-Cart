#if CARTERGAMES_CART_MODULE_LOADINGSCREENS

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

using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Core.MetaData.Editor;
using UnityEditor;

namespace CarterGames.Cart.Modules.LoadingScreens.Editor
{
    /// <summary>
    /// Handles the settings provider for the hierarchy.
    /// </summary>
    public sealed class SettingsProviderLoadingScreens : ISettingsProvider
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   ISettingsProvider Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Draws the inspector version of the settings.
        /// </summary>
        public void OnInspectorSettingsGUI()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField(AssetMeta.GetData("LoadingScreens").Labels[AssetMeta.SectionTitle], EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(ModulesScriptableRef.LoadingScreensSettingsObject.Fp("loadingScreenPrefab"), AssetMeta.GetData("LoadingScreens").Content("prefab"));

            if (EditorGUI.EndChangeCheck())
            {
                ModulesScriptableRef.LoadingScreensSettingsObject.ApplyModifiedProperties();
                ModulesScriptableRef.LoadingScreensSettingsObject.Update();
            }

            EditorGUILayout.EndVertical();
        }
        

        /// <summary>
        /// Draws the settings provider version of the settings.
        /// </summary>
        public void OnProjectSettingsGUI()
        {
            EditorSettingsLoadingScreens.IsModuleSettingsExpanded =
                EditorGUILayout.Foldout(EditorSettingsLoadingScreens.IsModuleSettingsExpanded, AssetMeta.GetData("LoadingScreens").Content(AssetMeta.SectionTitle));


            if (!EditorSettingsLoadingScreens.IsModuleSettingsExpanded) return;


            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Space(1.5f);
            EditorGUI.indentLevel++;

            // Draw the provider enum field on the GUI...
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(ModulesScriptableRef.LoadingScreensSettingsObject.Fp("loadingScreenPrefab"), AssetMeta.GetData("LoadingScreens").Content("prefab"));


            if (EditorGUI.EndChangeCheck())
            {
                ModulesScriptableRef.LoadingScreensSettingsObject.ApplyModifiedProperties();
                ModulesScriptableRef.LoadingScreensSettingsObject.Update();
            }


            EditorGUI.indentLevel--;
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}

#endif