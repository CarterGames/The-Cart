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
using System.Linq;
using CarterGames.Cart.Management;
using CarterGames.Cart.Random;
using CarterGames.Cart.Save;
using UnityEngine;

namespace CarterGames.Cart
{
	/// <summary>
	/// Handles any pre-defined player data a game might need.
	/// </summary>
	public static class PlayerInfo
	{
		private const string PlayerIdSaveKey = "CarterGames_Cart_Core_Player_Id";
		
		
		/// <summary>
		/// Gets if the user has a player id.
		/// </summary>
		public static bool HasPlayerId => !string.IsNullOrEmpty(PlayerId);
		
		
		/// <summary>
		/// A unique id for the player.
		/// </summary>
		public static string PlayerId
		{
			get => CartSaveHandler.Get<string>(PlayerIdSaveKey);
			private set => CartSaveHandler.Set(PlayerIdSaveKey, value);
		}
		

		[RuntimeInitializeOnLoadMethod]
		private static void InitializePlayerInfo()
		{
			if (HasPlayerId) return;

			string playerIntId = "";
			var rngProvider = new UnityRngProvider();
			
			for (var i = 0; i < 16; i++)
			{
				playerIntId += rngProvider.Int(0, 9).ToString();
			}

			PlayerId = $"Player_{playerIntId}_{Guid.NewGuid().ToString()}";
		}
	}
}