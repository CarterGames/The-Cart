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

using System;
using System.Collections.Generic;
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Events;
using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Cart.Modules.RuntimeConsole
{
	/// <summary>
	/// A class to handle the display for the console.
	/// </summary>
	public sealed class ConsoleDisplay : MonoBehaviour
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		[SerializeField] private Canvas canvas;
		[SerializeField] private GraphicRaycaster graphicRaycaster;

		[SerializeField] private Button closeButton;
		[SerializeField] private Button sendCommandButton;
		
		[SerializeField] private Transform container;
		[SerializeField] private ConsoleLine linePrefab;

		private ObjectPoolGeneric<ConsoleLine> pool;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Gets if the display is initialized.
		/// </summary>
		private bool IsInitialized { get; set; }
		
		
		/// <summary>
		/// Gets if the display is open.
		/// </summary>
		public static bool IsOpen { get; set; }

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Events
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Raises when the display is opened.
		/// </summary>
		public static readonly Evt Opened = new Evt();
		
		
		/// <summary>
		/// Raises when the display is closed.
		/// </summary>
		public static readonly Evt Closed = new Evt();
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Unity Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private void OnEnable()
		{
			pool = new ObjectPoolGeneric<ConsoleLine>(linePrefab.gameObject, container);
			
			closeButton.onClick.RemoveListener(Close);
			closeButton.onClick.AddListener(Close);
			
			sendCommandButton.onClick.RemoveListener(SendCommand);
			sendCommandButton.onClick.AddListener(SendCommand);
			
			ConsoleAccess.Requested.Add(OnOpenRequested);
			ConsoleInput.Submitted.Add(SendCommand);

			IsInitialized = true;
		}


		private void OnDestroy()
		{
			IsInitialized = false;
			
			closeButton.onClick.RemoveListener(Close);
			sendCommandButton.onClick.RemoveListener(SendCommand);
			
			ConsoleAccess.Requested.Remove(OnOpenRequested);
			ConsoleInput.Submitted.Remove(SendCommand);
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Updates the console with a new line.
		/// </summary>
		/// <param name="message">The message to show.</param>
		public void SendConsoleMessage(string message)
		{
			var line = pool.Assign();
			line.transform.SetAsLastSibling();
			line.SetDisplay($"[{DateTime.Now.TimeOfDay.Format<TimeFormatterConsole>()}]", message);
			line.gameObject.SetActive(true);
		}


		/// <summary>
		/// Runs when the display has been requested to open.
		/// </summary>
		private void OnOpenRequested()
		{
			canvas.enabled = true;
			graphicRaycaster.enabled = true;

			IsOpen = true;
			Opened.Raise();
		}


		/// <summary>
		/// Runs when the display has been requested to close.
		/// </summary>
		private void Close()
		{
			canvas.enabled = false;
			graphicRaycaster.enabled = false;
			
			IsOpen = false;
			Closed.Raise();
		}


		/// <summary>
		/// Sends to the command in the input field when called.
		/// </summary>
		private void SendCommand()
		{
			RuntimeConsoleManager.RunCommand(ConsoleInput.LastInput);
		}

		
		/// <summary>
		/// Clears the display of messages when called.
		/// </summary>
		public void ClearDisplay()
		{
			var toClear = new HashSet<ConsoleLine>(pool.AllInUse);
			
			foreach (var displayElement in toClear)
			{
				displayElement.SetDisplay(string.Empty, string.Empty);
				displayElement.gameObject.SetActive(false);
				pool.Return(displayElement);
			}
		}
	}
}

#endif