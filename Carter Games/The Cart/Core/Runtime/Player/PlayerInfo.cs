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

using System;
using System.Linq;
using CarterGames.Cart.Core.Management;
using CarterGames.Cart.Core.Random;
using CarterGames.Cart.Core.Save;
using UnityEngine;

namespace CarterGames.Cart.Core
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