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

using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Modules;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Data.Editor
{
    [CustomEditor(typeof(DataAsset), true)]
    public class InspectorDataAsset : CustomInspector
    {
        protected override string[] HideProperties => new string[]
        {
            "m_Script", "variantId", "excludeFromAssetIndex"
        };

        protected override void DrawInspectorGUI()
        {
            GUILayout.Space(1.5f);
            
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            
            EditorGUILayout.PropertyField(serializedObject.Fp("variantId"));

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Store in index");
            
            if (serializedObject.Fp("excludeFromAssetIndex").boolValue)
            {
                GUI.backgroundColor = Color.red;

                EditorGUILayout.LabelField(GeneralUtilEditor.CrossIcon, new GUIStyle("minibutton"), GUILayout.Width(35));
                
                GUI.backgroundColor = Color.yellow;
                
                if (GUILayout.Button("Toggle"))
                {
                    serializedObject.Fp("excludeFromAssetIndex").boolValue = false;
                }
            }
            else
            {
                GUI.backgroundColor = Color.green;
                
                EditorGUILayout.LabelField(GeneralUtilEditor.TickIcon, new GUIStyle("minibutton"), GUILayout.Width(35));
                
                GUI.backgroundColor = Color.yellow;
                
                if (GUILayout.Button("Toggle"))
                {
                    serializedObject.Fp("excludeFromAssetIndex").boolValue = true;
                }
            }

            GUI.backgroundColor = Color.white;
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
            
            GUILayout.Space(5f);
            GeneralUtilEditor.DrawHorizontalGUILine();
            GUILayout.Space(5f);

            DrawBaseInspectorGUI();
        }
    }
}