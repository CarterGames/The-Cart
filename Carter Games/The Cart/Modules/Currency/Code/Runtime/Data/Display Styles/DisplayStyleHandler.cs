#if CARTERGAMES_CART_MODULE_CURRENCY

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

using System;
using System.Collections;
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Events;
using UnityEngine;

namespace CarterGames.Cart.Modules.Currency
{
    [Serializable]
    public class DisplayStyleHandler
    {
        [SerializeField] private CurrencyDisplayStyle displayStyle;
        [SerializeField] private float instantDelay;
        [SerializeField] private float trickleDuration;
        
        
        private MonoBehaviour Target { get; set; }
        private Coroutine DisplayEffectRoutine { get; set; }


        public readonly Evt<double> DisplayedValue = new Evt<double>();
        
        

        public void ProcessDisplayEffect(MonoBehaviour target, AccountTransaction transaction)
        {
            Target = target;
            
            switch (displayStyle)
            {
                case CurrencyDisplayStyle.InstantOnRequest:

                    RuntimeTimer.Set(instantDelay, () => DisplayedValue.Raise(transaction.NewValue));
                    
                    break;
                case CurrencyDisplayStyle.TrickleToTarget:

                    if (DisplayEffectRoutine != null)
                    {
                        target.StopCoroutine(DisplayEffectRoutine);
                    }

                    DisplayEffectRoutine = target.StartCoroutine(Co_TrickleBalanceValue(transaction));
                    
                    break;
                case CurrencyDisplayStyle.Instant:
                default:
                    Debug.Log("Instantly set.");
                    DisplayedValue.Raise(transaction.NewValue);
                    break;
            }
        }


        private IEnumerator Co_TrickleBalanceValue(AccountTransaction transaction)
        {
            var current = transaction.StartingValue;
            var displayed = current;
            var target = transaction.NewValue;

            var timePassed = 0f;
            
            while(timePassed < trickleDuration)
            {
                // Get a linear growing factor between 0 and 1
                // It will take fadeDuration seconds to reach 1
                var factor = timePassed / trickleDuration;

                // Optional easing towards beginning and end to make it a bit "smoother"
                factor = Mathf.SmoothStep(0, 1, factor);

                // Linear interpolate between the start and target value using the factor
                displayed = MathHelper.Lerp(current, target, factor);
                
                // Update the display with the displayed amount
                // using F0 displays it rounded to int
                DisplayedValue.Raise(displayed);

                // Increase by the time passed since last frame
                timePassed += Time.deltaTime;
                
                // Tell Unity to "pause" here, render this frame and
                // continue from here in the next frame
                yield return null;
            }

            // To be sure to end with the exact value set the target fix here
            // This also covers the case for fadeDuration <= 0f
            DisplayedValue.Raise(target);

            // If we achieve to reach this we don't need the reference anymore
            DisplayEffectRoutine = null;
        }
    }
}

#endif