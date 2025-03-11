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

using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Editor
{
	/// <summary>
	/// An editor window classes to make utility style windows with ease.
	/// </summary>
	public abstract class UtilityEditorWindow : EditorWindow
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private static UtilityEditorWindow window;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Gets if the window is currently open.
		/// </summary>
		public static bool IsOpen => window != null;



		public static bool IsFocusedOn
		{
			get
			{
				if (window == null) return false;
				return window.hasFocus;
			}
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Opens the window when called.
		/// </summary>
		/// <param name="windowName">The name to show on the window title.</param>
		/// <typeparam name="T">The type the window is.</typeparam>
		public static void Open<T>(string windowName) where T : UtilityEditorWindow
		{
			window = GetWindow<T>(true);
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
	}
}