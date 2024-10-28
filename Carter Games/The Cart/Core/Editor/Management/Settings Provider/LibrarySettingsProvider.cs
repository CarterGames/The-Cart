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

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CarterGames.Cart.Core.Logs.Editor;
using CarterGames.Cart.Core.MetaData.Editor;
using CarterGames.Cart.Core.Random.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Management.Editor
{
    /// <summary>
    /// Handles the settings for the package.
    /// </summary>
    public static class LibrarySettingsProvider
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static Rect deselectRect;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Menu Items
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The menu item for opening the settings window.
        /// </summary>
        [MenuItem("Tools/Carter Games/The Cart/Edit Settings", priority = 0)]
        public static void OpenSettings()
        {
            SettingsService.OpenProjectSettings(UtilEditor.SettingsWindowPath);
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Handles the settings window in the engine.
        /// </summary>
        [SettingsProvider]
        public static SettingsProvider ScarletLibraryProvider()
        {
            var provider = new SettingsProvider(UtilEditor.SettingsWindowPath, SettingsScope.Project)
            {
                guiHandler = (searchContext) =>
                {
                    DrawHeader();
                    DrawVersionInfo();
                    GUILayout.Space(1.5f);
                    DrawAssetOptions();
                    GUILayout.Space(1.5f);
                    DrawRuntimeOptions();
                    GUILayout.Space(1.5f);
                    DrawModuleOptions();
                    GUILayout.Space(1.5f);
                    // DrawExtensionOptions();
                    DrawButtons();

                    WindowUtilEditor.CreateDeselectZone(ref deselectRect);
                },
                
                keywords = new HashSet<string>(new[]
                {
                    "The Cart", "Cart", "External Assets", "Tools", "Code", "Library", "Carter Games", "Carter"
                })
            };
            
            return provider;
        }


        /// <summary>
        /// Draws the header image for the settings provider.
        /// </summary>
        private static void DrawHeader()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(UtilEditor.BannerGraphic, GUIStyle.none, GUILayout.MaxHeight(110)))
            {
                GUI.FocusControl(null);
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        
        /// <summary>
        /// Draws the version info section on the settings provider.
        /// </summary>
        private static void DrawVersionInfo()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            EditorGUILayout.LabelField("Info", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.LabelField(AssetMeta.GetData("Asset").Content("version"), new GUIContent(CartVersionData.VersionNumber));
            GUILayout.FlexibleSpace();
            VersionEditorGUI.DrawCheckForUpdatesButton();
            
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField(AssetMeta.GetData("Asset").Content("releaseDate"), new GUIContent(CartVersionData.ReleaseDate));

            EditorGUILayout.EndVertical();
        }


        private static void DrawAssetOptions()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            EditorGUILayout.LabelField("Asset", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            PerUserSettings.AssetSettingsEditorDropdown = EditorGUILayout.Foldout(PerUserSettings.AssetSettingsEditorDropdown, AssetMeta.GetData("Asset").Content("editorSection"));

            if (PerUserSettings.AssetSettingsEditorDropdown)
            {
                EditorGUILayout.BeginVertical("Box");
                GUILayout.Space(1.5f);
                
                PerUserSettings.VersionValidationAutoCheckOnLoad = EditorGUILayout.Toggle(
                    AssetMeta.GetData("Asset").Content("autoVersionCheck"), PerUserSettings.VersionValidationAutoCheckOnLoad);
                
                GUILayout.Space(1.5f);
                EditorGUILayout.EndVertical();
            }

            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
        

        /// <summary>
        /// Draws the general options shown on the settings provider. 
        /// </summary>
        private static void DrawRuntimeOptions()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            EditorGUILayout.LabelField("Core", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();

            foreach (var provider in AssemblyHelper.GetClassesOfType<ISettingsProvider>(new Assembly[1]
                         {Assembly.Load("CarterGames.Cart.Core.Editor")}).OrderBy(t => t.GetType().Name))
            {
                provider.OnProjectSettingsGUI();
            }
            
            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
        
        
        /// <summary>
        /// Draws all module settings on the settings provider. 
        /// </summary>
        private static void DrawModuleOptions()
        {
            if (AssemblyHelper.CountClassesOfType<ISettingsProvider>(new Assembly[1]
                    {Assembly.Load("CarterGames.Cart.Modules")}) <= 0) return;
            
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            EditorGUILayout.LabelField("Modules", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUI.BeginChangeCheck();

            foreach (var provider in AssemblyHelper.GetClassesOfType<ISettingsProvider>(new Assembly[1]
                         {Assembly.Load("CarterGames.Cart.Modules")}).OrderBy(t => t.GetType().Name))
            {
                provider.OnProjectSettingsGUI();
            }
            
            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
        
        
        private static void DrawExtensionOptions()
        {
            if (AssemblyHelper.CountClassesOfType<ISettingsProvider>(new Assembly[1]
                    {Assembly.Load("CarterGames.Cart.Extensions")}) <= 0) return;
            
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            EditorGUILayout.LabelField("Extensions", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUI.BeginChangeCheck();

            foreach (var provider in AssemblyHelper.GetClassesOfType<ISettingsProvider>(new Assembly[1]
                         {Assembly.Load("CarterGames.Cart.Extensions")}))
            {
                provider.OnProjectSettingsGUI();   
            }
            
            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
        
        
        /// <summary>
        /// Draws the buttons section of the window.
        /// </summary>
        private static void DrawButtons()
        {
            GUILayout.Space(5f);
            
            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button("GitHub", GUILayout.Height(30), GUILayout.MinWidth(100)))
            {
                Application.OpenURL("https://github.com/CarterGames/The-Cart");
            }

            // if (GUILayout.Button("Documentation", GUILayout.Height(30), GUILayout.MinWidth(100)))
            // {
            //     Application.OpenURL("#");
            // }
            
            // if (GUILayout.Button("Support", GUILayout.Height(30), GUILayout.MinWidth(100)))
            // {
            //     Application.OpenURL("#");
            // }

            EditorGUILayout.EndHorizontal();
        }
    }
}