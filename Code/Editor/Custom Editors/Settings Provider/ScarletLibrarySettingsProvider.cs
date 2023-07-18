﻿/*
 * Copyright (c) 2018-Present Carter Games
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
using Scarlet.Editor.VersionCheck;
using Scarlet.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace Scarlet.Editor
{
    /// <summary>
    /// Handles the settings for the package.
    /// </summary>
    public static class ScarletLibrarySettingsProvider
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static Rect deselectRect;
        
        private static readonly GUIContent VersionTitle = new GUIContent("Version", "The version of the package in use.");
        private static readonly GUIContent VersionValue = new GUIContent(ScarletVersionData.VersionNumber, "The version you currently have installed.");
        private static readonly GUIContent ReleaseTitle = new GUIContent("Release Date", "The date this version of the package was published on.");
        private static readonly GUIContent ReleaseValue = new GUIContent(ScarletVersionData.ReleaseDate, "The data the version you currently have installed was released on.");


        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Menu Items
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The menu item for opening the settings window.
        /// </summary>
        [MenuItem("Tools/Scarlet Library/Edit Settings", priority = 0)]
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
                    DrawRuntimeOptions();
                    GUILayout.Space(1.5f);
                    DrawEditorOptions();
                    DrawButtons();

                    WindowUtilEditor.CreateDeselectZone(ref deselectRect);
                },
                
                keywords = new HashSet<string>(new[]
                {
                    "Scarlet Library", "External Assets", "Tools", "Code", "Library"
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
            if (GUILayout.Button(UtilEditor.ScarletBanner, GUIStyle.none, GUILayout.MaxHeight(110)))
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

            EditorGUILayout.LabelField(VersionTitle, VersionValue);
            GUILayout.FlexibleSpace();
            VersionEditorGUI.DrawCheckForUpdatesButton();
            
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField(ReleaseTitle, ReleaseValue);

            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Draws the general options shown on the settings provider. 
        /// </summary>
        private static void DrawRuntimeOptions()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            EditorGUILayout.LabelField("Runtime", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUI.BeginChangeCheck();

            RngSettingsDrawer.DrawSettings();
            LoggingSettingsDrawer.DrawSettings();
            GameTickerSettingsDrawer.DrawSettings();
            
            if (EditorGUI.EndChangeCheck())
            {
                UtilEditor.SettingsObject.ApplyModifiedProperties();
                UtilEditor.SettingsObject.Update();
            }
            
            EditorGUILayout.EndVertical();
        }
        
        
        /// <summary>
        /// Draws the general options shown on the settings provider. 
        /// </summary>
        private static void DrawEditorOptions()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            EditorGUILayout.LabelField("Editor", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUI.BeginChangeCheck();

            HierarchySeparatorSettingsDrawer.DrawSettings();
            
            if (EditorGUI.EndChangeCheck())
            {
                UtilEditor.EditorSettingsObject.ApplyModifiedProperties();
                UtilEditor.EditorSettingsObject.Update();
            }
            
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
                Application.OpenURL("https://github.com/JonathanMCarter/ScarletLibrary");
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