#if CARTERGAMES_CART_MODULE_PANELS && CARTERGAMES_CART_MODULE_EASING && UNITY_EDITOR

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
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Panels.Editor
{
    [CustomEditor(typeof(PanelTransitionEase))]
    public class PanelTransitionEaseEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space(2.5f);
            GeneralUtilEditor.DrawMonoScriptSection((PanelTransitionEase) target);
            
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
            
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);

            var isValid = IsValidSetup();

            GUI.backgroundColor = isValid ? Color.green : Color.red;
            GUILayout.Label(isValid ? ModuleManager.TickIcon : ModuleManager.CrossIcon, new GUIStyle("minibutton"), GUILayout.Width(25));
            GUI.backgroundColor = Color.white;
            
            EditorGUILayout.EndHorizontal();
            
            
            GeneralUtilEditor.DrawHorizontalGUILine();
            

            EditorGUILayout.PropertyField(serializedObject.Fp("useUnscaledTime"));
            GeneralUtilEditor.DrawHorizontalGUILine();
            EditorGUILayout.PropertyField(serializedObject.Fp("target"));
            EditorGUILayout.PropertyField(serializedObject.Fp("outEase"));
            EditorGUILayout.PropertyField(serializedObject.Fp("inEase"));
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }


        private bool IsValidSetup()
        {
            return serializedObject.Fp("target").objectReferenceValue != null &&
                   serializedObject.Fp("outEase").Fpr("easeType").intValue > 0 &&
                   serializedObject.Fp("outEase").Fpr("easeDuration").floatValue > 0 &&
                   serializedObject.Fp("inEase").Fpr("easeType").intValue > 0 &&
                   serializedObject.Fp("inEase").Fpr("easeDuration").floatValue > 0;
        }
    }
}

#endif