#if CARTERGAMES_CART_MODULE_CONDITIONS && UNITY_EDITOR

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

using System.Linq;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarterGames.Cart.Modules.Conditions.Editor
{
	public static class ConditionEditorHelper
	{
		public static void AddNewCondition()
		{
			var list = ScriptableRef.GetAssetDef<ConditionsIndex>().ObjectRef.Fp("assets").Fpr("list");
			list.InsertIndex(list.arraySize);
			
			var condition = ScriptableObject.CreateInstance<Condition>();
			condition.name = $"{condition.GetType().Name}_{condition.VariantId}";
			
			var path = ScriptableRef.GetAssetDef<ConditionsIndex>().DataAssetPath.Replace(ScriptableRef.GetAssetDef<ConditionsIndex>().DataAssetPath.Split('/').Last(), string.Empty) + "Conditions/";

			if (!AssetDatabase.IsValidFolder(path))
			{
				FileEditorUtil.CreateToDirectory(path);
			}
			
			AssetDatabase.CreateAsset(condition, path + condition.name + ".asset");
			
			list.GetIndex(list.arraySize - 1).Fpr("key").stringValue = condition.VariantId;
			list.GetIndex(list.arraySize - 1).Fpr("value").objectReferenceValue = condition;

			var conditionObj = new SerializedObject(condition);
			
			conditionObj.Fp("id").stringValue = condition.VariantId;
			conditionObj.ApplyModifiedProperties();
			conditionObj.Update();
			
			list.serializedObject.ApplyModifiedProperties();
			list.serializedObject.Update();
			
			AssetDatabase.SaveAssets();
		}
		
		
		
		public static void AddToNewGroup(SerializedObject target, Object toAdd)
		{
			// New group.
			var lastGroup = GetCurrentGroupProperty(target, toAdd);
							
			target.Fp("criteriaList").InsertIndex(target.Fp("criteriaList").arraySize);
			target.Fp("criteriaList").GetIndex(target.Fp("criteriaList").arraySize - 1).Fpr("criteria").InsertIndex(0);
			target.Fp("criteriaList").GetIndex(target.Fp("criteriaList").arraySize - 1).Fpr("groupCheckType").intValue = 1;
			target.Fp("criteriaList").GetIndex(target.Fp("criteriaList").arraySize - 1).Fpr("groupId").stringValue = $"Group {target.Fp("groupsMade").intValue}";
			target.Fp("criteriaList").GetIndex(target.Fp("criteriaList").arraySize - 1).Fpr("groupUuid").intValue = target.Fp("groupsMade").intValue;
			target.Fp("groupsMade").intValue++;
			target.Fp("criteriaList").GetIndex(target.Fp("criteriaList").arraySize - 1).Fpr("criteria").ClearArray();
			target.Fp("criteriaList").GetIndex(target.Fp("criteriaList").arraySize - 1).Fpr("criteria").InsertIndex(0);
			target.Fp("criteriaList").GetIndex(target.Fp("criteriaList").arraySize - 1).Fpr("criteria").GetIndex(0).objectReferenceValue = toAdd;
			
			
			if (lastGroup != null)
			{
				lastGroup.DeleteAndRemoveIndex(lastGroup.GetIndexOf(toAdd));
			}
							
			target.ApplyModifiedProperties();
			target.Update();
		}
		
		
		public static void AddToUngrouped(SerializedObject target, Object toAdd)
		{
			// Add to existing group.
			var lastGroup = GetCurrentGroupProperty(target, toAdd);
			var lastGroupBase = GetCurrentGroupProperty(target, toAdd, false);
							
			target.Fp("baseAndGroup").InsertIndex(target.Fp("baseAndGroup").arraySize);
			target.Fp("baseAndGroup").GetIndex(target.Fp("baseAndGroup").arraySize - 1).objectReferenceValue = toAdd;
			
			if (lastGroup != null)
			{ 
				lastGroup.DeleteAndRemoveIndex(lastGroup.GetIndexOf(toAdd));
				
				if (lastGroup.arraySize <= 0)
				{
					target.Fp("criteriaList").DeleteAndRemoveIndex(GetGroupIndex(target, lastGroupBase));
				}
			}
							
			target.ApplyModifiedProperties();
			target.Update();
		}


		public static void AddToExistingGroup(SerializedObject target, int targetGroupIndex, Object toAdd)
		{
			// Add to existing group.
			var lastGroup = GetCurrentGroupProperty(target, toAdd);
			var data = target.Fp("criteriaList").GetIndex(targetGroupIndex);
							
			if (data.Fpr("criteria").Contains(toAdd)) return;
							
			data.Fpr("criteria").InsertIndex(data.Fpr("criteria").arraySize);
			data.Fpr("criteria").GetIndex(data.Fpr("criteria").arraySize - 1).objectReferenceValue = toAdd;

			if (lastGroup != null)
			{
				lastGroup.DeleteAndRemoveIndex(lastGroup.GetIndexOf(toAdd));
			}
							
			target.ApplyModifiedProperties();
			target.Update();
		}
		

		private static SerializedProperty GetCurrentGroupProperty(SerializedObject target, Object toFind, bool getGroupCriteriaProp = true)
		{
			if (target.Fp("baseAndGroup").Contains(toFind))
			{
				return target.Fp("baseAndGroup");
			}

			for (var i = 0; i < target.Fp("criteriaList").arraySize; i++)
			{
				var group = target.Fp("criteriaList").GetIndex(i);

				if (group.Fpr("criteria").Contains(toFind)) return getGroupCriteriaProp ? group.Fpr("criteria") : group; 
			}

			return null;
		}
		
		
		private static int GetGroupIndex(SerializedObject target, SerializedProperty group)
		{
			for (var i = 0; i < target.Fp("criteriaList").arraySize; i++)
			{
				if (SerializedProperty.DataEquals(group, target.Fp("criteriaList").GetIndex(i))) return i;
			}

			return -1;
		}
	}
}

#endif