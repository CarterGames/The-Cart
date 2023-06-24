/*
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
using Scarlet.Editor.Utility;
using Scarlet.Editor.VersionCheck;
using Scarlet.General.Reflection;
using Scarlet.Random;
using UnityEditor;
using UnityEngine;

namespace Scarlet.Editor
{
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
                    DrawGeneralOptions();
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
            VersionEditorGUI.DrawCheckForUpdatesButton();
            
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.LabelField(ReleaseTitle, ReleaseValue);

            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Draws the general options shown on the settings provider. 
        /// </summary>
        private static void DrawGeneralOptions()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUI.BeginChangeCheck();

            DrawRngSettings();
            
            if (EditorGUI.EndChangeCheck())
            {
                UtilEditor.SettingsObject.ApplyModifiedProperties();
                UtilEditor.SettingsObject.Update();
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


        
        /// <summary>
        /// Draws the rng options in the library settings.
        /// </summary>
        private static void DrawRngSettings()
        {
            UtilEditor.SettingsObject.FindProperty("isRngExpanded").boolValue =
                EditorGUILayout.Foldout(UtilEditor.SettingsObject.FindProperty("isRngExpanded").boolValue, "Rng");

            
            if (!UtilEditor.SettingsObject.FindProperty("isRngExpanded").boolValue) return;


            EditorGUILayout.BeginVertical();
            EditorGUI.indentLevel++;
            
            
            // Draw the provider enum field on the GUI...
            EditorGUILayout.PropertyField(UtilEditor.SettingsObject.FindProperty("rngProvider"));

            
            var rngProvider = UtilEditor.SettingsObject.FindProperty("rngProvider");
            
            
            // If set to a provider that doesn't have a seed, return...
            // Currently this is only 0 - (Unity Random)
            if (rngProvider.intValue <= 0)
            {
                EditorGUI.indentLevel--;
                EditorGUILayout.EndVertical();
                return;
            }
            

            EditorGUILayout.BeginHorizontal();
            
        
            var systemSeedProperty = UtilEditor.SettingsObject.FindProperty("systemSeed");
            var aleaSeedProperty = UtilEditor.SettingsObject.FindProperty("aleaSeed");

            
            // System Seed Field
            if (rngProvider.intValue == 1)
            {
                EditorGUILayout.PropertyField(systemSeedProperty);
            }
                
            
            // Alea Seed Field
            if (rngProvider.intValue == 2)
            {
                EditorGUILayout.PropertyField(aleaSeedProperty);
            }


            // Draws the button to copy the seed...
            if (GUILayout.Button("Copy", GUILayout.Width(65)))
            {
                if (rngProvider.intValue == 1)
                {
                    systemSeedProperty.intValue.ToString().CopyToClipboard();
                }
                if (rngProvider.intValue == 2)
                {
                    aleaSeedProperty.stringValue.CopyToClipboard();
                }
                    
                Dialogue.Display("Seed Copy", "The seed has been added to your clipboard", "Continue");
            }

            
            // Draws the button the regenerate the seed. 
            if (GUILayout.Button("Regenerate", GUILayout.Width(100)))
            {
                ReflectionHelper.GetField(typeof(Rng), "providerCache", false, true).SetValue(null, null);
                var seededProvider = (ISeededRngProvider) ReflectionHelper.GetProperty(typeof(Rng), "Provider", false, true).GetValue(null);

                Dialogue.Display("Regen Seed", "Are you sure you want to regen the Seed?", "Yes",
                    "Cancel", seededProvider.GenerateSeed);
            }

            
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }
    }
}