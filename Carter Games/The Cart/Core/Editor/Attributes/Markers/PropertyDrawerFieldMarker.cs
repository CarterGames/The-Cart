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
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

namespace CarterGames.Cart.Core.Editor
{
	[CustomPropertyDrawer(typeof(FieldMarkerAttribute), true)]
	public class PropertyDrawerFieldMarker : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			EditorGUI.BeginChangeCheck();

			var data = fieldInfo
				.GetCustomAttribute(typeof(FieldMarkerAttribute), true);

			var message = (string)data.GetType()
				.GetField("message", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(data);

			var icon = (Texture)FieldMarkerIconHelper.GetIcon((FieldMarkerIcon)data.GetType()
				.GetField("icon", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(data));

			var showOption = (FieldMarkerShowOption)data.GetType()
				.GetField("showOptions", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(data);



			switch (showOption)
			{
				case FieldMarkerShowOption.None:
					DrawPropertyNoEdits(position, property, label);
					return;
				case FieldMarkerShowOption.All:
					DrawProperty(position, property, label, icon, message);
					return;
				case FieldMarkerShowOption.SceneContext:

					if (!IsInPrefabStage())
					{
						DrawProperty(position, property, label, icon, message);
						return;
					}
					
					break;
				case FieldMarkerShowOption.PrefabContext:

					if (IsInPrefabStage())
					{
						DrawProperty(position, property, label, icon, message);
						return;
					}
					
					break;
				case FieldMarkerShowOption.GameObjectDisabled:
					break;
				case FieldMarkerShowOption.GameObjectEnabled:
					break;
				default:
					break;
			}

			DrawPropertyNoEdits(position, property, label);

			// return;
			//
			// if (showOption.HasFlag(FieldMarkerShowOption.None))
			// {
			// 	EditorGUI.PropertyField(position, property, label);
			//
			// 	if (EditorGUI.EndChangeCheck())
			// 	{
			// 		property.serializedObject.ApplyModifiedProperties();
			// 		property.serializedObject.Update();
			// 	}
			//
			// 	EditorGUI.EndProperty();
			// 	return;
			// }
			//
			// if (showOption.HasFlag(FieldMarkerShowOption.All))
			// {
			// 	var iconRect = position;
			// 	iconRect.width = 17.5f;
			// 	iconRect.x += (position.width - iconRect.width);
			//
			// 	position.width -= iconRect.width;
			// 	EditorGUI.PropertyField(position, property, label);
			// 	EditorGUI.LabelField(iconRect, new GUIContent(icon, message));
			// 	
			// 	if (EditorGUI.EndChangeCheck())
			// 	{
			// 		property.serializedObject.ApplyModifiedProperties();
			// 		property.serializedObject.Update();
			// 	}
			//
			// 	EditorGUI.EndProperty();
			// 	return;
			// }
			//
			// // if (((GameObject)property.serializedObject.context).activeSelf)
			// // {
			// // 	if (showOption.HasFlag(FieldMarkerShowOption.GameObjectEnabled))
			// // 	{
			// // 		var iconRect = position;
			// // 		iconRect.width = 17.5f;
			// // 		iconRect.x += (position.width - iconRect.width);
			// //
			// // 		position.width -= iconRect.width;
			// // 		EditorGUI.PropertyField(position, property, label);
			// // 		EditorGUI.LabelField(iconRect, new GUIContent(icon, message));
			// // 	}
			// // 	else
			// // 	{
			// // 		EditorGUI.PropertyField(position, property, label);
			// // 	}
			// // }
			// // else
			// // {
			// // 	if (showOption.HasFlag(FieldMarkerShowOption.GameObjectDisabled))
			// // 	{
			// // 		var iconRect = position;
			// // 		iconRect.width = 17.5f;
			// // 		iconRect.x += (position.width - iconRect.width);
			// //
			// // 		position.width -= iconRect.width;
			// // 		EditorGUI.PropertyField(position, property, label);
			// // 		EditorGUI.LabelField(iconRect, new GUIContent(icon, message));
			// // 	}
			// // 	else
			// // 	{
			// // 		EditorGUI.PropertyField(position, property, label);
			// // 	}
			// // }
			//
			// if (IsInPrefabStage())
			// {
			// 	if (showOption.HasFlag(FieldMarkerShowOption.PrefabContext))
			// 	{
			// 		var iconRect = position;
			// 		iconRect.width = 17.5f;
			// 		iconRect.x += (position.width - iconRect.width);
			//
			// 		position.width -= iconRect.width;
			// 		EditorGUI.PropertyField(position, property, label);
			// 		EditorGUI.LabelField(iconRect, new GUIContent(icon, message));
			// 	}
			// 	else
			// 	{
			// 		EditorGUI.PropertyField(position, property, label);
			// 	}
			// 	
			// 	if (EditorGUI.EndChangeCheck())
			// 	{
			// 		property.serializedObject.ApplyModifiedProperties();
			// 		property.serializedObject.Update();
			// 	}
			//
			// 	EditorGUI.EndProperty();
			// 	
			// 	return;
			// }
			// else
			// {
			// 	if (showOption.HasFlag(FieldMarkerShowOption.SceneContext))
			// 	{
			// 		var iconRect = position;
			// 		iconRect.width = 17.5f;
			// 		iconRect.x += (position.width - iconRect.width);
			//
			// 		position.width -= iconRect.width;
			// 		EditorGUI.PropertyField(position, property, label);
			// 		EditorGUI.LabelField(iconRect, new GUIContent(icon, message));
			// 	}
			// 	else
			// 	{
			// 		EditorGUI.PropertyField(position, property, label);
			// 	}
			// 	
			// 	if (EditorGUI.EndChangeCheck())
			// 	{
			// 		property.serializedObject.ApplyModifiedProperties();
			// 		property.serializedObject.Update();
			// 	}
			//
			// 	EditorGUI.EndProperty();
			// 	
			// 	return;
			// }
		}


		private void DrawProperty(Rect position, SerializedProperty property, GUIContent label, Texture icon, string message)
		{
			var iconRect = position;
			iconRect.width = 17.5f;
			iconRect.x += (position.width - iconRect.width);

			position.width -= iconRect.width;
			EditorGUI.PropertyField(position, property, label);
			EditorGUI.LabelField(iconRect, new GUIContent(icon, message));

			if (EditorGUI.EndChangeCheck())
			{
				property.serializedObject.ApplyModifiedProperties();
				property.serializedObject.Update();
			}

			EditorGUI.EndProperty();
		}
		
		
		private void DrawPropertyNoEdits(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.PropertyField(position, property, label);

			if (EditorGUI.EndChangeCheck())
			{
				property.serializedObject.ApplyModifiedProperties();
				property.serializedObject.Update();
			}

			EditorGUI.EndProperty();
		}
		
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight(property, label);
		}
		
		
		private static bool IsInPrefabStage()
		{
			return PrefabStageUtility.GetCurrentPrefabStage() != null;
		}
	}
}