#if CARTERGAMES_CART_MODULE_PARAMETERS

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
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Events;
using CarterGames.Cart.Core.Logs;
using CarterGames.Cart.Core.Management;
using UnityEngine;

namespace CarterGames.Cart.Modules.Parameters
{
	/// <summary>
	/// The main manager class for the parameters' system.
	/// </summary>
	public static class ParametersManager
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private static Dictionary<string, Parameter> AllParamsLookup;
		private static List<Parameter> AllParams;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Gets if the parameters manager is initialized.
		/// </summary>
		public static bool IsInitialized { get; private set; }

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Events
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Raises when the parameters manager is initialized.
		/// </summary>
		public static Evt InitializedEvt = new Evt();
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods (Internal)
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Initializes the parameters system for use.
		/// </summary>
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitializeParameters()
		{
			if (IsInitialized) return;
			
			AllParams = AssemblyHelper.GetClassesOfType<Parameter>(false).ToList();
			AllParamsLookup = new Dictionary<string, Parameter>();
			
			foreach (var parameter in AllParams) 
			{
				if (parameter.GetType().IsAbstract) continue;
				parameter.Initialize();
				AllParamsLookup.Add(parameter.Key, parameter);
			}
			
			MonoEvents.OnDestroy.Add(DisposeOfParameters);

			IsInitialized = true;
			InitializedEvt.Raise();
			
			CartLogger.Log<LogCategoryParameters>("Parameters initialized.");
		}
		
		
		/// <summary>
		/// Disposes of the system when closing the game.
		/// </summary>
		private static void DisposeOfParameters()
		{
			MonoEvents.OnDestroy.Remove(DisposeOfParameters);
			
			foreach (var parameter in AllParams) 
			{
				parameter.Dispose();
			}

			AllParamsLookup.Clear();
			AllParams.Clear();
			
			CartLogger.Log<LogCategoryParameters>("Parameters disposed of.");
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods (Public API)
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Tries to get a parameter by its key.
		/// </summary>
		/// <param name="key">The key to look for.</param>
		/// <param name="parameter">The found parameter or null if none found.</param>
		/// <returns>If the process was successful.</returns>
		public static bool TryGetParameter(string key, out Parameter parameter)
		{
			parameter = GetParameter(key);
			return parameter != null;
		}
		

		/// <summary>
		/// Gets A parameter by its key.
		/// </summary>
		/// <remarks>
		///	If uncertain of the key existing, use TryGetParameter() instead.
		/// </remarks>
		/// <param name="key">The key to look for.</param>
		/// <returns>The found parameter or null if none found.</returns>
		public static Parameter GetParameter(string key)
		{
			if (!AllParamsLookup.TryGetValue(key, out var value))
			{
				CartLogger.LogWarning<LogCategoryParameters>($"Unable to find parameter with the key {key}.");
				return null;
			}
			
			return value;
		}
	}
}

#endif