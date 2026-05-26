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

using System;
using UnityEngine;

namespace CarterGames.Cart.Save
{
	public class SaveMethodBasic : ISaveMethod
	{
		public bool HasKey(string key)
		{
			return PlayerPrefs.HasKey(key);
		}
		

		public T GetValue<T>(string key, SaveCtx ctx = SaveCtx.ExactValue)
		{
			switch (ctx)
			{
				case SaveCtx.ExactValue:
					return RuntimePerUserSettings.GetValue<T>(key);
				case SaveCtx.JsonConvert:
					return JsonUtility.FromJson<T>(RuntimePerUserSettings.GetValue<string>(key));
				default:
					throw new ArgumentOutOfRangeException(nameof(ctx), ctx, null);
			}
		}

		
		public void SetValue<T>(string key, T value, SaveCtx ctx = SaveCtx.ExactValue)
		{
			switch (ctx)
			{
				case SaveCtx.ExactValue:
					RuntimePerUserSettings.SetValue<T>(key, value);
					break;
				case SaveCtx.JsonConvert:
					RuntimePerUserSettings.SetValue<string>(key, JsonUtility.ToJson(value));
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(ctx), ctx, null);
			}
		}
	}
}