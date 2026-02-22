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
using CarterGames.Cart;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Management;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Window
{
    public static class CrateDisplay
    {
        private static GUIStyle labelStyle;
        private static GUIStyle installedBadgeStyle;


        public static void DrawCrate(Crate crate)
        {
            if (crate == null)
            {
                EditorGUILayout.HelpBox("Select a crate to view details here.", MessageType.Info);
                return;
            }

            TrySetupStyles();

            EditorGUILayout.BeginVertical();

            if (CrateManager.IsProcessing)
            {
                EditorGUILayout.EndVertical();
                EditorGUI.EndDisabledGroup();
                return;
            }

            DrawCrateInfo(crate);


            if (crate.CrateLinks.Length > 0)
            {
                EditorUrlDrawer.DrawMultiple(crate.CrateLinks);
            }
            
            // if (HasDocs(crate, out var paths))
            // {
            //     GUILayout.Space(3.5f);
            //     DrawCrateDocs(crate, paths);
            // }
            
            GUILayout.Space(3.5f);
            DrawCrateOptions(crate);


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


        private static void DrawCrateInfo(Crate crate)
        {
            if (string.IsNullOrEmpty(EditorSettingsCrateWindow.SelectedCrateName)) return;
            if (crate == null) return;

            EditorGUILayout.BeginVertical("HelpBox");


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(crate.CrateName, EditorStyles.boldLabel);
            CrateDisplayCommon.GetCrateStatusButton(crate);

            EditorGUILayout.EndHorizontal();

            GeneralUtilEditor.DrawHorizontalGUILine();

            EditorGUILayout.LabelField(crate.CrateDescription, labelStyle);

            GeneralUtilEditor.DrawHorizontalGUILine();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Author:", GUILayout.MaxWidth(45));
            EditorGUILayout.LabelField(crate.CrateAuthor);
            EditorGUILayout.EndHorizontal();


            // Pre-requirements..
            if (!crate.PreRequisites.IsEmptyOrNull())
            {
                GeneralUtilEditor.DrawHorizontalGUILine();
                
                EditorGUILayout.LabelField("Required Crates", EditorStyles.boldLabel);

                foreach (var preRequisite in crate.PreRequisites)
                {
                    EditorGUILayout.BeginHorizontal();
                    
#if UNITY_EDITOR_LINUX
                    EditorGUILayout.LabelField("- " + preRequisite.CrateName + " " + (CrateManager.IsEnabled(preRequisite) ? "[Enabled]" : "[Disabled]"), labelStyle);
#else
                    EditorGUILayout.LabelField("- " + preRequisite.CrateName + " " + (CrateManager.IsEnabled(preRequisite) ? "<color=#71ff50>\u2714</color>" : "<color=#ff9494>\u2718</color>"), labelStyle);
#endif
                    
                    EditorGUILayout.EndHorizontal();
                }
            }
            
            // Optional requirements..
            if (!crate.OptionalPreRequisites.IsEmptyOrNull())
            {
                GeneralUtilEditor.DrawHorizontalGUILine();
                
                EditorGUILayout.LabelField("Optional Crates", EditorStyles.boldLabel);

                EditorGUILayout.BeginVertical();
                
                foreach (var preRequisite in crate.OptionalPreRequisites)
                {
                    EditorGUILayout.BeginHorizontal();
                    
#if UNITY_EDITOR_LINUX
                    EditorGUILayout.LabelField(new GUIContent(preRequisite.CrateName, (CrateManager.IsEnabled(preRequisite) ? EditorGUIUtility.IconContent("Valid@2x").image : EditorGUIUtility.IconContent("CrossIcon").image)),labelStyle, GUILayout.Height(17.5f));
#else
                    EditorGUILayout.LabelField("- " + preRequisite.CrateName + " " + (CrateManager.IsEnabled(preRequisite) ? "<color=#71ff50>\u2714</color>" : "<color=#ff9494>\u2718</color>"), labelStyle);
#endif                    
                    
                    EditorGUILayout.EndHorizontal();
                }
                
                EditorGUILayout.EndVertical();
            }
            

            // Packaged extra(s)
            if (HasPackagesForCrate(crate, out var packageInterface))
            {
                GeneralUtilEditor.DrawHorizontalGUILine();
                
                EditorGUILayout.LabelField("Package Dependant Extra's", EditorStyles.boldLabel);

                EditorGUILayout.BeginVertical();
                
                foreach (var entry in packageInterface.Packages)
                {
                    EditorGUILayout.BeginHorizontal();
                    
                    EditorGUILayout.LabelField(entry.displayName);
                    
                    if (CrateManager.CheckPackageInstalled(entry.technicalName))
                    {
                        if (GUILayout.Button("Remove", GUILayout.Width(100f)))
                        {
                            CrateManager.RemovePackage(entry);
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("Install", GUILayout.Width(100f)))
                        {
                            CrateManager.AddPackage(entry);
                        }
                    }
                    
                    EditorGUILayout.EndHorizontal();
                }
                
                EditorGUILayout.EndVertical();
            }
            

            GUILayout.Space(2.5f);
            
            EditorGUILayout.EndVertical();
        }
        
        
        private static void DrawCrateStatusButton(ExternalCrate crate, bool showText = true)
        { 
            if (CrateManager.CheckPackageInstalled(crate.PackageInfo.technicalName))
            {
                GUI.backgroundColor = CrateManager.InstallCol;
                
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
                GUI.backgroundColor = CrateManager.UninstallCol;
                
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


        private static void DrawCrateDocs(Crate crate, Dictionary<string, string> paths)
        {
            if (crate == null) return;

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


        private static bool HasDocs(Crate crate, out Dictionary<string, string> paths)
        {
            paths = new Dictionary<string, string>();
            // return false;
            //
            // // Temp disabled as new docs pass to come in 0.13.x
            
            if (!AssetDatabaseHelper.TryGetScriptPath(crate.GetType(), out var path)) return false;
            
            var editedPath = path.Replace($"Core/Editor/Management/Crates/Definitions/{crate.GetType().Name}.cs", $"Crates/{crate.CrateName}/");
            var usagePath = $"~Documentation/Docs_Crate_{crate.CrateName.TrimSpaces()}_Usage.pdf";
            var scriptingPath = $"~Documentation/Docs_Crate_{crate.CrateName.TrimSpaces()}_Scripting.pdf";

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
        
        

        private static void DrawCrateOptions(Crate crate)
        {
            if (crate == null) return;
            
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField("Actions:", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();

            if (CrateManager.IsEnabled(crate))
            {
                CrateDisplayCommon.GetUninstallButton(crate);
            }
            else
            {
                CrateDisplayCommon.GetInstallButton(crate);
            }
            
            EditorGUILayout.EndVertical();
        }


        



        private static bool HasPackagesForCrate(Crate crate, out IPackageDependency package)
        {
            try
            {
                package = AssemblyHelper.GetClassesOfType<IPackageDependency>().First(t => t.GetType() == crate.GetType());
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