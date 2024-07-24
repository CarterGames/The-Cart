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
using UnityEngine.UI;

namespace CarterGames.Cart.Modules.Panels.Editor
{
    [CustomEditor(typeof(PanelCloseButton), true)]
    public class PanelCloseButtonEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space(2.5f);
            GeneralUtilEditor.DrawMonoScriptSection((PanelCloseButton) target);
            
            EditorGUILayout.Space(2.5f);
            DrawSettings();


            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
        
        
        private void DrawSettings()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1f);
            

            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.LabelField("References", EditorStyles.boldLabel);

            GUI.backgroundColor = Color.yellow;
            
            if (GUILayout.Button("Try Get References", GUILayout.Width(140)))
            {
                serializedObject.Fp("panel").objectReferenceValue ??= ((PanelCloseButton)target).GetComponentInParent<Panel>();
                serializedObject.Fp("button").objectReferenceValue ??= ((PanelCloseButton)target).GetComponentInChildren<Button>();

                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            }
            
            GUI.backgroundColor = Color.white;
            
            EditorGUILayout.EndHorizontal();
            
            
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.PropertyField(serializedObject.Fp("panel"));
            EditorGUILayout.PropertyField(serializedObject.Fp("button"));
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}