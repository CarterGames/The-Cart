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
using CarterGames.Cart.Core.Events;
// using CarterGames.Cart.Modules.TimeScales;
using UnityEngine;

namespace CarterGames.Cart.Modules.Panels
{
    public abstract class PanelTransition : MonoBehaviour
    {
        [SerializeField] private bool useUnscaledTime;
        // [SerializeField] private bool useCustomInstance;
        // private CustomTimeInstance timeInstance;
        
        protected Coroutine TransitionRoutine;
        protected bool IsRunning => TransitionRoutine != null;


        private float DeltaTime => Time.deltaTime; // useCustomInstance ? timeInstance.DeltaTime : Time.deltaTime;
        private float UnscaledDeltaTime => Time.unscaledDeltaTime; // useCustomInstance ? timeInstance.DeltaTime : Time.unscaledDeltaTime;


        protected float TransitionDeltaTime => useUnscaledTime ? UnscaledDeltaTime : DeltaTime;


        public readonly Evt Completed = new Evt();
        
        
        public void TransitionIn()
        {
            if (IsRunning)
            {
                StopCoroutine(TransitionRoutine);
            }
            
            TransitionRoutine = StartCoroutine(Co_Transition(true));
        }

        
        public void TransitionOut()
        {
            if (IsRunning)
            {
                StopCoroutine(TransitionRoutine);
            }
            
            TransitionRoutine = StartCoroutine(Co_Transition(false));
        }
        
        
        protected abstract IEnumerator Co_Transition(bool fadeIn);
    }
}

#endif