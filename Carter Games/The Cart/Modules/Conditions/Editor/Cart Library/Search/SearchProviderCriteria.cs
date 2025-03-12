﻿#if CARTERGAMES_CART_MODULE_CONDITIONS && UNITY_EDITOR

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

using System;
using System.Collections.Generic;
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management;

namespace CarterGames.Cart.Modules.Conditions
{
	public class SearchProviderCriteria : SearchProvider<Type>
	{
		private static SearchProviderCriteria Instance;

		protected override string ProviderTitle => "Select Criteria";
		
		
		public override List<SearchGroup<Type>> GetEntriesToDisplay()
		{
			var group = new List<SearchGroup<Type>>();
			var entries = new List<SearchItem<Type>>();
			var instances = AssemblyHelper.GetClassesNamesOfType<Criteria>(false);
			
			foreach (var entry in instances)
			{
				entries.Add(SearchItem<Type>.Set(entry.Name.Replace("Criteria", string.Empty).SplitCapitalsWithSpace(), entry));
			}
			
			group.Add(new SearchGroup<Type>(entries));

			return group;
		}
		
		
		public static SearchProviderCriteria GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderCriteria>();
			}

			return Instance;
		}
	}
}

#endif