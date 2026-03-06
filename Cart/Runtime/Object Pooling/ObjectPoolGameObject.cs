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
    public sealed class ObjectPoolGameObject : ObjectPoolBase<GameObject>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Initialises the object pool setup.
        /// </summary>
        /// <param name="prefab">The template to pool.</param>
        /// <param name="parent">The parent to assign to.</param>
        /// <param name="initialCount">How many should be spawned by default</param>
        /// <param name="startActive">Should the objects start as active?</param>
        public ObjectPoolGameObject(GameObject prefab, Transform parent, int initialCount, bool startActive) : base(prefab, parent, initialCount, startActive)
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
                obj.SetActive(startActive);
                memberObjects.Add(obj);
                freeObjects.Add(obj);
            }
        }
        

        /// <summary>
        /// Creates a new member when called.
        /// </summary>
        /// <returns>The new member generated.</returns>
        protected override GameObject Create()
        {
            var newMember = Object.Instantiate(prefab, parent);
            memberObjects.Add(newMember);
            freeObjects.Add(newMember);
            newMember.SetActive(startActive);
            return newMember;
        }
    }
}