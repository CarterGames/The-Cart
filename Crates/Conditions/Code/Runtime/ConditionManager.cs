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
		public static readonly Evt InitializedEvt = new Evt();
		
		
		/// <summary>
		/// Raises when any condition is updated, with the id of the condition as the passed parameter.
		/// </summary>
		public static readonly Evt<string> AnyConditionCriteriaUpdatedEvt = new Evt<string>();


		/// <summary>
		/// Raises when any condition valid state is updated, with the id of the condition as the passed parameter as well as the state it is in.
		/// </summary>
		public static readonly Evt<string, bool> AnyConditionValidStateUpdatedEvt = new Evt<string, bool>();

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
				CartLogger.LogWarning<ConditionsLogs>("Cannot initialize conditions as the system has criteria that are not setup correctly in the editor.", typeof(ConditionManager));
				return;
			}
			
			if (IsInitialized) return;
			
			conditions = DataAccess.GetAsset<ConditionsIndex>().Lookup;

			foreach (var entry in conditions)
			{
				entry.Value.RefreshedEvt.AddAnonymous(entry.Key, () => AnyConditionCriteriaUpdatedEvt.Raise(entry.Key));
				entry.Value.EvaluationChangedEvt.AddAnonymous(entry.Key, (b) => AnyConditionValidStateUpdatedEvt.Raise(entry.Key, b));
				entry.Value.Initialize();
			}
			
			IsInitialized = true;
			InitializedEvt.Raise();
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Registers a listener with a condition state refresh change.
		/// </summary>
		/// <param name="conditionId">The condition id to listen to.</param>
		/// <param name="action">The action to run on the condition being valid.</param>
		public static void SubscribeAnyCriteriaChange(string conditionId, Action action)
		{
			if (!IsInitialized)
			{
				CartLogger.LogError<ConditionsLogs>("Conditions is not initialized! please check that conditions are initialzied before running other logic. This call has failed and the subscription change not applied!.", typeof(ConditionManager));
				return;
			}

			conditions[conditionId].RefreshedEvt.Add(action);
		}


		/// <summary>
		/// Unregisters a listener with a condition state refresh change.
		/// </summary>
		/// <param name="conditionId">The condition id to listen to.</param>
		/// <param name="action">The action to run on the condition being valid.</param>
		public static void UnsubscribeAnyCriteriaChange(string conditionId, Action action)
		{
			if (!IsInitialized)
			{
				CartLogger.LogError<ConditionsLogs>("Conditions is not initialized! please check that conditions are initialzied before running other logic. This call has failed and the subscription change not applied!.", typeof(ConditionManager));
				return;
			}
			conditions[conditionId].RefreshedEvt.Remove(action);
		}


		/// <summary>
		/// Registers a listener with a condition valid state change.
		/// </summary>
		/// <param name="conditionId">The condition id to listen to.</param>
		/// <param name="action">The action to run on the condition being valid.</param>
		/// <param name="runOnListen">Should the listener fire when registering?</param>
		public static void SubscribeValidStateChange(string conditionId, Action<bool> action, bool runOnListen = false)
		{
			if (!IsInitialized)
			{
				CartLogger.LogError<ConditionsLogs>("Conditions is not initialized! please check that conditions are initialzied before running other logic. This call has failed and the subscription change not applied!.", typeof(ConditionManager));
				return;
			}
			conditions[conditionId].EvaluationChangedEvt.Add(action);
			if (!runOnListen) return;
			action?.Invoke(IsTrue(conditionId));
		}
		

		/// <summary>
		/// Unregisters a listener with a condition valid state change.
		/// </summary>
		/// <param name="conditionId">The condition id to listen to.</param>
		/// <param name="action">The action to run on the condition being valid.</param>
		public static void UnsubscribeValidStateChange(string conditionId, Action<bool> action)
		{
			if (!IsInitialized)
			{
				CartLogger.LogWarning<ConditionsLogs>("Conditions is not initialized! please check that conditions are initialzied before running other logic. This call has failed and the subscription change not applied!.", typeof(ConditionManager));
				return;
			}

			conditions[conditionId].EvaluationChangedEvt.Remove(action);

		}


		/// <summary>
		/// Gets if a condition is currently valid or not.
		/// </summary>
		/// <param name="conditionId">The condition id to check.</param>
		/// <returns>If the entered condition id is valid or not.</returns>
		public static bool IsTrue(string conditionId)
		{
			if (!IsInitialized)
			{
				CartLogger.LogError<ConditionsLogs>("Conditions is not initialized! please check that conditions are initialzied before running other logic. This call has failed and will return false as a fallback.", typeof(ConditionManager));
				return false;
			}

			return conditions[conditionId].IsTrue;
		}


		/// <summary>
		/// Clears all listeners off a condition when called.
		/// </summary>
		/// <param name="conditionId">The condition id to clear.</param>
		public static void ClearAllListenersFromCondition(string conditionId)
		{
			if (!IsInitialized)
			{
				CartLogger.LogError<ConditionsLogs>("Conditions is not initialized! please check that conditions are initialzied before running other logic. This call has failed and the subscription changes are not applied!.", typeof(ConditionManager));
				return;
			}

			conditions[conditionId].RefreshedEvt.Clear();
			conditions[conditionId].EvaluationChangedEvt.Clear();
		}
	}
}

#endif
