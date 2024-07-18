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

namespace CarterGames.Cart.Modules.Editors
{
    [CustomEditor(typeof(ModuleCache))]
    public class ModuleCacheEditor : Editor
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Editor Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space(2.5f);
            
            GeneralUtilEditor.DrawSoScriptSection(target);

            EditorGUILayout.Space(2.5f);

            DrawVariantId();

            EditorGUILayout.Space(2.5f);
            
            DrawManifest();
            
            EditorGUILayout.Space(2.5f);
            
            DrawModulesRevisions();
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Drawer Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void DrawVariantId()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1f);

            EditorGUILayout.LabelField("Data Asset", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.PropertyField(serializedObject.Fp("variantId"));
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
        
        
        private void DrawModulesRevisions()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1f);
            
            EditorGUILayout.LabelField("Installed Modules", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            if (serializedObject.Fp("installedModuleReceipts").Fpr("list").arraySize > 0)
            {
                for (var i = 0; i < serializedObject.Fp("installedModuleReceipts").Fpr("list").arraySize; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.BeginDisabledGroup(true);

                    EditorGUILayout.LabelField(serializedObject.Fp("installedModuleReceipts").Fpr("list").GetIndex(i)
                        .Fpr("key").stringValue);
                    EditorGUILayout.IntField(
                        serializedObject.Fp("installedModuleReceipts").Fpr("list").GetIndex(i).Fpr("value")
                            .Fpr("revision").intValue, GUILayout.Width(50));

                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.EndHorizontal();
                }
            }
            else
            {
                EditorGUILayout.LabelField("No modules currently installed.");
            }
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }


        private void DrawManifest()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1f);
            
            EditorGUILayout.LabelField("Module Manifest", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.PropertyField(serializedObject.Fp("manifest"));
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}