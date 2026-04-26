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
using CarterGames.Cart.Logs;

namespace CarterGames.Cart
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
                CartLogger.LogError<CartLogs>("Instance is null. Please ensure the instance is initialised.", typeof(Instance<T>));
            }
            
            return instance.Value;
        }
    }
}