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

using System.Linq;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Logs.Editor
{
    public static class LogCategoryDrawer
    {
        public static void DrawLogCategories()
        {
            if (ScriptableRef.LogCategoriesObject.Fp("lookup").Fpr("list").arraySize <= 0) return;
            
            for (var i = 0; i < ScriptableRef.LogCategoriesObject.Fp("lookup").Fpr("list").arraySize; i++)
            {
                DrawLogCategory(ScriptableRef.LogCategoriesObject.Fp("lookup").Fpr("list").GetIndex(i));
            }
        }


        private static string TrimCategoryNameToLabel(string category)
        {
            return category.Split('.').Last().Replace("LogCategory", string.Empty);
        }


        private static string GetTooltip(string category)
        {
            return $"Toggle logs for: {category}";
        }
        
        
        private static void DrawLogCategory(SerializedProperty category)
        {
            ScriptableRef.LogCategoriesObject.Update();
            
            EditorGUI.BeginChangeCheck();
            
            category.Fpr("value").boolValue = EditorGUILayout.Toggle(new GUIContent(TrimCategoryNameToLabel(category.Fpr("key").stringValue), GetTooltip(category.Fpr("key").stringValue)), category.Fpr("value").boolValue);
            
            if (EditorGUI.EndChangeCheck())
            {
                ScriptableRef.LogCategoriesObject.ApplyModifiedProperties();
                ScriptableRef.LogCategoriesObject.Update();
            }
        }
    }
}