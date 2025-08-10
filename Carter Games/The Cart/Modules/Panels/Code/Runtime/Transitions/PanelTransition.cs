#if CARTERGAMES_CART_MODULE_PANELS

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
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections;
using CarterGames.Cart.Core.Events;
using UnityEngine;

namespace CarterGames.Cart.Modules.Panels
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