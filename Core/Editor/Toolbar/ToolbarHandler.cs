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
using CarterGames.Cart.Core.Management;
using CarterGames.Cart.ThirdParty;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Editor
{
	/// <summary>
	/// Handles the toolbar elements used in the editor.
	/// </summary>
	[InitializeOnLoad]
	public static class ToolbarHandler
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		// Caches all the toolbar elements.
		private static readonly IEnumerable<IToolbarElementRight> CacheToolBarElements;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Constructor
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Auto-initialized the toolbar handler when called.
		/// </summary>
		static ToolbarHandler()
		{
			CacheToolBarElements = AssemblyHelper.GetClassesOfType<IToolbarElementRight>().OrderBy(t => t.RightOrder);

			foreach (var element in CacheToolBarElements)
			{
				element.Initialize();
			}
			
			ToolbarExtender.RightToolbarGUI.Remove(OnRightGUI);
			ToolbarExtender.RightToolbarGUI.Add(OnRightGUI);
		}
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Draws buttons on the right GUI of the toolbar.
		/// </summary>
		private static void OnRightGUI()
		{
			GUILayout.FlexibleSpace();
			
			foreach (var element in CacheToolBarElements)
			{
				element.OnRightGUI();
			}
		}
	}
}