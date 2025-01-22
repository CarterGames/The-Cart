#if CARTERGAMES_CART_MODULE_LOCALIZATION && UNITY_EDITOR

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
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Editor;

namespace CarterGames.Cart.Modules.Localization.Editor
{
	public sealed class SearchProviderLanguages : SearchProvider<Language>
	{
		private static SearchProviderLanguages Instance;

		protected override string ProviderTitle => "Select Language";


		public override List<SearchGroup<Language>> GetEntriesToDisplay()
		{
			var list = new List<SearchGroup<Language>>();
			var items = new List<SearchItem<Language>>();
			
			foreach (var language in DataAccess.GetAsset<DataAssetDefinedLanguages>().Languages.OrderBy(t => t.DisplayName))
			{
				items.Add(SearchItem<Language>.Set(language.DisplayName, language));
			}

			list.Add(new SearchGroup<Language>(items));
			
			return list;
		}


		public static SearchProviderLanguages GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderLanguages>();
			}

			return Instance;
		}
	}
}

#endif