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

using System.Collections.Generic;
using CarterGames.Cart.Core;

namespace CarterGames.Cart.Modules.RuntimeConsole
{
	/// <summary>
	/// Stores a history of the commands sent to the console.
	/// </summary>
	public static class CommandHistory
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private static readonly List<string> PreviousCommands = new List<string>();

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Gets the amount of entries in the history log.
		/// </summary>
		public static int HistoryCount => PreviousCommands.Count;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// Tries to get an entry in the history.
		/// </summary>
		/// <param name="lastOffset">The offset from the last entry to get.</param>
		/// <param name="entry">The entry found.</param>
		/// <returns>If it was successful or not.</returns>
		public static bool TryGetHistory(int lastOffset, out string entry)
		{
			entry = string.Empty;
			
			if (!PreviousCommands.IsWithinIndex(HistoryCount - lastOffset)) return false;
			
			entry = PreviousCommands[HistoryCount - lastOffset];
			return true;
		}
		

		/// <summary>
		/// Adds an entry to the history.
		/// </summary>
		/// <param name="input">The input to add.</param>
		public static void AddToHistory(string input)
		{
			PreviousCommands.Add(input);
		}
		
		
		/// <summary>
		/// Clears the history when called.
		/// </summary>
		public static void ClearHistory()
		{
			PreviousCommands.Clear();
		}
	}
}

#endif