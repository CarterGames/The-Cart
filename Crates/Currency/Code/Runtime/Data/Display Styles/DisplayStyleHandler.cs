#if CARTERGAMES_CART_CRATE_CURRENCY

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
using System.Collections;
using CarterGames.Cart;
using CarterGames.Cart.Events;
using UnityEngine;

namespace CarterGames.Cart.Crates.Currency
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