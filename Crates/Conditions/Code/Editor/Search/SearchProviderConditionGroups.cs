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

using System.Collections.Generic;
using CarterGames.Cart.Editor;
using UnityEditor;

namespace CarterGames.Cart.Crates.Conditions
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
					var uuid = Target.Fp("criteriaList").GetIndex(i).Fpr("groupUuid").stringValue;
					
					entries.Add(SearchItem<int>.Set($"{label} ({uuid})", i));
				}
			}
			
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