#if CARTERGAMES_CART_CRATE_PARAMETERS

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
using CarterGames.Cart;
using CarterGames.Cart.Events;
using CarterGames.Cart.Logs;
using CarterGames.Cart.Management;
using UnityEngine;

namespace CarterGames.Cart.Crates.Parameters
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
			
			AllParams = AssemblyHelper.GetClassesOfType<Parameter>().ToList();
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