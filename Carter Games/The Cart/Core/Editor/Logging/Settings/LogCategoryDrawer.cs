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

using System.Linq;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Logs.Editor
{
    public static class LogCategoryDrawer
    {
        public static void DrawLogCategories()
        {
            if (LogCategoryStates.CategoryStates.IsEmptyOrNull())
            {
                Debug.Log("NONE");
                return;
            }

            EditorGUILayout.BeginVertical();
            
            foreach (var entry in LogCategoryStates.CategoryStates)
            {
                DrawLogCategory(entry.Key, entry.Value);
            }
            
            EditorGUILayout.EndVertical();
        }


        private static string TrimCategoryNameToLabel(string category)
        {
            return category.Split('.').Last().Replace("LogCategory", string.Empty);
        }


        private static string GetTooltip(string category)
        {
            return $"Toggle logs for: {category}";
        }
        
        
        private static void DrawLogCategory(string typeName, bool enabled)
        {
            EditorGUI.BeginChangeCheck();
            
            enabled = EditorGUILayout.Toggle(new GUIContent(TrimCategoryNameToLabel(typeName), GetTooltip(typeName)), enabled);

            if (!EditorGUI.EndChangeCheck()) return;
            var data = LogCategoryStates.CategoryStates;
            data[typeName] = enabled;
            LogCategoryStates.CategoryStates = SerializableDictionary<string, bool>.FromDictionary(data);
        }
    }
}