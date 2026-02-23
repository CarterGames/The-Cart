#if CARTERGAMES_CART_CRATE_CURRENCY && UNITY_EDITOR

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
using TMPro;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Currency.Editor
{
	[CustomEditor(typeof(CurrencyDisplay), true)]
	public class CurrencyDisplayEditor : CustomInspector
	{
		protected override string[] HideProperties { get; }
		

		protected override void DrawInspectorGUI()
		{
			EditorGUILayout.BeginVertical("HelpBox");
			
			EditorGUILayout.Space(1f);
			
			EditorGUILayout.PropertyField(serializedObject.Fp("accountId"));
			EditorGUILayout.PropertyField(serializedObject.Fp("formatter"));
			DisplayStyleEditor.DrawStyleSettings(serializedObject.Fp("displayStyle"));

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(serializedObject.Fp("label"));

			if (serializedObject.Fp("label").objectReferenceValue == null)
			{
				GUI.backgroundColor = Color.yellow;

				if (GUILayout.Button("Try Get Reference", GUILayout.Width(140)))
				{
					serializedObject.Fp("label").objectReferenceValue ??=
						((CurrencyDisplay) target).GetComponentInChildren<TMP_Text>();

					serializedObject.ApplyModifiedProperties();
					serializedObject.Update();
				}

				GUI.backgroundColor = Color.white;
			}
			
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.Space(1f);
			
			EditorGUILayout.EndVertical();
		}
	}
}

#endif