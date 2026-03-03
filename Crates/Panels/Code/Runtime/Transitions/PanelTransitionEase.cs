#if CARTERGAMES_CART_CRATE_PANELS && CARTERGAMES_CART_CRATE_EASING

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
using CarterGames.Cart.Crates.Easing;
using UnityEngine;

namespace CarterGames.Cart.Crates.Panels
{
    /// <summary>
    /// A easing transition for panels
    /// </summary>
    public class PanelTransitionEase : PanelTransition
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private Transform target;
        [SerializeField] private OutEaseData outEase;
        [SerializeField] private InEaseData inEase;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Coroutines
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Runs the transition.
        /// </summary>
        /// <param name="fadeIn">Is it a fade in?</param>
        protected override IEnumerator Co_Transition(bool fadeIn)
        {
            var elapsedTime = 0d;
            var progress = 0d;
            
            if (fadeIn)
            {
                while (elapsedTime < outEase.easeDuration)
                {
                    elapsedTime += TransitionDeltaTime;
                    progress = elapsedTime / outEase.easeDuration;
                    target.transform.localScale = Vector3.one * (float)Ease.ReadValue(outEase, progress);
                    yield return null;
                }
                
                CompletedEvt.Raise();
                target.transform.localScale = Vector3.one;
                TransitionRoutine = null;
            }
            else
            {
                while (elapsedTime < inEase.easeDuration)
                {
                    elapsedTime += TransitionDeltaTime;
                    progress = elapsedTime / inEase.easeDuration;
                    var easeValue = 1 - (float)Ease.ReadValue(inEase, progress);
                    if (easeValue >= 0) target.transform.localScale = Vector3.one * easeValue;
                    yield return null;
                }
                
                CompletedEvt.Raise();
                target.transform.localScale = Vector3.zero;
                TransitionRoutine = null;
            }
        }
    }
}

#endif