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

using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Management.Editor;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Window
{
    /// <summary>
    /// Handles the crate window to display all the crates and their statuses.
    /// </summary>
    public sealed class CratesWindow : StandardEditorWindow
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static Crate selectedCrate;
        private static ExternalCrate selectedPackagedCrate;

        private static GUIStyle labelStyle;
        private static GUIStyle helpBoxStyle;

        private static List<Crate> crateGrouping = new List<Crate>();
        private static bool IsCtrlPressed { get; set; }

        private static GUIStyle multiSelectStyle;

        private static IEnumerable<KeyValuePair<string, List<Crate>>> userCratesCache;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private Vector2 ScrollPos
        {
            get => (Vector2) PerUserSettings.GetOrCreateValue<Vector2>($"{PerUserSettings.UniqueId}_CrateManagerScroll",
                SettingType.SessionState, Vector2.zero);
            set => PerUserSettings.SetValue<Vector2>($"{PerUserSettings.UniqueId}_CrateManagerScroll",
                SettingType.SessionState, value);
        }
        

        private static bool IsSingleSelection => crateGrouping.Count <= 1;
        
        private bool CanEnableAny => crateGrouping.Any(t => t != null && !CrateManager.IsEnabled(t));
        private bool CanDisableAny => crateGrouping.Any(t => t != null && CrateManager.IsEnabled(t));

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Menu Items
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [MenuItem("Tools/Carter Games/The Cart/Crate Manager", priority = 1000)]
        private static void ShowWindow()
        {
            var window = GetWindow<CratesWindow>();
            window.titleContent = new GUIContent("Crates [The Cart]", EditorGUIUtility.IconContent("d_Prefab On Icon").image);
            window.maxSize = new Vector2(750, 600);
            window.minSize = new Vector2(600, 500);
            window.ShowPopup();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void OnEnable()
        {
            if (!string.IsNullOrEmpty(EditorSettingsCrateWindow.SelectedCrateName))
            {
                selectedCrate = CrateManager.GetCrateByName(EditorSettingsCrateWindow.SelectedCrateName);
            }
            
            if (EditorSettingsCrateWindow.MultiSelectCrates.Count > 0)
            {
                crateGrouping = EditorSettingsCrateWindow.MultiSelectCrates;
                selectedCrate = null;
            }

            multiSelectStyle = new GUIStyle
            {
                normal =
                {
                    background = TextureHelper.SolidColorTexture2D(1, 1, new Color32(73, 73, 73, 255))
                }
            };
        }
        
        
        private void OnGUI()
        {
            labelStyle ??= new GUIStyle(EditorStyles.label);
            labelStyle.wordWrap = true;
            labelStyle.richText = true;

            EditorStyles.helpBox.richText = true;
            
            if (!CrateManager.AnyCratesInProject)
            {
                EditorGUILayout.HelpBox("No crates detected in the project. Add some to have controls for them here",
                    MessageType.Info);
                return;
            }

            EditorGUILayout.BeginHorizontal();
            OnLeftGUI();
            //GeneralUtilEditor.DrawVerticalGUILine();
            GUILayout.Space(2f);
            OnRightGUI();
            EditorGUILayout.EndHorizontal();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Handles the left GUI for the window, showing the buttons for each crate.
        /// </summary>
        private void OnLeftGUI()
        {
            ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos, GUILayout.Width(220));

            // CTRL pressed
            /* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
            IsCtrlPressed = Event.current.control;
            
            
            // Carter Games Crates
            /* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.LabelField($"By {CrateConstants.CarterGamesAuthor}", EditorStyles.boldLabel);

            foreach (var crate in CrateManager.GetAllCratesFromAuthor(CrateConstants.CarterGamesAuthor))
            {
                DrawCrateOption(crate);
            }
            
            EditorGUILayout.EndVertical();
            /* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
            
            
            EditorGUILayout.Space(5f);

            
            // User Crates
            /* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
            if (userCratesCache == null)
            {
                // Crates from Carter Games are skipped along with any that are not valid.
                userCratesCache = CrateManager.GetAllCratesInProjectByAuthor()
                    .Where(t => t.Key != CrateConstants.CarterGamesAuthor);
            }
            
            foreach (var collection in userCratesCache)
            {
                EditorGUILayout.BeginVertical("HelpBox");
                EditorGUILayout.LabelField($"By {collection.Key}", EditorStyles.boldLabel);

                foreach (var crate in collection.Value)
                {
                    DrawCrateOption(crate);
                }
                
                EditorGUILayout.EndVertical();
            }
            /* ────────────────────────────────────────────────────────────────────────────────────────────────────── */

            
            EditorGUILayout.EndScrollView();
        }


        private void DrawCrateOption(Crate crate)
        {
            if (IsSingleSelection)
            {
                GUI.backgroundColor = crate.CrateName.Equals(EditorSettingsCrateWindow.SelectedCrateName)
                    ? Color.gray
                    : Color.white;
            }
            else
            {
                GUI.backgroundColor = crateGrouping.Contains(crate)
                    ? Color.gray
                    : Color.white;
            }

            EditorGUILayout.BeginHorizontal();

            var btn = GUILayout.Button(crate.CrateName, GUILayout.Width(162.5f));

            GUI.backgroundColor = Color.white;

            if (btn)
            {
                EditorSettingsCrateWindow.SelectedCrateName = crate.CrateName;

                if (IsCtrlPressed)
                {
                    if (!crateGrouping.Contains(crate))
                    {
                        if (!crateGrouping.Contains(selectedCrate))
                        {
                            crateGrouping.Add(selectedCrate);
                            EditorSettingsCrateWindow.MultiSelectCrates = crateGrouping;
                        }

                        if (!crateGrouping.Contains(crate))
                        {
                            crateGrouping.Add(crate);
                            EditorSettingsCrateWindow.MultiSelectCrates = crateGrouping;
                        }
                    }
                    else
                    {
                        crateGrouping.Remove(crate);
                        EditorSettingsCrateWindow.MultiSelectCrates = crateGrouping;
                    }
                }
                else
                {
                    if (!IsSingleSelection)
                    {
                        crateGrouping.Clear();
                        EditorSettingsCrateWindow.MultiSelectCrates = crateGrouping;
                    }

                    selectedPackagedCrate = null;
                    selectedCrate = CrateManager.AllCrates.First(t =>
                        t.CrateName.Equals(EditorSettingsCrateWindow.SelectedCrateName));
                }
            }

            CrateDisplayCommon.GetCrateStatusButton(crate, false);

            EditorGUILayout.EndHorizontal();

            GUI.backgroundColor = Color.white;
        }



        /// <summary>
        /// Handles the right GUI for the window, showing the information for the selected crate.
        /// </summary>
        private void OnRightGUI()
        {
            // Single Crate
            /* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
            if (IsSingleSelection)
            {
                if (!CrateValidator.IsCrateSetupValid(selectedCrate.CrateTechnicalName, out var failReason))
                {
                    EditorGUILayout.HelpBox($"CRATE INVALID: {failReason}", MessageType.Warning);
                }
                else
                {
                    if (selectedCrate != null)
                    {
                        EditorGUILayout.BeginVertical();

                        CrateDisplay.DrawCrate(selectedCrate);

                        GUILayout.FlexibleSpace();

                        EditorGUILayout.HelpBox("Ctrl-click to select multiple crates at once to perform group actions.", MessageType.Info);
                        GUILayout.Space(1.5f);

                        EditorGUILayout.EndVertical();
                    }
                    else if (selectedPackagedCrate != null)
                    {
                        EditorGUILayout.BeginVertical();

                        // DrawPackagedCrate(selectedPackagedCrate);

                        EditorGUILayout.EndVertical();
                    }
                }
            }
            // Multi Crate
            /* ────────────────────────────────────────────────────────────────────────────────────────────────────── */
            else
            {
                OnMultiSelect();
            }
        }


        /// <summary>
        /// Draws GUI when more than 1 crate is selected at a time.
        /// </summary>
        private void OnMultiSelect()
        {
            if (IsSingleSelection) return;

            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Edit multiple crates", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();

            EditorGUILayout.HelpBox("Editing multiple crates at once. You can perform the same action to all of the crates selected at once.", MessageType.Info);

            var toShow = crateGrouping.Where(t => t != null).OrderBy(t => t.CrateName).ToList();

            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Crate", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Status", EditorStyles.boldLabel, GUILayout.Width(40f));
            EditorGUILayout.EndHorizontal();

            GeneralUtilEditor.DrawHorizontalGUILine();

            for (var i = 0; i < toShow.Count; i++)
            {
                var crate = toShow[i];

                if (i % 2 == 0)
                {
                    EditorGUILayout.BeginHorizontal(multiSelectStyle);
                }
                else
                {
                    EditorGUILayout.BeginHorizontal();
                }

                EditorGUILayout.LabelField(crate.CrateName);
                CrateDisplayCommon.GetCrateStatusButton(crate, false);

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
            GeneralUtilEditor.DrawHorizontalGUILine();
            EditorGUILayout.BeginHorizontal();

            GUI.backgroundColor = Color.green;

            EditorGUI.BeginDisabledGroup(!CanEnableAny);
            if (GUILayout.Button(GeneralUtilEditor.TickIcon + " Enable", GUILayout.Width(90f)))
            {
                CscFileHandler.AddDefine(crateGrouping.ToList());
            }
            EditorGUI.EndDisabledGroup();

            GUI.backgroundColor = Color.red;

            EditorGUI.BeginDisabledGroup(!CanDisableAny);
            if (GUILayout.Button(GeneralUtilEditor.CrossIcon + " Disable", GUILayout.Width(90f)))
            {
                CscFileHandler.RemoveDefine(crateGrouping.ToList());
            }
            EditorGUI.EndDisabledGroup();
            GUI.backgroundColor = Color.white;

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }


        public static void ForceSelectCrate(Crate crate)
        {
            EditorSettingsCrateWindow.SelectedCrateName = crate.CrateName;
            selectedCrate = CrateManager.GetCrateByTechnicalName(crate.CrateTechnicalName);
        }
    }
}