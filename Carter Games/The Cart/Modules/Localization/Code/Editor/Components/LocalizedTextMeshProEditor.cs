#if CARTERGAMES_CART_MODULE_LOCALIZATION

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

using CarterGames.Cart.Core.Management.Editor;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization.Editor
{
	[CustomEditor(typeof(LocalizedTextMeshPro))]
	public class LocalizedTextMeshProEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			GeneralUtilEditor.DrawMonoScriptSection(target as LocalizedTextMeshPro);

			EditorGUILayout.Space(2.5f);
			DrawSetupOptions();
			EditorGUILayout.Space(2.5f);
			DrawReferencesOptions();
		}


		private void DrawReferencesOptions()
		{
			EditorGUILayout.BeginVertical("HelpBox");

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("References", EditorStyles.boldLabel);
			GUI.backgroundColor = Color.yellow;
			if (GUILayout.Button("Try Get References", GUILayout.Width(140)))
			{
				serializedObject.Fp("label").objectReferenceValue ??=
					((LocalizedTextMeshPro)target).GetComponentInChildren<TMP_Text>();

				serializedObject.ApplyModifiedProperties();
				serializedObject.Update();
			}
			GUI.backgroundColor = Color.white;
			EditorGUILayout.EndHorizontal();
			
			GeneralUtilEditor.DrawHorizontalGUILine();
			
			EditorGUILayout.PropertyField(serializedObject.Fp("label"));
			
			EditorGUILayout.EndVertical();
		}
		

		private void DrawSetupOptions()
		{
			EditorGUILayout.BeginVertical("HelpBox");
			
			EditorGUILayout.BeginHorizontal();
            
			EditorGUILayout.LabelField("Setup", EditorStyles.boldLabel);

			var isValid = !string.IsNullOrEmpty(serializedObject.Fp("locId").stringValue);

			GUI.backgroundColor = isValid ? Color.green : Color.red;
			GUILayout.Label(isValid ? ModuleManager.TickIcon : ModuleManager.CrossIcon, new GUIStyle("minibutton"), GUILayout.Width(25));
			GUI.backgroundColor = Color.white;
            
			EditorGUILayout.EndHorizontal();
			
			GeneralUtilEditor.DrawHorizontalGUILine();
			
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(serializedObject.Fp("locId"));
			
			if (EditorGUI.EndChangeCheck())
			{
				serializedObject.ApplyModifiedProperties();
				serializedObject.Update();
			}
			
			EditorGUILayout.EndVertical();
		}
	}
}

#endif