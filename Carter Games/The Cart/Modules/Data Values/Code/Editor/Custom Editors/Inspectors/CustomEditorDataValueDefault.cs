#if CARTERGAMES_CART_MODULE_DATAVALUES && UNITY_EDITOR

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

namespace CarterGames.Cart.Modules.DataValues.Editor
{
	public static class CustomEditorDataValueDefault
	{
		public static void DrawInspector(SerializedObject serializedObject, bool forceIndent = false)
		{
			EditorGUILayout.Space(5f);
			
			GeneralUtilEditor.DrawSoScriptSection(serializedObject.targetObject);

			EditorGUILayout.Space(5f);
			
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
				
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(12.5f);
				EditorGUILayout.PropertyField(serializedObject.Fp("defaultValue"));
				GUILayout.Space(2.5f);
				EditorGUILayout.EndHorizontal();
				
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