#if CARTERGAMES_CART_MODULE_PANELS && CARTERGAMES_CART_MODULE_EASING

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
using CarterGames.Cart.Modules.Easing;
using UnityEngine;

namespace CarterGames.Cart.Modules.Panels
{
    /// <summary>
    /// A easing transition for panels
    /// </summary>
    [AddComponentMenu("Carter Games/The Cart/Modules/Panels/Transitions/Panel Transition Ease")]
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