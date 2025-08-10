#if CARTERGAMES_CART_MODULE_HIERARCHY && UNITY_EDITOR

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
using CarterGames.Cart.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Hierarchy.Editor
{
    [Serializable]
    public sealed class HierarchyHeaderSeparatorConfig : IHierarchyConfig
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
#pragma warning disable 0414
        [SerializeField] private bool isEnabled = true;
#pragma warning restore
        [SerializeField] private string headerPrefix = "<---";
        [SerializeField] private string separatorPrefix = "--->";
        [SerializeField] private HierarchyTitleTextAlign textAlign = HierarchyTitleTextAlign.Center;
        [SerializeField] private bool fullWidth = false;
        [SerializeField] private Color headerBackgroundColor = Color.gray;
        [SerializeField] private Color textColor = Color.white;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public string OptionLabel => "Headers & Separators";
        public string HeaderPrefix => headerPrefix;
        public string SeparatorPrefix => separatorPrefix;
        public HierarchyTitleTextAlign HeaderTextAlign => textAlign;
        public bool HeaderFullWidth => fullWidth;
        public Color HeaderBackgroundColor => headerBackgroundColor;
        public Color HeaderTextColor => textColor;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public void DrawConfig(SerializedProperty serializedProperty)
        {
            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(serializedProperty.Fpr("isEnabled"));
            GeneralUtilEditor.DrawHorizontalGUILine();
            EditorGUILayout.PropertyField(serializedProperty.Fpr("headerPrefix"));
            EditorGUILayout.PropertyField(serializedProperty.Fpr("separatorPrefix"));
            EditorGUILayout.PropertyField(serializedProperty.Fpr("textAlign"));
            EditorGUILayout.PropertyField(serializedProperty.Fpr("fullWidth"));
            EditorGUILayout.PropertyField(serializedProperty.Fpr("headerBackgroundColor"));
            EditorGUILayout.PropertyField(serializedProperty.Fpr("textColor"));

            if (EditorGUI.EndChangeCheck())
            {
                EditorApplication.RepaintHierarchyWindow();
                
                serializedProperty.serializedObject.ApplyModifiedProperties();
                serializedProperty.serializedObject.Update();
            }
        }
    }
}

#endif