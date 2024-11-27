#if CARTERGAMES_CART_MODULE_CONDITIONS

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
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Reflection;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Conditions.Editor
{
	[CustomEditor(typeof(Condition))]
	public class ConditionCustomInspector : CustomInspector, IAssetEditorReload
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


		private void OnCriteriaDeleted()
		{
			for (var i = 0; i < serializedObject.Fp("baseAndGroup").arraySize; i++)
			{
				if (serializedObject.Fp("baseAndGroup").GetIndex(i).objectReferenceValue != null) continue;
				serializedObject.Fp("baseAndGroup").DeleteAndRemoveIndex(i);
			}
			
			for (var i = 0; i < serializedObject.Fp("criteriaList").arraySize; i++)
			{
				for (var j = 0; j < serializedObject.Fp("criteriaList").GetIndex(i).Fpr("criteria").arraySize; j++)
				{
					if (serializedObject.Fp("criteriaList").GetIndex(i).Fpr("criteria").GetIndex(j).objectReferenceValue !=
					    null) continue;
					serializedObject.Fp("criteriaList").GetIndex(i).Fpr("criteria").DeleteAndRemoveIndex(j);
				}
			}

			serializedObject.ApplyModifiedProperties();
			serializedObject.Update();
		}

		
		
		public void OnEditorReloaded()
		{
			ConditionEditorEvents.CriteriaDeletedFromInspector.Add(OnCriteriaDeleted);
		}
	}
}

#endif