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

using UnityEngine;

namespace CarterGames.Cart.Core
{
    /// <summary>
    /// A modular class to handle basic generic object pooling.
    /// </summary>
    /// <typeparam name="T">The type to pool.</typeparam>
    public sealed class ObjectPoolGeneric<T> : ObjectPoolBase<T>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public ObjectPoolGeneric(GameObject prefab, Transform parent, int initialCount = 3, bool startActive = false) : base(prefab, parent, initialCount)
        { }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Initializes the pool when called.
        /// </summary>
        /// <param name="initialCount">The initial count of objects in the pool.</param>
        /// <param name="startActive">Should the elements be active when spawned?</param>
        protected override void Initialize(int initialCount, bool startActive)
        {
            for (var i = 0; i < initialCount; i++)
            {
                var obj = Object.Instantiate(prefab, parent);
                var comp = obj.GetComponentInChildren<T>(true);
                
                obj.SetActive(startActive);
                memberObjects.Add(comp);
            }
        }

        
        /// <summary>
        /// Creates a new member when called.
        /// </summary>
        /// <returns>The new member generated.</returns>
        protected override T Create()
        {
            var newObj = Object.Instantiate(prefab, parent);
            var newMember = newObj.GetComponentInChildren<T>(true);
            memberObjects.Add(newMember);
            newObj.SetActive(startActive);
            return newMember;
        }
    }
}