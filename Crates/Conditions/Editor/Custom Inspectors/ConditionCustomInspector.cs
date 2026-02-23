#if CARTERGAMES_CART_CRATE_CONDITIONS && UNITY_EDITOR

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

using System.Reflection;
using CarterGames.Cart.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions.Editor
{
	[CustomEditor(typeof(Condition))]
	public class ConditionCustomInspector : CustomInspector
	{
		private int TotalCriteria
		{
			get
			{
				var total = 0;
				
				if (serializedObject.Fp("criteriaList").arraySize <= 0) return total;

				for (var i = 0; i < serializedObject.Fp("criteriaList").arraySize; i++)
				{
					total += serializedObject.Fp("criteriaList").GetIndex(i).Fpr("criteria").arraySize;
				}
	
				return total;
			}
		}


		private bool IsValid =>
			(bool) typeof(Condition)
				.GetProperty("IsTrue", BindingFlags.Public | BindingFlags.Instance)
				!.GetValue(serializedObject.targetObject);


		protected override string[] HideProperties { get; }

		protected override void DrawInspectorGUI()
		{
			EditorGUILayout.Space(5f);
			
			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1.5f);

			EditorGUI.BeginDisabledGroup(true);
			EditorGUILayout.PropertyField(serializedObject.Fp("variantId"));
			EditorGUILayout.PropertyField(serializedObject.Fp("id"), new GUIContent("Condition Display Name"));
			EditorGUILayout.IntField(new GUIContent("Criteria Total"), TotalCriteria);
			
			if (EditorApplication.isPlaying)
			{
				EditorGUILayout.Toggle(new GUIContent("Is Valid?"), IsValid);
			}
			
			EditorGUI.EndDisabledGroup();
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();

			if (GUILayout.Button("Open Conditions Editor Window"))
			{
				EditorWindowConditions.OpenThis();
			}
		}
	}
}

#endif