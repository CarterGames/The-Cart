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

using CarterGames.Cart.Data;
using CarterGames.Cart.Management;

namespace CarterGames.Cart.Save
{
	public static class CartSaveHandler
	{
		private static ISaveMethod SaveMethod => CoreSettings!.SaveMethodType;
		

		private static DataAssetCoreRuntimeSettings CoreSettings =>
			DataAccess.GetAsset<DataAssetCoreRuntimeSettings>();


		public static bool HasKey(string key)
		{
			return SaveMethod.HasKey(key);
		}
		
		
		public static T Get<T>(string key, SaveCtx ctx = SaveCtx.ExactValue)
		{
			return SaveMethod.GetValue<T>(key, ctx);
		}
		
		
		public static void Set<T>(string key, T value, SaveCtx ctx = SaveCtx.ExactValue)
		{
			SaveMethod.SetValue(key, value, ctx);
		}
	}
}