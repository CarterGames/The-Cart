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

using CarterGames.Cart.Events;
using CarterGames.Cart.Logs;
using UnityEngine;

namespace CarterGames.Cart
{
	/// <summary>
	/// Handles the frame rate for the game.
	/// </summary>
	public static class FramerateController
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
		
		private const string PlayerPrefKey = "CarterGames_TheCart_FramerateTarget";
		private const int DefaultFramerateTarget = 30;
		private static int frameRate;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
		
		/// <summary>
		/// Gets the target framerate currently set.
		/// </summary>
		public static int TargetFramerate
		{
			get => frameRate;
			set => SetTargetFramerate(value);
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Events
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
		
		/// <summary>
		/// Raises when the framerate is changed.
		/// </summary>
		public static readonly Evt Changed = new Evt();
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Constructors
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
		
		static FramerateController()
		{
			frameRate = PlayerPrefs.HasKey(PlayerPrefKey) 
				? PlayerPrefs.GetInt(PlayerPrefKey) 
				: DefaultFramerateTarget;

			Application.targetFrameRate = TargetFramerate;
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
		
		/// <summary>
		/// Sets the target framerate.
		/// </summary>
		/// <param name="value">The value to set to.</param>
		public static void SetTargetFramerate(int value)
		{
			SetTargetFramerateWithoutNotify(value);
			Changed.Raise();
		}
		
		
		/// <summary>
		/// Sets the target framerate without sending the changed event.
		/// </summary>
		/// <param name="value">The value to set to.</param>
		public static void SetTargetFramerateWithoutNotify(int value)
		{
			if (frameRate <= 0)
			{
				CartLogger.Log<LogCategoryCart>("Framerate too low to set. Must be greater than 0.", typeof(FramerateController));
			}
				
			frameRate = value;
			PlayerPrefs.SetInt(PlayerPrefKey, value);
			Application.targetFrameRate = frameRate;
		}
	}
}