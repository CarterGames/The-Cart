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

using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Management;
using CarterGames.Cart.ThirdParty;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Editor
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