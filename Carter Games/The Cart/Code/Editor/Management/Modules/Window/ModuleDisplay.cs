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

using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Management;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Window
{
    public static class ModuleDisplay
    {
        private static GUIStyle labelStyle;
        
        
        public static void DrawModule(IModule module)
        {
            TrySetupStyles();
            
            EditorGUI.BeginDisabledGroup(ModuleManager.CurrentProcess != null);
            
            EditorGUILayout.BeginVertical();
            
            if (ModuleManager.CurrentProcess != null)
            {
                EditorGUILayout.HelpBox("Processing...", MessageType.Info);
            }
            
            if (ModuleManager.IsProcessing)
            {
                EditorGUILayout.HelpBox("Processing changes...", MessageType.None);
            }
            else
            {
                DrawModuleInfo(module);
                GUILayout.Space(3.5f);
                
                // Update info if applicable...
                if (ModuleManager.IsInstalled(module))
                {
                    if (ModuleManager.HasUpdate(module))
                    {
                        DrawUpdateInfo(module);
                        GUILayout.Space(3.5f);
                    }
                }
                
                DrawModuleOptions(module);
            }
            
            EditorGUILayout.EndVertical();
            EditorGUI.EndDisabledGroup();
        }



        private static void TrySetupStyles()
        {
            labelStyle ??= new GUIStyle(EditorStyles.label);
            labelStyle.wordWrap = true;
            labelStyle.richText = true;
        }
        

        private static void DrawModuleInfo(IModule module)
        {
            if (string.IsNullOrEmpty(EditorSettingsModuleWindow.SelectedModuleName)) return;
            if (module == null) return;
            
            EditorGUILayout.BeginVertical("HelpBox");


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(module.ModuleName, EditorStyles.boldLabel);
            DrawModuleStatusButton(module);
            
            EditorGUILayout.EndHorizontal();
            
            // Author & revision will go here...
            if (DataAccess.GetAsset<ModuleCache>().Manifest.GetData(module) != null)
            {
                var revisionLabel = DataAccess.GetAsset<ModuleCache>().InstalledModulesInfo
                    .ContainsKey(module.Namespace)
                    ? DataAccess.GetAsset<ModuleCache>().InstalledModulesInfo[module.Namespace]
                        .Revision
                    : DataAccess.GetAsset<ModuleCache>().Manifest.GetData(module).Revision;
                
                GeneralUtilEditor.DrawHorizontalGUILine();
                EditorGUILayout.LabelField("<b>Rev:</b> " + revisionLabel, labelStyle);
                EditorGUILayout.LabelField("<b>Author:</b> " + (DataAccess.GetAsset<ModuleCache>().Manifest.GetData(module).Author), labelStyle);
            }

            // Pre-requirements..
            if (module.PreRequisites.Length > 0)
            {
                GeneralUtilEditor.DrawHorizontalGUILine();
                
                EditorGUILayout.LabelField("Requires", EditorStyles.boldLabel);

                foreach (var preRequisite in module.PreRequisites)
                {
                    EditorGUILayout.BeginHorizontal();
                    
                    EditorGUILayout.LabelField("- " + preRequisite.ModuleName + " " + (ModuleManager.IsInstalled(preRequisite) ? "<color=#71ff50>\u2714</color>" : "<color=#ff9494>\u2718</color>"), labelStyle);
                    
                    EditorGUILayout.EndHorizontal();
                }
            }
            
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.LabelField(module.ModuleDescription, labelStyle);
            
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.EndVertical();
        }


        private static void DrawModuleStatusButton(IModule module)
        {
            if (ModuleManager.HasUpdate(module))
            {
                GUI.backgroundColor = ModuleManager.UpdateCol;
                
                GUILayout.Label(ModuleManager.UpdateIcon + " Update Available", new GUIStyle("minibutton"));
            }
            else if (ModuleManager.IsInstalled(module))
            {
                GUI.backgroundColor = ModuleManager.InstallCol;
                
                GUILayout.Label(ModuleManager.TickIcon + " Installed", new GUIStyle("minibutton"));
            }
            else
            {
                GUI.backgroundColor = ModuleManager.UninstallCol;
                
                GUILayout.Label(ModuleManager.CrossIcon + " Not Installed", new GUIStyle("minibutton"));
            }
            
            GUI.backgroundColor = Color.white;
        }



        private static void DrawModuleOptions(IModule module)
        {
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField("Module Actions:", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();

            if (ModuleManager.IsInstalled(module))
            {
                if (ModuleManager.HasUpdate(module))
                {
                    DrawUpdateButton(module);
                }
                
                DrawUninstallButton(module);
            }
            else
            {
                DrawInstallButton(module);
            }
            
            EditorGUILayout.EndVertical();
        }


        
        private static void DrawInstallButton(IModule module)
        {
            EditorGUI.BeginDisabledGroup(!ModuleManager.HasPackage(module));
            GUI.backgroundColor = ModuleManager.InstallCol;
                
            if (GUILayout.Button(ModuleManager.TickIcon + " Install"))
            {
                ModuleInstaller.Install(module);
            }

            GUI.backgroundColor = Color.white;
            EditorGUI.EndDisabledGroup();
        }


        private static void DrawUpdateButton(IModule module)
        {
            GUI.backgroundColor = ModuleManager.UpdateCol;
                    
            if (GUILayout.Button(ModuleManager.UpdateIcon + " Update"))
            {
                ModuleUpdater.UpdateModule(module);
            }
                    
            GUI.backgroundColor = Color.white;
        }


        private static void DrawUpdateInfo(IModule module)
        {
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField("This module has an update available.", labelStyle);
            GeneralUtilEditor.DrawHorizontalGUILine();
            EditorGUILayout.LabelField($"Current: Rev.{ModuleManager.InstalledRevisionNumber(module)}\nLatest: Rev.{DataAccess.GetAsset<ModuleCache>().Manifest.GetData(module).Revision}", labelStyle);

            EditorGUILayout.EndVertical();
        }


        private static void DrawUninstallButton(IModule module)
        {
            GUI.backgroundColor = ModuleManager.UninstallCol;
                    
            if (GUILayout.Button(ModuleManager.CrossIcon + " Uninstall"))
            {
                ModuleManager.HasPrompted = false;
                ModuleUninstaller.Uninstall(module);
            }
                    
            GUI.backgroundColor = Color.white;
        }
    }
}