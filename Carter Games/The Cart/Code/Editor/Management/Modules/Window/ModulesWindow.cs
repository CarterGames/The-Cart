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
using CarterGames.Cart.Core.Management;
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
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Menu Items
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [MenuItem("Tools/Carter Games/The Cart/Modules/Window")]
        private static void ShowWindow()
        {
            var window = GetWindow<ModulesWindow>();
            window.titleContent = new GUIContent("Modules");
            window.maxSize = new Vector2(750, 600);
            window.Show();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void OnEnable()
        {
            ModuleManager.RefreshNamespaceCache();
            
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
        /// Handles the right GUI for the window, showing the infomation for the selected module.
        /// </summary>
        private void OnRightGUI()
        {
            EditorGUILayout.BeginVertical();

            if (ModuleManager.IsProcessing)
            {
                EditorGUILayout.HelpBox("Processing changes...", MessageType.None);
            }
            else
            {
                DrawModuleControls();
                GUILayout.Space(5f);
                DrawModule();
            }
            
            EditorGUILayout.EndVertical();
        }



        public static void RepaintWindow()
        {
            GetWindow<ModulesWindow>().Repaint();
        }


        private void DrawModuleControls()
        {
            if (string.IsNullOrEmpty(EditorSettingsModuleWindow.SelectedModuleName)) return;
            if (selectedModule == null) return;
            
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.BeginHorizontal();

            if (!ModuleManager.IsInstalled(selectedModule))
            {
                EditorGUI.BeginDisabledGroup(!ModuleManager.HasPackage(selectedModule));
                GUI.backgroundColor = ModuleManager.InstallCol;
                
                if (GUILayout.Button(ModuleManager.TickIcon + " Install", GUILayout.Height(25f)))
                {
                    ModuleInstaller.Install(selectedModule);
                }

                GUI.backgroundColor = Color.white;
                EditorGUI.EndDisabledGroup();
            }
            else
            {
                if (ModuleManager.HasUpdate(selectedModule))
                {
                    EditorGUILayout.BeginVertical();
                    GUI.backgroundColor = ModuleManager.UpdateCol;
                    
                    if (GUILayout.Button(ModuleManager.UpdateIcon + " Update", GUILayout.Height(25f)))
                    {
                        ModuleUpdater.UpdateModule(selectedModule);
                    }
                    
                    GUI.backgroundColor = Color.white;
                    GUILayout.Space(1.75f);
                    
                    EditorGUILayout.LabelField("This module has an update available.", labelStyle);
                    GeneralUtilEditor.DrawHorizontalGUILine();
                    EditorGUILayout.LabelField($"Current: Rev.{ModuleManager.InstalledRevisionNumber(selectedModule)}\nLatest: Rev.{CartSoAssetAccessor.GetAsset<ModuleCache>().Manifest.GetData(selectedModule).Revision}", labelStyle);

                    EditorGUILayout.EndVertical();
                }
                else
                {
                    GUI.backgroundColor = ModuleManager.UninstallCol;
                    
                    if (GUILayout.Button(ModuleManager.CrossIcon + " Uninstall", GUILayout.Height(25f)))
                    {
                        ModuleManager.HasPrompted = false;
                        ModuleUninstaller.Uninstall(selectedModule);
                    }
                    
                    GUI.backgroundColor = Color.white;
                }
            }
            
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        
        

        private void DrawModule()
        {
            if (string.IsNullOrEmpty(EditorSettingsModuleWindow.SelectedModuleName)) return;
            if (selectedModule == null) return;
            
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField(selectedModule.ModuleName, EditorStyles.boldLabel);
            
            // Author & revision will go here...
            if (CartSoAssetAccessor.GetAsset<ModuleCache>().Manifest.GetData(selectedModule) != null)
            {
                var revisionLabel = CartSoAssetAccessor.GetAsset<ModuleCache>().InstalledModulesInfo
                    .ContainsKey(selectedModule.Namespace)
                    ? CartSoAssetAccessor.GetAsset<ModuleCache>().InstalledModulesInfo[selectedModule.Namespace]
                        .Revision
                    : CartSoAssetAccessor.GetAsset<ModuleCache>().Manifest.GetData(selectedModule).Revision;
                
                GeneralUtilEditor.DrawHorizontalGUILine();
                EditorGUILayout.LabelField("<b>Rev:</b> " + revisionLabel, labelStyle);
                EditorGUILayout.LabelField("<b>Author:</b> " + (CartSoAssetAccessor.GetAsset<ModuleCache>().Manifest.GetData(selectedModule).Author), labelStyle);
            }
            

            // Pre-requirements..
            if (selectedModule.PreRequisites.Length > 0)
            {
                GeneralUtilEditor.DrawHorizontalGUILine();
                
                EditorGUILayout.LabelField("Requires", EditorStyles.boldLabel);

                foreach (var preRequisite in selectedModule.PreRequisites)
                {
                    EditorGUILayout.BeginHorizontal();
                    
                    EditorGUILayout.LabelField("- " + preRequisite.ModuleName + " " + (ModuleManager.IsInstalled(preRequisite) ? "<color=#71ff50>\u2714</color>" : "<color=#ff9494>\u2718</color>"), labelStyle);
                    
                    EditorGUILayout.EndHorizontal();
                }
            }
            
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.LabelField(selectedModule.ModuleDescription, labelStyle);
            
            GeneralUtilEditor.DrawHorizontalGUILine();
            

            
            EditorGUILayout.EndVertical();
        }
    }
}