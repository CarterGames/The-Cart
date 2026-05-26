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
    /// A animation curve transition for panels
    /// </summary>
    public class PanelTransitionCurve : PanelTransition
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private Transform target;
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private float speed = 4f;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The speed of the transition.
        /// </summary>
        private float TransitionSpeed => 1f / speed;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Coroutines
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Runs the transition.
        /// </summary>
        /// <param name="fadeIn">Is it a fade in?</param>
        protected override IEnumerator Co_Transition(bool fadeIn)
        {
            var elapsedTime = 0f;
            var progress = 0f;
            
            if (fadeIn)
            {
                while (elapsedTime < TransitionSpeed)
                {
                    elapsedTime += TransitionDeltaTime;
                    progress = elapsedTime / TransitionSpeed;
                    target.transform.localScale = Vector3.one * curve.Evaluate(progress);
                    yield return null;
                }
                
                CompletedEvt.Raise();
                target.transform.localScale = Vector3.one;
                TransitionRoutine = null;
            }
            else
            {
                while (elapsedTime < TransitionSpeed)
                {
                    elapsedTime += TransitionDeltaTime;
                    progress = elapsedTime / TransitionSpeed;
                    target.transform.localScale = Vector3.one * (curve.Evaluate(1f - progress));
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