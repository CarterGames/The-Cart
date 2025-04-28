#if CARTERGAMES_CART_MODULE_CURRENCY && UNITY_EDITOR

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

namespace CarterGames.Cart.Modules.Currency.Editor
{
    public static class DisplayStyleEditor
    {
        public static void DrawStyleSettings(SerializedProperty targetProperty)
        {
            EditorGUILayout.BeginVertical();
            
            EditorGUILayout.PropertyField(targetProperty.Fpr("displayStyle"));
            
            switch (targetProperty.Fpr("displayStyle").intValue)
            {
                case 1:
                    EditorGUILayout.PropertyField(targetProperty.Fpr("instantDelay"));
                    break;
                case 2:
                    EditorGUILayout.PropertyField(targetProperty.Fpr("trickleDuration"));
                    break;
                case 0:
                default:
                    break;
            }
            
            EditorGUILayout.EndVertical();
        }
    }
}

#endif