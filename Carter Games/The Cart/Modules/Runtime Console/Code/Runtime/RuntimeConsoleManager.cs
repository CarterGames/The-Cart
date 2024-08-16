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

using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Events;
using CarterGames.Cart.Core.Logs;
using UnityEngine;

namespace CarterGames.Cart.Modules.RuntimeConsole
{
	/// <summary>
	/// Handles the console setup.
	/// </summary>
	public static class RuntimeConsoleManager
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private static ConsoleDisplay display;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// The display for the console.
		/// </summary>
		public static ConsoleDisplay Display
		{
			get
			{
				if (display != null) return display;
				display = SceneRef.GetComponentFromAllScenes<ConsoleDisplay>();
				return display;
			}
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Events
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Raises when a command is run.
		/// </summary>
		public static readonly Evt Run = new Evt();

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize()
		{
			if (display != null) return;
			var obj = Object.Instantiate(DataAccess.GetAsset<DataAssetRuntimeConsole>().DisplayPrefab);
			obj.name = "[Runtime Console]: Display";
			Object.DontDestroyOnLoad(obj);

			Application.logMessageReceived -= OnUnityLogMessage;
			Application.logMessageReceived += OnUnityLogMessage;
			
			CartLogger.Logged.Add(OnLogMessageMade);

			display = obj.GetComponentInChildren<ConsoleDisplay>();
		}


		/// <summary>
		/// Runs the command entered.
		/// </summary>
		/// <param name="data">The command key to try and run.</param>
		public static void RunCommand(string data)
		{
			CommandHistory.AddToHistory(data);
			
			var command = CommandHandler.Get(data);

			if (!Debug.isDebugBuild && !command.AllowInProduction)
			{
				Display.SendConsoleMessage($"Command \"{data}\" not recognized.");
				Run.Raise();
				return;
			}

			if (command != null)
			{
				Display.SendConsoleMessage(command.OnExecutedMessage());
				command.OnCommandExecuted();
			}
			else
			{
				Display.SendConsoleMessage($"Command \"{data}\" not recognized.");
			}
			
			Run.Raise();
		}


		/// <summary>
		/// Runs when a unity log is sent.
		/// </summary>
		/// <param name="message">The message sent.</param>
		/// <param name="stackTrace">The stack trace for the log.</param>
		/// <param name="logType">The log type.</param>
		private static void OnUnityLogMessage(string message, string stackTrace, LogType logType)
		{
			if (!DataAccess.GetAsset<DataAssetRuntimeConsole>().ShowUnityConsoleMessages) return;
			Display.SendConsoleMessage(message);
		}
		

		/// <summary>
		/// Runs when a cart log is sent.
		/// </summary>
		/// <param name="logType">The log type.</param>
		/// <param name="formattedMessage">The message sent.</param>
		private static void OnLogMessageMade(LogType logType, string formattedMessage)
		{
			if (DataAccess.GetAsset<DataAssetRuntimeConsole>().ShowUnityConsoleMessages) return;
			if (!DataAccess.GetAsset<DataAssetRuntimeConsole>().ShowCartLogs) return;
			Display.SendConsoleMessage(formattedMessage);
		}
	}
}

#endif