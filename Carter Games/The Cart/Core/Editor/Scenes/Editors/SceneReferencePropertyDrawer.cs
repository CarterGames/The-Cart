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
    [CustomPropertyDrawer(typeof(SceneReference)), CanEditMultipleObjects]
    public class SceneReferencePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var isDirtyProperty = property.FindPropertyRelative("isDirty");
            
            if (isDirtyProperty.boolValue)
            {
                isDirtyProperty.boolValue = false;
            }

            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            const float padding = 2f;

            var assetPos = position;
            var buildSettingsPos = position;
            buildSettingsPos.x += position.width + padding;

            var sceneAssetProperty = property.FindPropertyRelative("sceneAssetRef");
            var hadReference = sceneAssetProperty.objectReferenceValue != null;

            EditorGUI.PropertyField(assetPos, sceneAssetProperty, new GUIContent());
            
            if (sceneAssetProperty.objectReferenceValue)
            {
                if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(sceneAssetProperty.objectReferenceValue, out var guid, out long _))
                {
                    Array.FindIndex(EditorBuildSettings.scenes, s => s.guid.ToString() == guid);
                }
            }
            else if (hadReference)
            {
                property.FindPropertyRelative("scenePath").stringValue = string.Empty;
            }

            EditorGUI.EndProperty();
        }
    }
}