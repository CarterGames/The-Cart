#if CARTERGAMES_CART_CRATE_DATAVALUES && UNITY_EDITOR

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

namespace CarterGames.Cart.Crates.DataValues.Editor
{
	public static class CustomEditorDataValueDefault
	{
		public static void DrawInspector(SerializedObject serializedObject, bool forceIndent = false)
		{
			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1.5f);
			
			EditorGUILayout.LabelField("Notes", EditorStyles.boldLabel);
			GeneralUtilEditor.DrawHorizontalGUILine();
			serializedObject.Fp("devDescription").stringValue = EditorGUILayout.TextArea(serializedObject.Fp("devDescription").stringValue);
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();
			
			
			EditorGUILayout.Space(5f);
			DrawValue(serializedObject, forceIndent);
			EditorGUILayout.Space(5f);
			DrawDefaultValueSection(serializedObject);
			EditorGUILayout.Space(5f);
			DrawDataValueEventsSection(serializedObject);


			serializedObject.ApplyModifiedProperties();
			serializedObject.Update();
		}


		private static void DrawValue(SerializedObject serializedObject, bool forceIndent = false)
		{
			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1.5f);
			
			EditorGUILayout.LabelField("Data", EditorStyles.boldLabel);
			GeneralUtilEditor.DrawHorizontalGUILine();

			EditorGUILayout.PropertyField(serializedObject.Fp("key"));
			
			if (serializedObject.Fp("value").isArray || serializedObject.Fp("value").exposedReferenceValue != null || forceIndent)
			{
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(12.5f);
				EditorGUILayout.PropertyField(serializedObject.Fp("value"));
				GUILayout.Space(2.5f);
				EditorGUILayout.EndHorizontal();
			}
			else
			{
				EditorGUILayout.PropertyField(serializedObject.Fp("value"));
			}
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();
		}
		
		
		private static void DrawDefaultValueSection(SerializedObject serializedObject)
		{
			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1.5f);
			
			
			if (serializedObject.Fp("canReset").boolValue)
			{
				EditorGUILayout.LabelField("Resetting", EditorStyles.boldLabel);
				GeneralUtilEditor.DrawHorizontalGUILine();
				
				if (serializedObject.Fp("resetStates").intValue == 0)
				{
					EditorGUILayout.Space(1.5f);
					EditorGUILayout.HelpBox("Value will not reset when the reset state is not assigned", MessageType.Info);
					EditorGUILayout.Space(2.5f);
				}

				if (serializedObject.Fp("defaultValue").hasVisibleChildren)
				{
					EditorGUILayout.BeginHorizontal();
					GUILayout.Space(12.5f);
					EditorGUILayout.PropertyField(serializedObject.Fp("defaultValue"));
					GUILayout.Space(2.5f);
					EditorGUILayout.EndHorizontal();
				}
				else
				{
					EditorGUILayout.PropertyField(serializedObject.Fp("defaultValue"));
				}
				
				EditorGUILayout.PropertyField(serializedObject.Fp("resetStates"));
				
				GeneralUtilEditor.DrawHorizontalGUILine();
				
				GUI.backgroundColor = Color.red;
				
				if (GUILayout.Button("Disable default value"))
				{
					serializedObject.Fp("canReset").boolValue = false;
				}
				
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUI.backgroundColor = Color.green;
				
				if (GUILayout.Button("Enable default value"))
				{
					serializedObject.Fp("canReset").boolValue = true;
				}
				
				GUI.backgroundColor = Color.white;
			}
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();
		}


		private static void DrawDataValueEventsSection(SerializedObject serializedObject)
		{
			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1.5f);

			if (serializedObject.Fp("useDataValueEvents").boolValue)
			{
				EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);
				GeneralUtilEditor.DrawHorizontalGUILine();
			
				EditorGUILayout.PropertyField(serializedObject.Fp("onChanged"));

				EditorGUI.BeginDisabledGroup(!serializedObject.Fp("canReset").boolValue);
				EditorGUILayout.PropertyField(serializedObject.Fp("onReset"));
				EditorGUI.EndDisabledGroup();
				
				GUI.backgroundColor = Color.red;
				
				if (GUILayout.Button("Disable data value events"))
				{
					serializedObject.Fp("useDataValueEvents").boolValue = false;
				}
				
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUI.backgroundColor = Color.green;
				
				if (GUILayout.Button("Enable data value events"))
				{
					serializedObject.Fp("useDataValueEvents").boolValue = true;
				}
				
				GUI.backgroundColor = Color.white;
			}
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();
		}
	}
}

#endif