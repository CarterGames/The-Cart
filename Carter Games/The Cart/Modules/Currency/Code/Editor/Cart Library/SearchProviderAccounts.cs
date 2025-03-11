#if CARTERGAMES_CART_MODULE_CURRENCY && UNITY_EDITOR

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
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Save;
using CarterGames.Cart.ThirdParty;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Currency.Editor
{
	public class SearchProviderAccounts : SearchProvider<string>
	{
		private static SearchProviderAccounts Instance;

		protected override string ProviderTitle => "Select Account";


		private List<string> GetAccountIds()
		{
			var list = new List<string>();
			var data = CartSaveHandler.Get<string>("CartSave_Modules_Currency_Accounts");
            
			if (!string.IsNullOrEmpty(data))
			{
				var jsonData = JSONNode.Parse(data);
				
				if (jsonData.AsArray.Count > 0)
				{
					foreach (var account in jsonData)
					{
						list.Add(account.Value["key"]);
					}
				}
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


		public static void DrawSelectableAccount(SerializedProperty targetProperty)
		{
			if (string.IsNullOrEmpty(targetProperty.stringValue))
			{
				if (GUILayout.Button("Select Account"))
				{
					GetProvider().Open(targetProperty.stringValue);
				}
			}
			else
			{
				EditorGUILayout.BeginHorizontal();
				
				EditorGUI.BeginDisabledGroup(true);
				EditorGUILayout.PropertyField(targetProperty);
				EditorGUI.EndDisabledGroup();

				if (GUILayout.Button("Edit", GUILayout.Width(55)))
				{
					GetProvider().Open(targetProperty.stringValue);
				}
				
				EditorGUILayout.EndHorizontal();
			}
		}
	}
}

#endif