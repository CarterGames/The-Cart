#if CARTERGAMES_CART_MODULE_RUNTIMECONSOLE

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
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using CarterGames.Cart.Core.Events;
using UnityEngine;

namespace CarterGames.Cart.Modules.RuntimeConsole
{
	/// <summary>
	/// Handles user access to the console from the game.
	/// </summary>
	public sealed class ConsoleAccess : MonoBehaviour
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private bool ListeningState { get; set; }

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Events
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		public static readonly Evt Requested = new Evt();

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Unity Events
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private void OnEnable()
		{
			ConsoleDisplay.Closed.AddAnonymous("CloseToggleInput", () => ToggleListeningForInput(true));
			ToggleListeningForInput(true);
		}


		private void OnDestroy()
		{
			ConsoleDisplay.Closed.RemoveAnonymous("CloseToggleInput");
		}
		
		
		private void OnGUI()
		{
			if (!ListeningState) return;
			if (Event.current.type != EventType.KeyDown) return;
			if (Event.current.keyCode != KeyCode.BackQuote) return;
			
			Requested.Raise();
			ListeningState = false;
		}
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private void ToggleListeningForInput(bool state)
		{
			ListeningState = state;
		}
	}
}

#endif