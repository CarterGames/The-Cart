#if CARTERGAMES_CART_MODULE_CONDITIONS

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
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Events;
using CarterGames.Cart.Core.Logs;
using UnityEngine;

namespace CarterGames.Cart.Modules.Conditions
{
	/// <summary>
	/// The main manager class for the conditions' system.
	/// </summary>
	public static class ConditionManager
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private static Dictionary<string, Condition> conditions;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Gets if the system is initialized or not.
		/// </summary>
		public static bool IsInitialized { get; private set; }


		/// <summary>
		/// Gets if conditions can be used or not. Is disabled if a criteria class is not setup property.
		/// </summary>
		private static bool CanEnableSystem => CriteriaValidation.AllCriteriaValid();

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Events
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Raises when the system is initialized.
		/// </summary>
		public static readonly Evt Initialized = new Evt();
		
		
		/// <summary>
		/// Raises when any condition is updated, with the id of the condition as the passed parameter.
		/// </summary>
		public static readonly Evt<string> ConditionUpdated = new Evt<string>();

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Initialization
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Initializes the manager automatically.
		/// </summary>
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
		private static void Initialize()
		{
			if (!CanEnableSystem)
			{
				CartLogger.LogWarning<LogCategoryModules>("Cannot initialize conditions as the system is not setup correctly in the editor.", typeof(ConditionManager));
				return;
			}
			
			if (IsInitialized) return;
			
			conditions = DataAccess.GetAsset<ConditionsIndex>().Lookup;

			foreach (var entry in conditions)
			{
				entry.Value.StateChanged.AddAnonymous(entry.Key, () => ConditionUpdated.Raise(entry.Key));
				entry.Value.Initialize();
			}
			
			IsInitialized = true;
			Initialized.Raise();
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Registers a listener with a condition.
		/// </summary>
		/// <param name="conditionId">The condition id to listen to.</param>
		/// <param name="action">The action to run on the condition being valid.</param>
		public static void RegisterListener(string conditionId, Action action)
		{
			if (!IsInitialized) return;
			conditions[conditionId].StateChanged.Add(action);
		}
		
		
		/// <summary>
		/// Unregisters a listener with a condition.
		/// </summary>
		/// <param name="conditionId">The condition id to listen to.</param>
		/// <param name="action">The action to run on the condition being valid.</param>
		public static void UnregisterListener(string conditionId, Action action)
		{
			if (!IsInitialized) return;
			conditions[conditionId].StateChanged.Remove(action);
		}


		/// <summary>
		/// Gets if a condition is currently valid or not.
		/// </summary>
		/// <param name="conditionId">The condition id to check.</param>
		/// <returns>If the entered condition id is valid or not.</returns>
		public static bool IsValid(string conditionId)
		{
			if (!IsInitialized) return false;
			return conditions[conditionId].IsValid;
		}


		/// <summary>
		/// Clears all listeners off a condition when called.
		/// </summary>
		/// <param name="conditionId">The condition id to clear.</param>
		public static void ClearAllListeners(string conditionId)
		{
			if (!IsInitialized) return;
			conditions[conditionId].StateChanged.Clear();
		}
	}
}

#endif