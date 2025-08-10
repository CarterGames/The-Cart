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

using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Editor
{
    [CustomEditor(typeof(RuntimeTimer))]
    public class InspectorRuntimeTimer : CustomInspector
    {
        protected override string[] HideProperties { get; }
        
        
        protected override void DrawInspectorGUI()
        {
            GUILayout.Space(2.5f);
            
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(serializedObject.Fp("timeRemaining"));
            EditorGUILayout.PropertyField(serializedObject.Fp("timerDuration"));
            EditorGUILayout.PropertyField(serializedObject.Fp("timerPaused"));
            EditorGUILayout.PropertyField(serializedObject.Fp("timerUnscaledTimer"));
            EditorGUILayout.PropertyField(serializedObject.Fp("timerLoops"));
            EditorGUI.EndDisabledGroup();
            
            EditorGUILayout.EndVertical();
        }
    }
}