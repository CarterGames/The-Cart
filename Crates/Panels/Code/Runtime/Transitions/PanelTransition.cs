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
using CarterGames.Cart.Events;
using UnityEngine;

namespace CarterGames.Cart.Crates.Panels
{
    /// <summary>
    /// The base class for panel transition methods.
    /// </summary>
    public abstract class PanelTransition : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private bool useUnscaledTime;
        protected Coroutine TransitionRoutine;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets if the transition is currently running.
        /// </summary>
        protected bool IsRunning => TransitionRoutine != null;
        
        
        /// <summary>
        /// Gets the transition time to adjust by.
        /// </summary>
        protected float TransitionDeltaTime => useUnscaledTime ?  Time.unscaledDeltaTime : Time.deltaTime;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Raises when the transition is completed.
        /// </summary>
        public readonly Evt CompletedEvt = new Evt();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Transitions in the transition.
        /// </summary>
        public void TransitionIn()
        {
            if (IsRunning)
            {
                StopCoroutine(TransitionRoutine);
            }
            
            TransitionRoutine = StartCoroutine(Co_Transition(true));
        }

        
        /// <summary>
        /// Transitions out the transition.
        /// </summary>
        public void TransitionOut()
        {
            if (IsRunning)
            {
                StopCoroutine(TransitionRoutine);
            }
            
            TransitionRoutine = StartCoroutine(Co_Transition(false));
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Coroutines
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Runs the transition.
        /// </summary>
        /// <param name="fadeIn">Is it a fade in?</param>
        protected abstract IEnumerator Co_Transition(bool fadeIn);
    }
}

#endif