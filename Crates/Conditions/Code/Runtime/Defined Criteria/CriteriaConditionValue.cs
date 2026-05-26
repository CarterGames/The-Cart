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
using CarterGames.Cart.Events;
using CarterGames.Cart.Logs;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions
{
	/// <summary>
	/// A criteria for checking another condition's Valid value.
	/// </summary>
	[Serializable]
	public sealed class CriteriaConditionValue : Criteria
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		[SerializeField] [SelectCondition] private Condition targetConditionObject;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		protected override bool Valid
		{
			get
			{
				if (targetCondition == targetConditionObject)
				{
					CartLogger.LogError<ConditionsLogs>(
						"CriteriaConditionValue cannot be the same as the condition the criteria is on.",
						GetType());
					return false;
				}
				
				return targetConditionObject.IsTrue;
			}
		}
		
		public override string SearchProviderGroup => "Conditions";

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public override void OnInitialize(Evt stateChanged)
		{
			if (targetCondition == targetConditionObject) return;
			targetConditionObject.EvaluationChangedEvt.Add(Refresh);

			return;

			void Refresh(bool state)
			{
				stateChanged.Raise();
			}
		}
	}
}

#endif
