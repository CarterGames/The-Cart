#if CARTERGAMES_CART_CRATE_CURRENCY && UNITY_EDITOR

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
using System.Linq;
using CarterGames.Cart.Data;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Save;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace CarterGames.Cart.Crates.Currency.Editor
{
	public class SearchProviderAccounts : SearchProvider<string>
	{
		private static SearchProviderAccounts Instance;

		protected override string ProviderTitle => "Select Account";

		public override bool HasOptions => CurrencyManager.AllAccountIds.Count > 0;


		private List<string> GetAccountIds()
		{
			var list = new List<string>();
			
			foreach (var account in CurrencyManager.AllAccountIds)
			{
				list.Add(account);
			}

			foreach (var defAccount in DataAccess.GetAsset<DataAssetDefaultAccounts>().DefaultAccounts)
			{
				if (list.Contains(defAccount.Key)) continue;
				list.Add(defAccount.Key);
			}

			return list;
		}


		public override List<SearchGroup<string>> GetEntriesToDisplay()
		{
			var list = new List<SearchGroup<string>>();
			var entries = new List<SearchItem<string>>();

			var options = GetAccountIds().Where(t => !ToExclude.Contains(t));
			
			foreach (var entry in options)
			{
				entries.Add(SearchItem<string>.Set(entry, entry));
			}
			
			entries.Add(SearchItem<string>.Set("[Clear Selection]", string.Empty));
			
			list.Add(new SearchGroup<string>(string.Empty, entries));
			
			return list;
		}


		public static SearchProviderAccounts GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderAccounts>();
			}

			return Instance;
		}
	}
}

#endif