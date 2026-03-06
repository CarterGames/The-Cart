#if CARTERGAMES_CART_CRATE_LOCALIZATION && UNITY_EDITOR

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

using CarterGames.Cart.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Localization.Editor
{
    [CustomPropertyDrawer(typeof(LocalizationFontDefinition), true)]
    public class DrawerLocalizationFont : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
		    
            var pos = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(pos, property.isExpanded, label, new GUIStyle("foldout"));

            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;
                pos.y += pos.height + 1.5f;
                DrawElements(pos, property, position);
                EditorGUI.indentLevel--;
            }

            EditorGUI.EndFoldoutHeaderGroup();
            EditorGUI.EndProperty();

            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.Update();
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.isExpanded)
            {
                return EditorGUI.GetPropertyHeight(property, label, true) - EditorGUIUtility.singleLineHeight;
            }
	        
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
        
        
        protected virtual void DrawElements(Rect pos, SerializedProperty property, Rect ogPosition)
        {
            EditorGUI.PropertyField(pos, property.Fpr("language"));
            
            pos.y += EditorGUIUtility.singleLineHeight + 1.5f;

            EditorGUI.PropertyField(pos, property.Fpr("fontAsset"));

            pos.y += EditorGUIUtility.singleLineHeight + 1.5f;

            if (property.Fpr("usesMaterial").boolValue)
            {
                EditorGUILayout.BeginHorizontal();
					
                pos.width = pos.width / 20 * 19;
					
                EditorGUI.PropertyField(pos, property.Fpr("fontMaterial"));

                pos.x += pos.width + 1.5f;
                pos.width = ogPosition.width - (pos.width + 1.5f);
					
                GUI.backgroundColor = Color.red;
                if (GUI.Button(pos,GeneralUtilEditor.CrossIcon))
                {
                    property.Fpr("usesMaterial").boolValue = false;
                }
                GUI.backgroundColor = Color.white;
				    
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUI.LabelField(pos, property.Fpr("fontMaterial").displayName);
                
                pos.width -= EditorGUIUtility.labelWidth;
                pos.x += EditorGUIUtility.labelWidth;
			    
                if (GUI.Button(pos, "Use Material"))
                {
                    property.Fpr("usesMaterial").boolValue = true;
                }
            }
        }
    }
}

#endif