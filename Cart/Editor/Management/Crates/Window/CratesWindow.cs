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
using CarterGames.Cart;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Window
{
    /// <summary>
    /// Handles the crate window to display all the crates and their statuses.
    /// </summary>
    public sealed class CratesWindow : EditorWindow
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private Crate selectedCrate;
        private ExternalCrate selectedPackagedCrate;

        private static GUIStyle labelStyle;
        private static GUIStyle helpBoxStyle;

        private static List<Crate> crateGrouping = new List<Crate>();
        private static bool IsShiftPressed { get; set; }

        private static GUIStyle multiSelectStyle;

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
            window.Show();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void OnEnable()
        {
            if (!string.IsNullOrEmpty(EditorSettingsCrateWindow.SelectedCrateName))
            {
                selectedCrate = CrateManager.GetCrateFromName(EditorSettingsCrateWindow.SelectedCrateName);
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
            
            if (!CrateManager.AllCrates.Any()) return;

            EditorGUILayout.BeginHorizontal();
            OnLeftGUI();
            GUILayout.Space(5f);
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

            
            if (Event.current.shift)
            {
                if (!IsShiftPressed)
                {
                    IsShiftPressed = true;
                }
            }
            else
            {
                if (IsShiftPressed)
                {
                    IsShiftPressed = false;
                }
            }
            
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.LabelField("By Carter Games", EditorStyles.boldLabel);

            foreach (var crate in CrateManager.CratesLookupByAuthor["Carter Games"])
            {
                DrawCrateOption(crate);
            }
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space(5f);

            foreach (var collection in CrateManager.CratesLookupByAuthor)
            {
                if (collection.Key == "Carter Games") continue;
                
                EditorGUILayout.BeginVertical("HelpBox");
                EditorGUILayout.LabelField($"By {collection.Key}", EditorStyles.boldLabel);

                foreach (var crate in collection.Value)
                {
                    DrawCrateOption(crate);
                }
                EditorGUILayout.EndVertical();
            }
            
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

                if (IsShiftPressed)
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
            if (IsSingleSelection)
            {
                if (selectedCrate != null)
                {
                    EditorGUILayout.BeginVertical();
                
                    CrateDisplay.DrawCrate(selectedCrate);
                
                    GUILayout.FlexibleSpace();
                
                    EditorGUILayout.HelpBox("Shift-click to select multiple crates at once to perform group actions.", MessageType.Info);
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
            EditorGUILayout.LabelField("crate", EditorStyles.boldLabel);
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
            
            GUI.backgroundColor = CrateManager.InstallCol;
            
            EditorGUI.BeginDisabledGroup(!CanEnableAny);
            if (GUILayout.Button(GeneralUtilEditor.TickIcon + " Enable", GUILayout.Width(90f)))
            {
                CscFileHandler.AddDefine(crateGrouping.ToList());
            }
            EditorGUI.EndDisabledGroup();
            
            GUI.backgroundColor = CrateManager.UninstallCol;
            
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
    }
}