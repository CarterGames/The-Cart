#if CARTERGAMES_CART_CRATE_CONDITIONS

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

using System;
using System.Collections.Generic;
using CarterGames.Cart.Data;
using CarterGames.Cart.Events;
using CarterGames.Cart.Logs;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions
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
				CartLogger.LogWarning<LogCategoryConditions>("Cannot initialize conditions as the system is not setup correctly in the editor.", typeof(ConditionManager));
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
		public static bool IsTrue(string conditionId)
		{
			if (!IsInitialized) return false;
			return conditions[conditionId].IsTrue;
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