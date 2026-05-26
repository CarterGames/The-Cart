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
using System.Linq;
using System.Text;
using CarterGames.Cart.Data;
using CarterGames.Cart.Events;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions
{
	/// <summary>
	/// A container for a condition asset.
	/// </summary>
	[Serializable]
	public sealed class Condition : DataAsset
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		[SerializeField] [HideInInspector] private bool isExpanded;
		[SerializeField] private string uid;
		[SerializeField] private string id;
		[SerializeField] private List<CriteriaGroup> criteriaList;
		[NonSerialized] private bool lastEvaluatedState;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// The id of the condition.
		/// </summary>
		public string Id => id;

		
		/// <summary>
		/// Gets if the condition is valid or not.
		/// </summary>
		public bool IsTrue => lastEvaluatedState;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Events
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Raises when the conditions criteria have changed.
		/// </summary>
		public readonly Evt RefreshedEvt = new Evt();


		/// <summary>
		/// Raises when the condition evaluation state is changed.
		/// </summary>
		public readonly Evt<bool> EvaluationChangedEvt = new Evt<bool>();


		/// <summary>
		/// Raises when the conditions criteria have changed internally before signalling the public RefreshedEvt event.
		/// </summary>
		private readonly Evt CriteriaRefreshedEvt = new Evt();
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// Initializes the condition when called.
		/// </summary>
		public new void Initialize()
		{
			foreach (var group in criteriaList)
			{
				foreach (var criteria in group.Criteria)
				{
					if (criteria == null) continue;
					criteria.OnInitialize(CriteriaRefreshedEvt);
				}
			}

			CriteriaRefreshedEvt.Add(OnCriteriaRefreshed);
			OnCriteriaRefreshed();
		}


		/// <summary>
		/// Handles updating the condition evaluated state when a criteria is refreshed.
		/// </summary>
		private void OnCriteriaRefreshed()
		{
			var result = EvaluateCondition();

			if (result == lastEvaluatedState)
			{
				RefreshedEvt.Raise();
				return;
			}

			lastEvaluatedState = result;

			RefreshedEvt.Raise();
			EvaluationChangedEvt.Raise(IsTrue);
		}


		/// <summary>
		/// Returns the evaluation of the condition.
		/// </summary>
		private bool EvaluateCondition()
		{
			return criteriaList.All(t => t is {IsTrue: true});
		}


		/// <summary>
		/// Returns a full string summary of the condition's status.
		/// </summary>
		public string PrintStatus()
		{
			var builder = new StringBuilder();
			builder.Append($"Id: {Id}");
			builder.AppendLine();
			builder.Append($"Uid: {uid}");
			builder.AppendLine();
			builder.Append($"Evaluates as: {IsTrue}");
			builder.AppendLine();
			
			foreach (var group in criteriaList)
			{
				builder.AppendLine();
				builder.Append($"Criteria group ({group.CheckType}) evaluates as {group.IsTrue}");
				builder.AppendLine();

				foreach (var criteria in group.Criteria)
				{
					if (criteria == null) continue;
					builder.Append($"{criteria.GetType().Name} ({criteria.DisplayName}) evaluates as {criteria.IsCriteriaValid}");
					builder.AppendLine();
				}
			}

			return builder.ToString();
		}
	}
}

#endif
