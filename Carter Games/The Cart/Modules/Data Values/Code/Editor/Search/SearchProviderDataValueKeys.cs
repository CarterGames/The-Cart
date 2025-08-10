#if CARTERGAMES_CART_MODULE_DATAVALUES && UNITY_EDITOR

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
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Core.Editor;

namespace CarterGames.Cart.Modules.DataValues.Editor.Search
{
	public class SearchProviderDataValueKeys : SearchProvider<string>
	{
		private static SearchProviderDataValueKeys Instance;

		protected override string ProviderTitle => "Select Data Value Key";
		public override bool HasOptions => AssetDatabaseHelper.GetAllInstancesInProject<DataValueAsset>().Any();


		public override List<SearchGroup<string>> GetEntriesToDisplay()
		{
			var list = new List<SearchGroup<string>>();
			var items = new List<SearchItem<string>>();
			
			foreach (var asset in AssetDatabaseHelper.GetAllInstancesInProject<DataValueAsset>())
			{
				if (ToExclude.Contains(asset.Key)) continue;
				items.Add(SearchItem<string>.Set(asset.Key, asset.Key));
			}

			list.Add(new SearchGroup<string>(items));
			
			return list;
		}


		public static SearchProviderDataValueKeys GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderDataValueKeys>();
			}

			return Instance;
		}
	}
}

#endif