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

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Crates;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Management.Editor
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
        private static IEnumerable<ISettingsProvider> codeProviders;
        private static IEnumerable<ICrateSettingsProvider> crateProviders;
        
        private static readonly GUIContent VersionTitle = new GUIContent("Version", "The version of the library in use.");
        private static readonly GUIContent VersionValue = new GUIContent(CartVersionData.VersionNumber, "The version you currently have installed.");
        private static readonly GUIContent ReleaseTitle = new GUIContent("Release Date", "The date this version of the library was published on.");
        private static readonly GUIContent ReleaseValue = new GUIContent(CartVersionData.ReleaseDate, "The release date of the version you currently have installed.");

        private static readonly GUIContent CarterGamesLabel = new GUIContent("Carter Games", "See more from Carter Games here!");
        private static readonly GUIContent SupportDevLabel = new GUIContent("Buy me a coffee?", "Support the developer with a small drink. Totally optional.");
        private static readonly GUIContent GithubLabel = new GUIContent("Github", "The repository for the library.");
        private static readonly GUIContent ContactLabel = new GUIContent("Contact Dev", "Contact the developer for help. You'll usually get an answer within 24 hours.");

        private const string OpenLinkButtonLabel = "Open Link";
        
        private const string CarterGamesLink = "https://www.carter.games";
        private const string SupportDeveloperLink = "https://www.buymeacoffee.com/cartergames";
        private const string GithubLink = "https://github.com/CarterGames/The-Cart";
        private const string ContactLink = "https://www.carter.games/contact";
        
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
                    if (codeProviders == null)
                    {
                        codeProviders = AssemblyHelper.GetClassesOfType<ISettingsProvider>(new Assembly[2]
                            {
                                Assembly.Load("CarterGames.Cart.Editor"),
                                Assembly.Load("CarterGames.Cart.Runtime")
                            })
                            .OrderBy(t => t.GetType().Name);
                    }

                    if (crateProviders == null)
                    {
                        crateProviders = AssemblyHelper.GetClassesOfType<ICrateSettingsProvider>(new Assembly[1]
                            { Assembly.Load("CarterGames.Cart.Crates") }).OrderBy(t => t.GetType().Name);
                    }
                    
                    GUILayout.Space(15f);
                    
                    EditorGUI.indentLevel++;
                    
                    DrawVersionInfo();
                    EditorGUILayout.Space(15f);
                    DrawCoreSettingsProviders();
                    EditorGUILayout.Space(15f);
                    
                    EditorGUILayout.LabelField("Useful Links", EditorStyles.boldLabel);
                    GUILayout.Space(1.5f);
                    DrawButtons();
                    
                    GUILayout.Space(2.5f);
                    EditorGUI.indentLevel--;

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
        /// Draws the version info section on the settings provider.
        /// </summary>
        private static void DrawVersionInfo()
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Space(1.5f);
            
            EditorGUILayout.LabelField("Asset Version Info", EditorStyles.boldLabel);
            
            EditorGUI.BeginDisabledGroup(true);
            
            EditorGUILayout.TextField(VersionTitle,  VersionValue.text);
            EditorGUILayout.TextField(ReleaseTitle, ReleaseValue.text);
            
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(17.5f);
            VersionEditorGUI.DrawCheckForUpdatesButton();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(2.5f);
            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Draws the general options shown on the settings provider. 
        /// </summary>
        private static void DrawCoreSettingsProviders()
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Space(1.5f);
            EditorGUILayout.LabelField("Core Library Settings", EditorStyles.boldLabel);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(17.5f);
            GeneralUtilEditor.DrawHorizontalGUILine();
            EditorGUILayout.EndHorizontal();

            foreach (var provider in codeProviders)
            {
                provider.OnProjectSettingsGUI();
                EditorGUILayout.Space(7.5f);
            }
            
            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
        
        
        [SettingsProviderGroup]
        public static SettingsProvider[] DrawCrateProviders()
        {
            var final = new List<SettingsProvider>();
            
            if (crateProviders == null)
            {
                crateProviders = AssemblyHelper.GetClassesOfType<ICrateSettingsProvider>(new Assembly[1]
                    { Assembly.Load("CarterGames.Cart.Crates") }).OrderBy(t => t.GetType().Name);
            }

            foreach (var provider in crateProviders)
            {
                final.Add(new SettingsProvider(UtilEditor.SettingsWindowPath + "/" + provider.MenuName, SettingsScope.Project)
                {
                    guiHandler = (searchContext) =>
                    {
                        GUILayout.Space(15f);
                    
                        EditorGUI.indentLevel++;
                        provider.OnProjectSettingsGUI();
                        EditorGUI.indentLevel--;

                        WindowUtilEditor.CreateDeselectZone(ref deselectRect);
                    },
                
                    keywords = new HashSet<string>(new[]
                    {
                        "The Cart", "Cart", "External Assets", "Tools", "Code", "Library", "Carter Games", "Carter"
                    })
                });
            }

            return final.ToArray();
        }


        /// <summary>
        /// Draws the buttons section of the window.
        /// </summary>
        private static void DrawButtons()
        {
            // Contact Dev.
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField(ContactLabel, ContactLink);
            
            if (GUILayout.Button(OpenLinkButtonLabel, GUILayout.Width(100)))
            {
                Application.OpenURL(ContactLink);
            }
            
            EditorGUILayout.EndHorizontal();
            
            
            // Github.
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField(GithubLabel, GithubLink);
            
            if (GUILayout.Button(OpenLinkButtonLabel, GUILayout.Width(100)))
            {
                Application.OpenURL(GithubLink);
            }
            
            EditorGUILayout.EndHorizontal();
            
            
            // Carter Games.
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField(CarterGamesLabel, CarterGamesLink);
            
            if (GUILayout.Button(OpenLinkButtonLabel, GUILayout.Width(100)))
            {
                Application.OpenURL(CarterGamesLink);
            }
            
            EditorGUILayout.EndHorizontal();
            
            
            // Buy me a coffee.
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField(SupportDevLabel, SupportDeveloperLink);
            
            if (GUILayout.Button(OpenLinkButtonLabel, GUILayout.Width(100)))
            {
                Application.OpenURL(SupportDeveloperLink);
            }
            
            EditorGUILayout.EndHorizontal();
        }
    }
}