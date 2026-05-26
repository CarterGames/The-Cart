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

using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Editor
{
	public abstract class StandardEditorWindow : EditorWindow
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private static StandardEditorWindow window;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Gets if the window is currently open.
		/// </summary>
		public static bool IsOpen => window != null;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Opens the window when called.
		/// </summary>
		/// <param name="windowName">The name to show on the window title.</param>
		/// <typeparam name="T">The type the window is.</typeparam>
		public static void Open<T>(string windowName) where T : StandardEditorWindow
		{
			window = GetWindow<T>();
			window.titleContent = new GUIContent(windowName);
			window.Show();
		}


		/// <summary>
		/// Closes the window when called.
		/// </summary>
		public new void Close()
		{
			if (window == null) return;
			((EditorWindow) window).Close();
			window = null;
		}


		public static void RepaintWindow<T>() where T : StandardEditorWindow
		{
			window = GetWindow<T>();
			window.Repaint();
		}
	}
}