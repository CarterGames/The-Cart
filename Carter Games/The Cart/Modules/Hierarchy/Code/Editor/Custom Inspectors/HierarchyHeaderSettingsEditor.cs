#if CARTERGAMES_CART_MODULE_HIERARCHY

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
using CarterGames.Cart.Core.MetaData.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Hierarchy.Editor
{
    /// <summary>
    /// Handles the custom inspector for the hierarchy header settings script.
    /// </summary>
    [CustomEditor(typeof(HierarchyHeaderSettings))]
    public sealed class HierarchyHeaderSettingsEditor : UnityEditor.Editor, IMeta
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IMeta
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public string MetaDataPath => $"{ScriptableRef.AssetBasePath}/Carter Games/The Cart/Modules/Hierarchy/Data/Meta Data/";
        public MetaData MetaData => Meta.GetData(MetaDataPath, "Hierarchy");
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Overrides
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public override void OnInspectorGUI()
        {
            GUILayout.Space(4f);
            GeneralUtilEditor.DrawMonoScriptSection((HierarchyHeaderSettings)target);
            GUILayout.Space(2f);
            
            DrawHeaderBox();
            GUILayout.Space(2f);
            DrawLabelBox();

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Draws the header settings box & its options.
        /// </summary>
        private void DrawHeaderBox()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(2f);
            EditorGUILayout.LabelField("Header", EditorStyles.boldLabel);
            GUILayout.Space(1f);
            GeneralUtilEditor.DrawHorizontalGUILine();
            GUILayout.Space(1f);

            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(serializedObject.Fp("backgroundColor"), MetaData.Content("customHierarchy_backgroundCol")); 

            if (GUILayout.Button("R", GUILayout.Width("  R  ".GUIWidth())))
            {
                serializedObject.Fp("backgroundColor").colorValue = Color.gray;
            }
            
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(serializedObject.Fp("fullWidth"), MetaData.Content("customHierarchy_fullWidth"));
            
            if (EditorGUI.EndChangeCheck())
            {
                EditorApplication.RepaintHierarchyWindow();
            }
           
            GUILayout.Space(2f);
            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Draws the label settings box & its options.
        /// </summary>
        private void DrawLabelBox()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(2f);
            EditorGUILayout.LabelField(MetaData.Labels["customHierarchy_LabelTitle"], EditorStyles.boldLabel);
            GUILayout.Space(1f);
            GeneralUtilEditor.DrawHorizontalGUILine();
            GUILayout.Space(1f);
            
            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(serializedObject.Fp("label"), MetaData.Content("customHierarchy_label"));
            EditorGUILayout.PropertyField(serializedObject.Fp("labelColor"), MetaData.Content("customHierarchy_labelColor"));
            EditorGUILayout.PropertyField(serializedObject.Fp("boldLabel"), MetaData.Content("customHierarchy_labelBold"));
            EditorGUILayout.PropertyField(serializedObject.Fp("textAlign"), MetaData.Content("customHierarchy_labelAlignment"));
          
            if (EditorGUI.EndChangeCheck())
            {
                EditorApplication.RepaintHierarchyWindow();
            }
            
            GUILayout.Space(2f);
            EditorGUILayout.EndVertical();
        }
    }
}

#endif