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
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Window
{
    /// <summary>
    /// Handles the module window to display all the modules and their statuses.
    /// </summary>
    public sealed class ModulesWindow : EditorWindow
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private IModule selectedModule;

        private static GUIStyle labelStyle;
        private static GUIStyle helpBoxStyle;

        private static List<IModule> moduleGrouping = new List<IModule>();
        private static bool IsShiftPressed { get; set; }

        private static GUIStyle multiSelectStyle;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private Vector2 ScrollPos
        {
            get => (Vector2) PerUserSettings.GetOrCreateValue<Vector2>($"{PerUserSettings.UniqueId}_ModuleManagerScroll",
                SettingType.SessionState, Vector2.zero);
            set => PerUserSettings.SetValue<Vector2>($"{PerUserSettings.UniqueId}_ModuleManagerScroll",
                SettingType.SessionState, value);
        }
        

        private static bool IsSingleSelection => moduleGrouping.Count <= 1;
        
        private bool CanEnableAny => moduleGrouping.Any(t => t != null && !ModuleManager.IsEnabled(t));
        private bool CanDisableAny => moduleGrouping.Any(t => t != null && ModuleManager.IsEnabled(t));

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Menu Items
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [MenuItem("Tools/Carter Games/The Cart/Module Manager", priority = 1000)]
        private static void ShowWindow()
        {
            var window = GetWindow<ModulesWindow>();
            window.titleContent = new GUIContent("Cart Modules");
            window.maxSize = new Vector2(750, 600);
            window.Show();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void OnEnable()
        {
            if (!string.IsNullOrEmpty(EditorSettingsModuleWindow.SelectedModuleName))
            {
                selectedModule = ModuleManager.GetModuleFromName(EditorSettingsModuleWindow.SelectedModuleName);
            }
            
            if (EditorSettingsModuleWindow.MultiSelectModules.Count > 0)
            {
                moduleGrouping = EditorSettingsModuleWindow.MultiSelectModules;
                selectedModule = null;
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
            
            if (ModuleManager.AllModules.Length <= 0) return;

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
        /// Handles the left GUI for the window, showing the buttons for each module.
        /// </summary>
        private void OnLeftGUI()
        {
            ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos,  "HelpBox", GUILayout.Width(194));
            
            
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
            
            
            foreach (var module in ModuleManager.AllModules)
            {
                if (IsSingleSelection)
                {
                    GUI.backgroundColor = module.ModuleName.Equals(EditorSettingsModuleWindow.SelectedModuleName)
                        ? Color.gray
                        : Color.white;
                }
                else
                {
                    GUI.backgroundColor = moduleGrouping.Contains(module)
                        ? Color.gray
                        : Color.white;
                }

                EditorGUILayout.BeginHorizontal();

                var btn = GUILayout.Button(module.ModuleName, GUILayout.Width(175 - 22.5f));
                
                if (btn)
                {
                    EditorSettingsModuleWindow.SelectedModuleName = module.ModuleName;

                    if (IsShiftPressed)
                    {
                        if (!moduleGrouping.Contains(module))
                        {
                            if (!moduleGrouping.Contains(selectedModule))
                            {
                                moduleGrouping.Add(selectedModule);
                                EditorSettingsModuleWindow.MultiSelectModules = moduleGrouping;
                            }
                            
                            if (!moduleGrouping.Contains(module))
                            {
                                moduleGrouping.Add(module);
                                EditorSettingsModuleWindow.MultiSelectModules = moduleGrouping;
                            }
                        }
                        else
                        {
                            moduleGrouping.Remove(module);
                            EditorSettingsModuleWindow.MultiSelectModules = moduleGrouping;
                        }
                    }
                    else
                    {
                        if (!IsSingleSelection)
                        {
                            moduleGrouping.Clear();
                            EditorSettingsModuleWindow.MultiSelectModules = moduleGrouping;
                        }
                        
                        selectedModule = ModuleManager.AllModules.First(t => t.ModuleName.Equals(EditorSettingsModuleWindow.SelectedModuleName));
                    }
                }
                
                EditorGUILayout.LabelField(ModuleManager.GetModuleStatusIcon(module), labelStyle, GUILayout.Width(17.5f));
                
                EditorGUILayout.EndHorizontal();
                
                GUI.backgroundColor = Color.white;
            }
            
            EditorGUILayout.EndScrollView();
        }

        
        /// <summary>
        /// Handles the right GUI for the window, showing the information for the selected module.
        /// </summary>
        private void OnRightGUI()
        {
            if (IsSingleSelection)
            {
                EditorGUILayout.BeginVertical();
                
                ModuleDisplay.DrawModule(selectedModule);
                
                GUILayout.FlexibleSpace();
                
                EditorGUILayout.HelpBox("Shift-click to select multiple modules at once to perform group actions.", MessageType.Info);
                GUILayout.Space(1.5f);
                
                EditorGUILayout.EndVertical();
            }
            else
            {
                OnMultiSelect();
            }
        }


        /// <summary>
        /// Draws GUI when more than 1 module is selected at a time.
        /// </summary>
        private void OnMultiSelect()
        {
            if (IsSingleSelection) return;
            
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField("Edit multiple modules", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.HelpBox("Editing multiple modules at once. You can perform the same action to all of the modules selected at once.", MessageType.Info);

            var toShow = moduleGrouping.Where(t => t != null).OrderBy(t => t.ModuleName).ToList();
            
            EditorGUILayout.BeginVertical();
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Module", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Status", EditorStyles.boldLabel, GUILayout.Width(40f));
            EditorGUILayout.EndHorizontal();
            
            GeneralUtilEditor.DrawHorizontalGUILine();

            for (var i = 0; i < toShow.Count; i++)
            {
                var module = toShow[i];
                
                if (i % 2 == 0)
                {
                    EditorGUILayout.BeginHorizontal(multiSelectStyle);
                }
                else
                {
                    EditorGUILayout.BeginHorizontal();
                }
                
                EditorGUILayout.LabelField(module.ModuleName);
                ModuleDisplay.DrawModuleStatusButton(module, false);
                
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
            GeneralUtilEditor.DrawHorizontalGUILine();
            EditorGUILayout.BeginHorizontal();
            
            GUI.backgroundColor = ModuleManager.InstallCol;
            
            EditorGUI.BeginDisabledGroup(!CanEnableAny);
            if (GUILayout.Button(GeneralUtilEditor.TickIcon + " Enable", GUILayout.Width(90f)))
            {
                CscFileHandler.AddDefine(moduleGrouping.ToList());
            }
            EditorGUI.EndDisabledGroup();
            
            GUI.backgroundColor = ModuleManager.UninstallCol;
            
            EditorGUI.BeginDisabledGroup(!CanDisableAny);
            if (GUILayout.Button(GeneralUtilEditor.CrossIcon + " Disable", GUILayout.Width(90f)))
            {
                CscFileHandler.RemoveDefine(moduleGrouping.ToList());
            }
            EditorGUI.EndDisabledGroup();
            GUI.backgroundColor = Color.white;
            
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
    }
}