/*
 * The Cart
 * Copyright (c) 2026 Carter Games
 *
 * This program is free software: you can redistribute it and/or modify it under the terms of the
 * GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version. 
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
 *
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <https://www.gnu.org/licenses/>. 
 */

using System;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Editor
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