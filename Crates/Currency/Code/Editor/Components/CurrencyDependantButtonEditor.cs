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
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Crates.Currency.Editor
{
	[CustomEditor(typeof(CurrencyDependantButton), true)]
	public class CurrencyDependantButtonEditor : UnityEditor.Editor
	{
		private static readonly string[] ToExcludeFromDraw = new string[10]
		{
			"m_Script", "accountId", "setAmount", "amount", "transactionType", "button", "buttonGraphic", "buttonLabel",
			"affordableLabelColor", "unaffordableLabelColor"
		};


		public override void OnInspectorGUI()
		{
			EditorGUILayout.Space(5f);
			
			GeneralUtilEditor.DrawMonoScriptSection(target as CurrencyDependantButton);
			
			EditorGUILayout.Space(5f);

			EditorGUI.BeginChangeCheck();
			
			DrawAccountOptions();
			EditorGUILayout.Space(5f);
			DrawDisplayOptions();
			EditorGUILayout.Space(5f);
			DrawRemainingOptions();
			
			if (EditorGUI.EndChangeCheck())
			{
				serializedObject.ApplyModifiedProperties();
				serializedObject.Update();
			}
		}


		private void DrawAccountOptions()
		{
			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1f);
			
			EditorGUILayout.PropertyField(serializedObject.Fp("accountId"));
			EditorGUILayout.PropertyField(serializedObject.Fp("setAmount"), new GUIContent("Pre-defined Amount"));

			if (serializedObject.Fp("setAmount").boolValue)
			{
				EditorGUILayout.PropertyField(serializedObject.Fp("amount"));
				EditorGUILayout.PropertyField(serializedObject.Fp("transactionType"));
			}
			
			EditorGUILayout.Space(1f);
			EditorGUILayout.EndVertical();
		}


		private void DrawDisplayOptions()
		{
			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1f);
			
			EditorGUILayout.PropertyField(serializedObject.Fp("button"));
			EditorGUILayout.PropertyField(serializedObject.Fp("buttonGraphic"));
			EditorGUILayout.PropertyField(serializedObject.Fp("buttonLabel"));
			EditorGUILayout.PropertyField(serializedObject.Fp("affordableLabelColor"));
			EditorGUILayout.PropertyField(serializedObject.Fp("unaffordableLabelColor"));
			
			EditorGUILayout.Space(1f);
			EditorGUILayout.EndVertical();
		}


		private void DrawRemainingOptions()
		{
			if (serializedObject.TotalProperties().Equals(ToExcludeFromDraw.Length)) return;
			
			GeneralUtilEditor.DrawHorizontalGUILine();
			EditorGUILayout.Space(2.5f);
			
			DrawPropertiesExcluding(serializedObject, ToExcludeFromDraw);
		}
	}
}

#endif