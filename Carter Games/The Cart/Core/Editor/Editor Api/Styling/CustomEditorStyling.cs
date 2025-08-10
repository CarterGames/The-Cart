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
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Editor
{
    public static class CustomEditorStyling
    {
        public static void SmallCrossButton(Action onPress)
        {
            GUI.backgroundColor = Color.red;
            
            if (GUILayout.Button(GeneralUtilEditor.CrossIcon, GUILayout.Width(GeneralUtilEditor.CrossIcon.GUIWidth() + 7.5f)))
            {
                onPress?.Invoke();
            }
            
            GUI.backgroundColor = Color.white;
        }

        
        public static void CrossMiniButton(string extraLabel, float width = -1)
        {
            GUI.backgroundColor = Color.red;
            
            if (width > 0)
            {
                EditorGUILayout.LabelField(GeneralUtilEditor.CrossIcon + " " + extraLabel, new GUIStyle("minibutton"), GUILayout.Width(width));
            }
            else
            {
                EditorGUILayout.LabelField(GeneralUtilEditor.CrossIcon + " " + extraLabel, new GUIStyle("minibutton"));
            }
            
            GUI.backgroundColor = Color.white;
        }
        

        public static void CrossMiniButton(float width, string extraLabel = "")
        {
            GUI.backgroundColor = Color.red;

            if (string.IsNullOrEmpty(extraLabel))
            {
                EditorGUILayout.LabelField(GeneralUtilEditor.CrossIcon, new GUIStyle("minibutton"), GUILayout.Width(width));
            }
            else
            {
                CrossMiniButton(extraLabel, width);
            }
            
            GUI.backgroundColor = Color.white;
        }
        
        
        public static void TickMiniButton(string extraLabel, float width = -1)
        {
            GUI.backgroundColor = Color.green;
            
            if (width > 0)
            {
                EditorGUILayout.LabelField(GeneralUtilEditor.TickIcon + " " + extraLabel, new GUIStyle("minibutton"), GUILayout.Width(width));
            }
            else
            {
                EditorGUILayout.LabelField(GeneralUtilEditor.TickIcon + " " + extraLabel, new GUIStyle("minibutton"));
            }      
            
            GUI.backgroundColor = Color.white;
        }
        
        
        public static void TickMiniButton(float width, string extraLabel = "")
        {
            GUI.backgroundColor = Color.green;
            
            if (string.IsNullOrEmpty(extraLabel))
            {
                EditorGUILayout.LabelField(GeneralUtilEditor.TickIcon, new GUIStyle("minibutton"), GUILayout.Width(width));
            }
            else
            {
                TickMiniButton(extraLabel, width);
            }
            
            GUI.backgroundColor = Color.white;
        }
    }
}