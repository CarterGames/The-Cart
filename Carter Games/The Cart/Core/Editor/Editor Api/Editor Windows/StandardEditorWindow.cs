using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Editor
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