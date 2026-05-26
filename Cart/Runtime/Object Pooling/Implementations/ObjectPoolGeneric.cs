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

using UnityEngine;

namespace CarterGames.Cart
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
                freeObjects.Add(comp);
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
            freeObjects.Add(newMember);
            newObj.SetActive(startActive);
            return newMember;
        }
    }
}