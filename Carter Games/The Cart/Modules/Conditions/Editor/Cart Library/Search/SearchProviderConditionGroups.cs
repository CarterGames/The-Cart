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

using System.Collections.Generic;
using CarterGames.Cart.Core.Editor;
using UnityEditor;

namespace CarterGames.Cart.Modules.Conditions
{
	public class SearchProviderConditionGroups : SearchProvider<int>
	{
		private static SearchProviderConditionGroups Instance;

		
		public static bool IsInGroup { get; set; }
		protected override string ProviderTitle => "Select group";
		public override bool HasOptions => true;
		private static SerializedObject Target { get; set; }


		public void Open(SerializedObject targetCondition)
		{
			Target = targetCondition;
			Open();
		}
		
		
		public override List<SearchGroup<int>> GetEntriesToDisplay()
		{
			if (Target == null) return new List<SearchGroup<int>>();

			var group = new List<SearchGroup<int>>();
			var entries = new List<SearchItem<int>>();
			
			if (Target.Fp("criteriaList").arraySize > 0)
			{
				for (var i = 0; i < Target.Fp("criteriaList").arraySize; i++)
				{
					var label = Target.Fp("criteriaList").GetIndex(i).Fpr("groupId").stringValue;
					var uuid = Target.Fp("criteriaList").GetIndex(i).Fpr("groupUuid").intValue.ToString();
					
					entries.Add(SearchItem<int>.Set($"{label} ({uuid})", i));
				}
			}

			var nonGroupedOptions = new List<SearchItem<int>> {SearchItem<int>.Set("+ New Group", -1)};

			if (IsInGroup)
			{
				nonGroupedOptions.Add(SearchItem<int>.Set("Ungroup", 0));
			}
			
			group.Add(new SearchGroup<int>(nonGroupedOptions));
			group.Add(new SearchGroup<int>(entries));

			return group;
		}
		
		
		public static SearchProviderConditionGroups GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderConditionGroups>();
			}

			return Instance;
		}
	}
}

#endif