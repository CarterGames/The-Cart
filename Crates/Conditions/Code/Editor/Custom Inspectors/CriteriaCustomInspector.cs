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

using CarterGames.Cart.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions.Editor
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
			ConditionsSaveHandler.TrySetDirty();
		}
	}
}

#endif