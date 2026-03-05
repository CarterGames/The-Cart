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

using CarterGames.Cart.Editor;
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

            DrawCrateInfo(crate);
            DrawDependencies(crate);
            DrawLinks(crate);
            DrawPackageInfo(crate);
            DrawCrateOptions(crate);

            EditorGUILayout.EndVertical();
        }



        private static void TrySetupStyles()
        {
            labelStyle ??= new GUIStyle(EditorStyles.label);
            labelStyle.wordWrap = true;
            labelStyle.richText = true;

            installedBadgeStyle ??= new GUIStyle("ProfilerBadge");
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
            
            EditorGUILayout.TextField(crate.CrateTechnicalName);
            
            GeneralUtilEditor.DrawHorizontalGUILine();

            EditorGUILayout.LabelField(crate.CrateDescription, labelStyle);

            GeneralUtilEditor.DrawHorizontalGUILine();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Author:", GUILayout.MaxWidth(45));
            EditorGUILayout.LabelField(crate.CrateAuthor);
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(2.5f);
            
            EditorGUILayout.EndVertical();
        }


        private static void DrawDependencies(Crate crate)
        {
            if (crate.PreRequisites.IsEmptyOrNull() && crate.OptionalPreRequisites.IsEmptyOrNull()) return;
            
            EditorGUILayout.BeginVertical("HelpBox");
            
            // Pre-requirements..
            if (!crate.PreRequisites.IsEmptyOrNull())
            {
                EditorGUILayout.LabelField("Requires", EditorStyles.boldLabel);
                GeneralUtilEditor.DrawHorizontalGUILine();
                
                foreach (var preRequisite in crate.PreRequisites)
                {
                    if (!CrateManager.TryGetCrateByTechnicalName(preRequisite, out var result)) continue;
                    
                    EditorGUILayout.BeginHorizontal();
                    
                    if (GUILayout.Button(result.CrateName))
                    {
                        CratesWindow.ForceSelectCrate(result);
                    }
                    
                    CrateDisplayCommon.GetCrateStatusButton(result, false);
                    
                    EditorGUILayout.EndHorizontal();
                }
            }
            
            // optional-requirements..
            if (!crate.OptionalPreRequisites.IsEmptyOrNull())
            {
                EditorGUILayout.LabelField("Extended With", EditorStyles.boldLabel);
                GeneralUtilEditor.DrawHorizontalGUILine();
                
                foreach (var preRequisite in crate.OptionalPreRequisites)
                {
                    if (!CrateManager.TryGetCrateByTechnicalName(preRequisite, out var result)) continue;
                    
                    EditorGUILayout.BeginHorizontal();
                    
                    if (GUILayout.Button(result.CrateName))
                    {
                        CratesWindow.ForceSelectCrate(result);
                    }
                    
                    CrateDisplayCommon.GetCrateStatusButton(result, false);
                    
                    EditorGUILayout.EndHorizontal();
                }
            }
            
            EditorGUILayout.EndVertical();
        }

        
        private static void DrawLinks(Crate crate)
        {
            if (crate.CrateLinks.Length <= 0) return;

            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.LabelField("Useful Links", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();

            EditorUrlDrawer.DrawMultiple(crate.CrateLinks);

            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }


        private static void DrawPackageInfo(Crate crate)
        {
            if (!CrateManager.IsExternal(crate)) return;
            
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.LabelField("External Package Info", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();

            var externalCrate = CrateManager.GetAsExternal(crate);
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Technical Name", GUILayout.MaxWidth(100));
            EditorGUILayout.TextField(externalCrate.PackageInfo.technicalName);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Url",GUILayout.MaxWidth(100));
            EditorGUILayout.TextField(externalCrate.PackageInfo.packageUrl);
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }


        private static void DrawCrateOptions(Crate crate)
        {
            if (crate == null) return;
            
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField("Actions", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();

            if (CrateManager.IsExternal(crate))
            {
                if (CrateManager.IsEnabled(crate))
                {
                    CrateDisplayCommon.GetExternalUninstallButton((ExternalCrate) crate);
                }
                else
                {
                    CrateDisplayCommon.GetExternalInstallButton((ExternalCrate) crate);
                }
            }
            else
            {
                if (CrateManager.IsEnabled(crate))
                {
                    CrateDisplayCommon.GetUninstallButton(crate);
                }
                else
                {
                    CrateDisplayCommon.GetInstallButton(crate);
                }
            }
            
            
            EditorGUILayout.EndVertical();
        }
    }
}