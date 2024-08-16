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
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CarterGames.Cart.Modules.RuntimeConsole
{
	/// <summary>
	/// Handles the input for the runtime console.
	/// </summary>
	public sealed class ConsoleInput : MonoBehaviour
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		[SerializeField] private TMP_InputField userInputField;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public static string LastInput { get; private set; }
		private int HistoryIndexOffset { get; set; } = 0;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Events
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public static readonly Evt Changed = new Evt();
		public static readonly Evt Submitted = new Evt();

		private static readonly Evt UpPressed = new Evt();
		private static readonly Evt DownPressed = new Evt();
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Unity Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private void OnEnable()
		{
			userInputField.onEndEdit.RemoveListener(OnUserInputChange);
			userInputField.onEndEdit.AddListener(OnUserInputChange);
			
			userInputField.onSubmit.RemoveListener(OnUserInputSubmit);
			userInputField.onSubmit.AddListener(OnUserInputSubmit);
			
			RuntimeConsoleManager.Run.Add(ClearInput);
			ConsoleDisplay.Opened.Add(OnConsoleOpen);
			ConsoleDisplay.Closed.Add(OnConsoleClosed);
		}


		private void OnDisable()
		{
			userInputField.onEndEdit.RemoveListener(OnUserInputChange);
			userInputField.onSubmit.RemoveListener(OnUserInputSubmit);
			
			RuntimeConsoleManager.Run.Remove(ClearInput);
			ConsoleDisplay.Opened.Remove(OnConsoleOpen);
			ConsoleDisplay.Closed.Remove(OnConsoleClosed);
		}
		
		
		private void OnGUI()
		{
			if (!ConsoleDisplay.IsOpen) return;

			if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.UpArrow)
			{
				UpPressed.Raise();
			}

			if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.DownArrow)
			{
				DownPressed.Raise();
			}
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// Runs on the console has been opened.
		/// </summary>
		private void OnConsoleOpen()
		{
			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(userInputField.gameObject);
			
			UpPressed.Add(OnUpPressed);
			DownPressed.Add(OnDownPressed);
		}


		/// <summary>
		/// Runs on the console has been closed.
		/// </summary>
		private void OnConsoleClosed()
		{
			UpPressed.Remove(OnUpPressed);
			DownPressed.Remove(OnDownPressed);
		}


		/// <summary>
		/// Runs when up is pressed.
		/// </summary>
		private void OnUpPressed()
		{
			HistoryIndexOffset++;

			if (!CommandHistory.TryGetHistory(HistoryIndexOffset, out var entry)) return;
			
			userInputField.SetTextWithoutNotify(entry);
		}
		
		
		/// <summary>
		/// Runs when down is pressed.
		/// </summary>
		private void OnDownPressed()
		{
			HistoryIndexOffset--;
			
			if (!CommandHistory.TryGetHistory(HistoryIndexOffset, out var entry)) return;
			
			userInputField.SetTextWithoutNotify(entry);
		}
		

		/// <summary>
		/// Runs when input has updated from the input field.
		/// </summary>
		/// <param name="data">The string in the field.</param>
		private void OnUserInputChange(string data)
		{
			LastInput = data;
			Changed.Raise();
		}
		

		/// <summary>
		/// Runs when input has submitted the input from the input field.
		/// </summary>
		/// <param name="data">The string in the field.</param>
		private void OnUserInputSubmit(string data)
		{
			LastInput = data;
			Submitted.Raise();
		}


		/// <summary>
		/// Clears the input field for re-use when called.
		/// </summary>
		private void ClearInput()
		{
			userInputField.SetTextWithoutNotify(string.Empty);
			HistoryIndexOffset = 0;
			OnConsoleOpen();
		}
	}
}

#endif