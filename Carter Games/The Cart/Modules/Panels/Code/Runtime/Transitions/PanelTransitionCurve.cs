#if CARTERGAMES_CART_MODULE_PANELS

/*
 * Copyright (c) 2024 Carter Games
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
using UnityEngine;

namespace CarterGames.Cart.Modules.Panels
{
    [AddComponentMenu("Carter Games/The Cart/Modules/Panels/Transitions/Animation Curve")]
    public class PanelTransitionCurve : PanelTransition
    {
        [SerializeField] private Transform target;
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private float speed = 4f;


        private float TransitionSpeed => 1f / speed;


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
                
                Completed.Raise();
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
                
                Completed.Raise();
                target.transform.localScale = Vector3.zero;
                TransitionRoutine = null;
            }
        }
    }
}

#endif