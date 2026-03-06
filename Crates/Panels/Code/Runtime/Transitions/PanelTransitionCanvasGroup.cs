#if CARTERGAMES_CART_CRATE_PANELS

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

using System.Collections;
using UnityEngine;

namespace CarterGames.Cart.Crates.Panels
{
    /// <summary>
    /// A canvas group fade transition for panels
    /// </summary>
    public class PanelTransitionCanvasGroup : PanelTransition
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeSpeed;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Coroutines
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Runs the transition.
        /// </summary>
        /// <param name="fadeIn">Is it a fade in?</param>
        protected override IEnumerator Co_Transition(bool fadeIn)
        {
            if (fadeIn)
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                
                while (canvasGroup.alpha < 1)
                {
                    canvasGroup.alpha += fadeSpeed * TransitionDeltaTime;
                    yield return null;
                }
                
                CompletedEvt.Raise();
                canvasGroup.alpha = 1;
                TransitionRoutine = null;
            }
            else
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                
                while (canvasGroup.alpha > 0)
                {
                    canvasGroup.alpha -= fadeSpeed * TransitionDeltaTime;
                    yield return null;
                }
                
                CompletedEvt.Raise();
                canvasGroup.alpha = 0;
                TransitionRoutine = null;
            }
        }
    }
}

#endif