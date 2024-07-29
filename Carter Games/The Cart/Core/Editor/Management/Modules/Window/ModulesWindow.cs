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

using System.Linq;
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
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Menu Items
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [MenuItem("Tools/Carter Games/The Cart/Modules/Module Manager", priority = 1000)]
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
            if (string.IsNullOrEmpty(EditorSettingsModuleWindow.SelectedModuleName)) return;
            selectedModule = ModuleManager.AllModules.FirstOrDefault(t => t.ModuleName.Equals(EditorSettingsModuleWindow.SelectedModuleName));
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
            EditorGUILayout.BeginVertical("HelpBox", GUILayout.Width(175));

            foreach (var module in ModuleManager.AllModules)
            {
                GUI.backgroundColor = module.ModuleName.Equals(EditorSettingsModuleWindow.SelectedModuleName)
                    ? Color.gray
                    : Color.white;

                EditorGUILayout.BeginHorizontal();
                
                if (GUILayout.Button(module.ModuleName, GUILayout.Width(175 - 22.5f)))
                {
                    EditorSettingsModuleWindow.SelectedModuleName = module.ModuleName;
                    selectedModule = ModuleManager.AllModules.First(t => t.ModuleName.Equals(EditorSettingsModuleWindow.SelectedModuleName));
                }
                
                EditorGUILayout.LabelField(ModuleManager.GetModuleStatusIcon(module), labelStyle, GUILayout.Width(17.5f));
                
                EditorGUILayout.EndHorizontal();
                
                GUI.backgroundColor = Color.white;
            }
            
            EditorGUILayout.EndVertical();
        }

        
        /// <summary>
        /// Handles the right GUI for the window, showing the information for the selected module.
        /// </summary>
        private void OnRightGUI()
        {
            ModuleDisplay.DrawModule(selectedModule);
        }



        public static void RepaintWindow()
        {
            GetWindow<ModulesWindow>().Repaint();
        }
    }
}