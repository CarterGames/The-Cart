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
	[CustomEditor(typeof(LocalizedAudioSource))]
	public class LocalizedAudioSourceEditor : CustomInspector
	{
		protected override string[] HideProperties { get; }
		

		protected override void DrawInspectorGUI()
		{
			EditorGUILayout.Space(2.5f);
			DrawSetupOptions();
		}


		private void DrawSetupOptions()
		{
			EditorGUILayout.BeginVertical("HelpBox");
			
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(serializedObject.Fp("updateOnStart"));
            
			GeneralUtilEditor.DrawHorizontalGUILine();
			
			EditorGUILayout.PropertyField(serializedObject.Fp("locId"));
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(serializedObject.Fp("source"));
			
			if (serializedObject.Fp("source").objectReferenceValue == null)
			{
				GUI.backgroundColor = Color.yellow;
				
				if (GUILayout.Button("Try Assign", GUILayout.Width("Try Assign".GUIWidth())))
				{
					serializedObject.Fp("source").objectReferenceValue =
						((LocalizedAudioSource) target).GetComponent<AudioSource>();
				}
				
				GUI.backgroundColor = Color.white;
			}
			
			EditorGUILayout.EndHorizontal();
			
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