#if CARTERGAMES_CART_MODULE_PARAMETERS && UNITY_EDITOR

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
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management;

namespace CarterGames.Cart.Modules.Parameters.Editor
{
	public class SearchProviderParameterKeys : SearchProvider<string>
	{
		private static SearchProviderParameterKeys Instance;
		private bool cacheHasOptions = false;
		private bool hasSetCache = false;

		private static List<string> IgnoreTypes = new List<string>()
		{
			typeof(ParameterGeneric<>).FullName
		};
		
		protected override string ProviderTitle => "Select Parameter";
		public override bool HasOptions
		{
			get
			{
				if (hasSetCache) return cacheHasOptions;
				cacheHasOptions = AssemblyHelper.CountClassesOfType<Parameter>(false) > 2;
				hasSetCache = true;
				return true;
			}
		}


		public override List<SearchGroup<string>> GetEntriesToDisplay()
		{
			var list = new List<SearchGroup<string>>();
			var items = new List<SearchItem<string>>();
			
			foreach (var type in AssemblyHelper.GetClassesNamesOfType<Parameter>(false))
			{
				if (IgnoreTypes.Contains(type.FullName)) continue;
				items.Add(SearchItem<string>.Set(type.Name, type.FullName));
			}

			list.Add(new SearchGroup<string>(items));
			
			return list;
		}


		public static SearchProviderParameterKeys GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderParameterKeys>();
			}

			return Instance;
		}
	}
}

#endif