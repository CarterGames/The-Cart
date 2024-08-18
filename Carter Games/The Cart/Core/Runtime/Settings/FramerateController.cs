/*
* Copyright (c) 2024 Carter Games
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

using CarterGames.Cart.Core.Events;
using CarterGames.Cart.Core.Logs;
using UnityEngine;

namespace CarterGames.Cart.Core
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
				CartLogger.Log<LogCategoryCore>("Framerate too low to set. Must be greater than 0.", typeof(FramerateController));
			}
				
			frameRate = value;
			PlayerPrefs.SetInt(PlayerPrefKey, value);
			Application.targetFrameRate = frameRate;
		}
	}
}