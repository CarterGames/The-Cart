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
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Editor;

namespace CarterGames.Cart.Modules.Conditions.Editor.Search
{
	public class SearchProviderConditionId : SearchProvider<string>
	{
		private static SearchProviderConditionId Instance;

		protected override string ProviderTitle => "Select Condition";


		public override List<SearchGroup<string>> GetEntriesToDisplay()
		{
			var list = new List<SearchGroup<string>>();
			var items = new List<SearchItem<string>>();
			
			foreach (var asset in DataAccess.GetAssets<Condition>())
			{
				if (ToExclude.Contains(asset.Id)) continue;
				items.Add(SearchItem<string>.Set(asset.Id, asset.Id));
			}

			list.Add(new SearchGroup<string>(items));
			
			return list;
		}


		public static SearchProviderConditionId GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderConditionId>();
			}

			return Instance;
		}
	}
}

#endif