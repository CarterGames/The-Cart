﻿#if CARTERGAMES_CART_MODULE_CONDITIONS && CARTERGAMES_CART_MODULE_PANELS

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
using CarterGames.Cart.Core.Events;
using CarterGames.Cart.Modules.Panels;
using UnityEngine;

namespace CarterGames.Cart.Modules.Conditions
{
	/// <summary>
	/// A criteria for checking if a panel is being tracked or not. 
	/// </summary>
	[Serializable]
	public sealed class CriteriaPanelTracked : Criteria
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		[SerializeField] private string panelId;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		protected override bool Valid
		{
			get
			{
				if (string.IsNullOrEmpty(panelId)) return false;
				return PanelTracker.IsTrackingPanel(panelId);
			}
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public override void OnInitialize(Evt stateChanged)
		{
			PanelTracker.PanelTracked.Add(OnPanelTracked);
			PanelTracker.PanelUnTracked.Add(OnPanelTracked);
			return;
			
			void OnPanelTracked(Panel panel)
			{
				stateChanged.Raise();
			}
		}
	}
}

#endif