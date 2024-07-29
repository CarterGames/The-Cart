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
            if (module == null)
            {
                EditorGUILayout.HelpBox("Select a module to view details here.", MessageType.Info);
                return;
            }

            TrySetupStyles();

            EditorGUILayout.BeginVertical();

            if (ModuleManager.IsProcessing)
            {
                EditorGUILayout.EndVertical();
                EditorGUI.EndDisabledGroup();
                return;
            }

            DrawModuleInfo(module);
            GUILayout.Space(3.5f);
            DrawModuleOptions(module);


            EditorGUILayout.EndVertical();
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

            // Pre-requirements..
            if (module.PreRequisites.Length > 0)
            {
                GeneralUtilEditor.DrawHorizontalGUILine();
                
                EditorGUILayout.LabelField("Requires", EditorStyles.boldLabel);

                foreach (var preRequisite in module.PreRequisites)
                {
                    EditorGUILayout.BeginHorizontal();
                    
                    EditorGUILayout.LabelField("- " + preRequisite.ModuleName + " " + (ModuleManager.IsEnabled(preRequisite) ? "<color=#71ff50>\u2714</color>" : "<color=#ff9494>\u2718</color>"), labelStyle);
                    
                    EditorGUILayout.EndHorizontal();
                }
            }
            
            GeneralUtilEditor.DrawHorizontalGUILine();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Author:", GUILayout.MaxWidth(45));
            EditorGUILayout.LabelField(module.ModuleAuthor);
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(10f);
            EditorGUILayout.LabelField(module.ModuleDescription, labelStyle);

            GUILayout.Space(2.5f);
            
            EditorGUILayout.EndVertical();
        }


        private static void DrawModuleStatusButton(IModule module)
        { 
            if (ModuleManager.IsEnabled(module))
            {
                GUI.backgroundColor = ModuleManager.InstallCol;
                
                GUILayout.Label(ModuleManager.TickIcon + " Enabled", new GUIStyle("minibutton"), GUILayout.MaxWidth(100));
            }
            else
            {
                GUI.backgroundColor = ModuleManager.UninstallCol;
                
                GUILayout.Label(ModuleManager.CrossIcon + " Disabled", new GUIStyle("minibutton"), GUILayout.MaxWidth(100));
            }
            
            GUI.backgroundColor = Color.white;
        }



        private static void DrawModuleOptions(IModule module)
        {
            if (module == null) return;
            
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField("Module Actions:", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();

            if (ModuleManager.IsEnabled(module))
            {
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
            GUI.backgroundColor = ModuleManager.InstallCol;
                
            if (GUILayout.Button(ModuleManager.TickIcon + " Enable"))
            {
                ModuleInstaller.Install(module);
            }

            GUI.backgroundColor = Color.white;
        }


        private static void DrawUninstallButton(IModule module)
        {
            GUI.backgroundColor = ModuleManager.UninstallCol;
                    
            if (GUILayout.Button(ModuleManager.CrossIcon + " Disable"))
            {
                ModuleManager.HasPrompted = false;
                ModuleUninstaller.Uninstall(module);
            }
                    
            GUI.backgroundColor = Color.white;
        }
    }
}