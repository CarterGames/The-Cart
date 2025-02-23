#if CARTERGAMES_CART_MODULE_CONDITIONS && UNITY_EDITOR

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
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Reflection;
using CarterGames.Cart.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Conditions.Editor
{
	[CustomEditor(typeof(Condition))]
	public class ConditionCustomInspector : CustomInspector
	{
		private int TotalCriteria
		{
			get
			{
				var total = serializedObject.Fp("baseAndGroup").arraySize;
				
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
				.GetProperty("IsValid", BindingFlags.Public | BindingFlags.Instance)
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
		}
	}
}

#endif