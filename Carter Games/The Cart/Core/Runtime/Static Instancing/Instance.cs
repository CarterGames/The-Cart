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
using CarterGames.Cart.Core.Logs;

namespace CarterGames.Cart.Core
{
    /// <summary>
    /// A modular class to handle an instance of another class.
    /// </summary>
    /// <typeparam name="T">The type to instance.</typeparam>
    public class Instance<T> where T : UnityEngine.Object
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The instance value.
        /// </summary>
        public T Value { get; }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Initialises the instance to the value entered. 
        /// </summary>
        /// <param name="typeValue">The instance reference to use.</param>
        /// <param name="dontDestroy">Should the instance be using DoNoDestroyOnLoad().</param>
        public Instance(T typeValue, bool dontDestroy = false)
        {
            Value = typeValue;

            if (!dontDestroy) return;
            UnityEngine.Object.DontDestroyOnLoad(Value);
        }
        
        
        /// <summary>
        /// Initialises the instance via the function entered. 
        /// </summary>
        /// <param name="typeInstance">The function to get the instance reference.</param>
        /// <param name="dontDestroy">Should the instance be using DoNoDestroyOnLoad().</param>
        public Instance(Func<T> typeInstance, bool dontDestroy = false)
        {
            Value = typeInstance.Invoke();

            if (!dontDestroy) return;
            UnityEngine.Object.DontDestroyOnLoad(Value);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Operators
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Implicitly returns the instance so you don't need to use Instance.Value all the time.
        /// </summary>
        /// <param name="instance">The instance to use.</param>
        /// <returns>The instance saved.</returns>
        public static implicit operator T(Instance<T> instance)
        {
            if (instance.Value == null)
            {
                CartLogger.LogError<LogCategoryCore>("Instance is null. Please ensure the instance is initialised.", typeof(Instance<T>));
            }
            
            return instance.Value;
        }
    }
}