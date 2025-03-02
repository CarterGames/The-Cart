#if CARTERGAMES_CART_MODULE_LOCALIZATION && UNITY_EDITOR

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

namespace CarterGames.Cart.Modules.Localization.Editor
{
	[CustomPropertyDrawer(typeof(BatchTextElement))]
	public class PropertyDrawerBatchTextElement : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			label = EditorGUI.BeginProperty(position, label, property);
			
			label.text = string.IsNullOrEmpty(property.FindPropertyRelative("locId").stringValue)
				? "Unassigned"
				: $"{property.FindPropertyRelative("locId").stringValue}";
			
			var pos = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
			property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(pos, property.isExpanded, label, new GUIStyle("foldout"));
			
			if (property.isExpanded)
			{
				EditorGUI.indentLevel++;

				pos.y += pos.height + 1.5f;
				
				EditorGUI.PropertyField(pos, property.FindPropertyRelative("locId"));
				
				pos.y += EditorGUIUtility.singleLineHeight + 1.5f;
				
				EditorGUI.PropertyField(pos, property.FindPropertyRelative("label"));

				pos.y += EditorGUIUtility.singleLineHeight + 1.5f;
				
				if (property.Fpr("altFontFromDefault").boolValue)
				{
					EditorGUILayout.BeginHorizontal();
					
					pos.width = pos.width / 20 * 19;
					
					EditorGUI.PropertyField(pos, property.FindPropertyRelative("overrideFont"));

					pos.x += pos.width + 1.5f;
					pos.width = position.width - (pos.width + 1.5f);
					
					GUI.backgroundColor = Color.red;
					if (GUI.Button(pos,GeneralUtilEditor.CrossIcon))
					{
						property.Fpr("altFontFromDefault").boolValue = false;
					}
					GUI.backgroundColor = Color.white;
					EditorGUILayout.EndHorizontal();
				}
				else
				{
					EditorGUI.LabelField(pos, property.Fpr("overrideFont").displayName);
                
					pos.width -= EditorGUIUtility.labelWidth;
					pos.x += EditorGUIUtility.labelWidth;
			    
					if (GUI.Button(pos, "Enable"))
					{
						property.Fpr("altFontFromDefault").boolValue = true;
					}
				}

				EditorGUI.indentLevel--;
			}
			
			EditorGUI.EndFoldoutHeaderGroup();
			EditorGUI.EndProperty();

			property.serializedObject.ApplyModifiedProperties();
			property.serializedObject.Update();
		}


		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property, label, true);
		}
	}
}

#endif