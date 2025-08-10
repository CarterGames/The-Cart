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

using CarterGames.Cart.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Conditions.Editor
{
	[CustomEditor(typeof(Criteria), true)]
	public sealed class CriteriaCustomInspector : CustomInspector
	{
		protected override string[] HideProperties { get; }

		protected override void DrawInspectorGUI()
		{
			EditorGUILayout.Space(5f);
			
			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1.5f);
			
			EditorGUILayout.LabelField("Criteria", EditorStyles.boldLabel);
			GeneralUtilEditor.DrawHorizontalGUILine();

			EditorGUI.BeginDisabledGroup(true);
			EditorGUILayout.PropertyField(serializedObject.Fp("readInverted"));
			EditorGUI.EndDisabledGroup();
			
			if (EditorApplication.isPlaying)
			{
				EditorGUI.BeginDisabledGroup(true);
				EditorGUILayout.Toggle(new GUIContent("Is valid"), ((Criteria) target).IsCriteriaValid);
				EditorGUI.EndDisabledGroup();
			}

			if (GUILayout.Button("Delete"))
			{
				if (Dialogue.Display("Remove criteria", "Are you sure you want to delete this criteria?", "Delete",
					    "Cancel"))
				{
					AssetDatabase.RemoveObjectFromAsset(target);
					var targetCondition =
						new SerializedObject(serializedObject.Fp("targetCondition").objectReferenceValue);
					DestroyImmediate(target);
					
					OnCriteriaDeleted(targetCondition);
					
					AssetDatabase.SaveAssets();
					AssetDatabase.Refresh();
					return;
				}
			}
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();
		}
		
		
		private void OnCriteriaDeleted(SerializedObject targetCondition)
		{
			for (var i = 0; i < targetCondition.Fp("baseAndGroup").arraySize; i++)
			{
				if (targetCondition.Fp("baseAndGroup").GetIndex(i).objectReferenceValue != null) continue;
				targetCondition.Fp("baseAndGroup").DeleteAndRemoveIndex(i);
			}
			
			for (var i = 0; i < targetCondition.Fp("criteriaList").arraySize; i++)
			{
				for (var j = 0; j < targetCondition.Fp("criteriaList").GetIndex(i).Fpr("criteria").arraySize; j++)
				{
					if (targetCondition.Fp("criteriaList").GetIndex(i).Fpr("criteria").GetIndex(j).objectReferenceValue !=
					    null) continue;
					targetCondition.Fp("criteriaList").GetIndex(i).Fpr("criteria").DeleteAndRemoveIndex(j);
				}
			}

			targetCondition.ApplyModifiedProperties();
			targetCondition.Update();
			
			ConditionsSoCache.ClearCache();
		}
	}
}

#endif