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
	public class SearchProviderDataValueAsset : SearchProvider<DataValueAsset>
	{
		private static SearchProviderDataValueAsset Instance;

		protected override string ProviderTitle => "Select Data Value";
		public override bool HasOptions => AssetDatabaseHelper.GetAllInstancesInProject<DataValueAsset>().Any();


		public override List<SearchGroup<DataValueAsset>> GetEntriesToDisplay()
		{
			var list = new List<SearchGroup<DataValueAsset>>();
			var items = new List<SearchItem<DataValueAsset>>();
			
			foreach (var asset in AssetDatabaseHelper.GetAllInstancesInProject<DataValueAsset>())
			{
				if (ToExclude.Contains(asset)) continue;
				items.Add(SearchItem<DataValueAsset>.Set(asset.Key, asset));
			}

			list.Add(new SearchGroup<DataValueAsset>(items));
			
			return list;
		}


		public static SearchProviderDataValueAsset GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderDataValueAsset>();
			}

			return Instance;
		}
	}
}

#endif