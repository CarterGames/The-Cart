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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Window
{
    public static class ModuleDisplay
    {
        private static GUIStyle labelStyle;
        private static GUIStyle installedBadgeStyle;


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
            
            if (HasDocs(module, out var paths))
            {
                GUILayout.Space(3.5f);
                DrawModuleDocs(module, paths);
            }
            
            GUILayout.Space(3.5f);
            DrawModuleOptions(module);


            EditorGUILayout.EndVertical();
        }



        private static void TrySetupStyles()
        {
            labelStyle ??= new GUIStyle(EditorStyles.label);
            labelStyle.wordWrap = true;
            labelStyle.richText = true;

            installedBadgeStyle ??= new GUIStyle("CN CountBadge");
            installedBadgeStyle.wordWrap = false;
            installedBadgeStyle.richText = true;
            installedBadgeStyle.stretchWidth = false;
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
            
            GeneralUtilEditor.DrawHorizontalGUILine();

            EditorGUILayout.LabelField(module.ModuleDescription, labelStyle);
            
            GeneralUtilEditor.DrawHorizontalGUILine();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Author:", GUILayout.MaxWidth(45));
            EditorGUILayout.LabelField(module.ModuleAuthor);
            EditorGUILayout.EndHorizontal();
            

            // Pre-requirements..
            if (module.PreRequisites.Length > 0)
            {
                GeneralUtilEditor.DrawHorizontalGUILine();
                
                EditorGUILayout.LabelField("Required Modules", EditorStyles.boldLabel);

                foreach (var preRequisite in module.PreRequisites)
                {
                    EditorGUILayout.BeginHorizontal();
                    
#if UNITY_EDITOR_LINUX
                    EditorGUILayout.LabelField("- " + preRequisite.ModuleName + " " + (ModuleManager.IsEnabled(preRequisite) ? "[Enabled]" : "[Disabled]"), labelStyle);
#else
                    EditorGUILayout.LabelField("- " + preRequisite.ModuleName + " " + (ModuleManager.IsEnabled(preRequisite) ? "<color=#71ff50>\u2714</color>" : "<color=#ff9494>\u2718</color>"), labelStyle);
#endif
                    
                    EditorGUILayout.EndHorizontal();
                }
            }
            
            // Optional requirements..
            if (module.OptionalPreRequisites.Length > 0)
            {
                GeneralUtilEditor.DrawHorizontalGUILine();
                
                EditorGUILayout.LabelField("Optional Modules", EditorStyles.boldLabel);

                EditorGUILayout.BeginVertical();
                
                foreach (var preRequisite in module.OptionalPreRequisites)
                {
                    EditorGUILayout.BeginHorizontal();
                    
#if UNITY_EDITOR_LINUX
                    EditorGUILayout.LabelField("- " + preRequisite.ModuleName + " " + (ModuleManager.IsEnabled(preRequisite) ? "[Enabled]" : "[Disabled]"), labelStyle);
#else
                    EditorGUILayout.LabelField("- " + preRequisite.ModuleName + " " + (ModuleManager.IsEnabled(preRequisite) ? "<color=#71ff50>\u2714</color>" : "<color=#ff9494>\u2718</color>"), labelStyle);
#endif                    
                    
                    EditorGUILayout.EndHorizontal();
                }
                
                EditorGUILayout.EndVertical();
            }
            

            // Packaged extra(s)
            if (HasPackagesForModule(module, out var packageInterface))
            {
                GeneralUtilEditor.DrawHorizontalGUILine();
                
                EditorGUILayout.LabelField("Package Dependant Extra's", EditorStyles.boldLabel);

                EditorGUILayout.BeginVertical();
                
                foreach (var entry in packageInterface.Packages)
                {
                    EditorGUILayout.BeginHorizontal();
                    
                    EditorGUILayout.LabelField(entry.displayName);
                    
                    if (ModuleManager.CheckPackageInstalled(entry.packageName))
                    {
                        if (GUILayout.Button("Remove", GUILayout.Width(100f)))
                        {
                            ModuleManager.RemovePackage(entry);
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("Install", GUILayout.Width(100f)))
                        {
                            ModuleManager.AddPackage(entry);
                        }
                    }
                    
                    EditorGUILayout.EndHorizontal();
                }
                
                EditorGUILayout.EndVertical();
            }
            

            GUILayout.Space(2.5f);
            
            EditorGUILayout.EndVertical();
        }


        public static void DrawModuleStatusButton(IModule module, bool showText = true)
        { 
            if (ModuleManager.IsEnabled(module))
            {
                GUI.backgroundColor = ModuleManager.InstallCol;
                
                if (showText)
                {
                    GUILayout.Label(GeneralUtilEditor.TickIcon + " Enabled", new GUIStyle("minibutton"),
                        GUILayout.MaxWidth(100));
                }
                else
                {
                    GUILayout.Label(GeneralUtilEditor.TickIcon, new GUIStyle("minibutton"),
                        GUILayout.MaxWidth(22.5f));
                }
            }
            else
            {
                GUI.backgroundColor = ModuleManager.UninstallCol;
                
                if (showText)
                {
                    GUILayout.Label(GeneralUtilEditor.CrossIcon + " Disabled", new GUIStyle("minibutton"),
                        GUILayout.MaxWidth(100));
                }
                else
                {
                    GUILayout.Label(GeneralUtilEditor.CrossIcon, new GUIStyle("minibutton"),
                        GUILayout.MaxWidth(22.5f));
                }
            }
            
            GUI.backgroundColor = Color.white;
        }


        private static void DrawModuleDocs(IModule module, Dictionary<string, string> paths)
        {
            if (module == null) return;

            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Documentation:", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();

            GUI.backgroundColor = Color.yellow;
            EditorGUILayout.BeginHorizontal();

            if (paths.TryGetValue("usage", out var path))
            {
                if (GUILayout.Button(" Usage"))
                {
                    AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<DefaultAsset>(path));
                }
            }
            
            if (paths.TryGetValue("scripting", out path))
            {
                if (GUILayout.Button(" Scripting API"))
                {
                    AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<DefaultAsset>(path));
                }
            }

            EditorGUILayout.EndHorizontal();
            GUI.backgroundColor = Color.white;

            EditorGUILayout.EndVertical();
        }


        private static bool HasDocs(IModule module, out Dictionary<string, string> paths)
        {
            paths = new Dictionary<string, string>();
            return false;
            
            // Temp disabled as new docs pass to come in 0.13.x
            if (!AssetDatabaseHelper.TryGetScriptPath(module.GetType(), out var path)) return false;
            
            var editedPath = path.Replace($"Core/Editor/Management/Modules/Definitions/{module.GetType().Name}.cs", $"Modules/{module.ModuleName}/");
            var usagePath = $"~Documentation/Docs_Module_{module.ModuleName.TrimSpaces()}_Usage.pdf";
            var scriptingPath = $"~Documentation/Docs_Module_{module.ModuleName.TrimSpaces()}_Scripting.pdf";

            if (AssetDatabase.LoadAssetAtPath<DefaultAsset>(editedPath + usagePath) != null)
            {
                paths.Add("usage", editedPath + usagePath);
            }
            
            if (AssetDatabase.LoadAssetAtPath<DefaultAsset>(editedPath + scriptingPath) != null)
            {
                paths.Add("scripting", editedPath + scriptingPath);
            }

            return paths.Count > 0;
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
                
            if (GUILayout.Button(GeneralUtilEditor.TickIcon + " Enable"))
            {
                ModuleInstaller.Install(module);
            }

            GUI.backgroundColor = Color.white;
        }


        private static void DrawUninstallButton(IModule module)
        {
            GUI.backgroundColor = ModuleManager.UninstallCol;
                    
            if (GUILayout.Button(GeneralUtilEditor.CrossIcon + " Disable"))
            {
                ModuleManager.HasPrompted = false;
                ModuleUninstaller.Uninstall(module);
            }
                    
            GUI.backgroundColor = Color.white;
        }


        private static bool HasPackagesForModule(IModule module, out IPackageDependency package)
        {
            try
            {
                package = AssemblyHelper.GetClassesOfType<IPackageDependency>().First(t => t.GetType() == module.GetType());
                return package != null;
            }
#pragma warning disable 0168
            catch (Exception e)
#pragma warning restore
            {
                package = null;
                return false;
            }
        }
    }
}