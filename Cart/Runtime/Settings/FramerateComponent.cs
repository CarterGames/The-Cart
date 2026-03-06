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

using CarterGames.Cart.Logs;
using UnityEngine;

namespace CarterGames.Cart
{
	/// <summary>
	/// Handles editing the application framerate.
	/// </summary>
	public sealed class FramerateComponent : MonoBehaviour
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
		
		[SerializeField] private int frameRate = 30;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// Gets the current target framerate set.
		/// </summary>
		public int Framerate => frameRate;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Unity Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
		
		private void Awake()
		{
			FramerateController.TargetFramerate = frameRate;
		}
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
		
		/// <summary>
		/// Sets the framerate when called.
		/// </summary>
		/// <param name="value">The target to set to.</param>
		public void SetFramerateTarget(int value)
		{
			if (value <= 0)
			{
				CartLogger.Log<LogCategoryCart>("Framerate too low to set. Must be greater than 0", typeof(FramerateComponent));
				return;
			}
			
			frameRate = value;
			FramerateController.TargetFramerate = frameRate;
		}
	}
}