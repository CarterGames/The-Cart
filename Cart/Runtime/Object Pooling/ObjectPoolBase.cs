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

using System.Collections.Generic;
using CarterGames.Cart.Logs;
using UnityEngine;

namespace CarterGames.Cart
{
    /// <summary>
    /// A base class for an object pool.
    /// </summary>
    /// <typeparam name="T">The type to pool.</typeparam>
    public abstract class ObjectPoolBase<T>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        protected readonly List<T> memberObjects;
        protected readonly HashSet<T> unavailableObjects;
        protected HashSet<T> freeObjects;
        protected readonly GameObject prefab;
        protected readonly Transform parent;
        protected bool startActive;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets if the pool is initialized.
        /// </summary>
        public bool IsInitialized => memberObjects != null;


        /// <summary>
        /// Gets/Sets if the pool should auto-expand. Def = true.
        /// </summary>
        public bool ShouldExpand { get; set; } = true;
        
        
        /// <summary>
        /// Gets all the members of the pool.
        /// </summary>
        public List<T> AllMembers => memberObjects;
        
        
        /// <summary>
        /// Gets all the in use members of the pool.
        /// </summary>
        public HashSet<T> AllInUse => unavailableObjects;
        
        
        /// <summary>
        /// Gets all the free members of the pool.
        /// </summary>
        public IReadOnlyCollection<T> FreeMembers => freeObjects;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Creates a new pool.
        /// </summary>
        /// <param name="prefab">The prefab to pool.</param>
        /// <param name="parent">The parent of the pool objects.</param>
        /// <param name="initialCount">The initial count of objects in the pool.</param>
        /// <param name="startActive">Should the elements be active when spawned?</param>
        protected ObjectPoolBase(GameObject prefab, Transform parent, int initialCount = 3, bool startActive = false)
        {
            this.prefab = prefab;
            this.parent = parent;

            this.startActive = startActive;

            memberObjects = new List<T>();
            unavailableObjects = new HashSet<T>();
            freeObjects = new HashSet<T>();
            
            Initialize(initialCount, startActive);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Initializes the pool when called.
        /// </summary>
        /// <param name="initialCount">The initial count of objects in the pool.</param>
        /// <param name="startActive">Should the elements be active when spawned?</param>
        protected abstract void Initialize(int initialCount, bool startActive);
        
        
        /// <summary>
        /// Creates a new member of the pool when called.
        /// </summary>
        /// <returns>The newly created pool member.</returns>
        protected abstract T Create();

        
        /// <summary>
        /// Assigns a new member of the pool to be used. Creates a new member if needed and is allowed to expand.
        /// </summary>
        /// <returns>The assigned member.</returns>
        public virtual T Assign()
        {
            foreach (var t in memberObjects)
            {
                if (unavailableObjects.Contains(t)) continue;
                unavailableObjects.Add(t);
                freeObjects.Remove(t);
                return t;
            }

            if (!ShouldExpand)
            {
                CartLogger.Log<LogCategoryCart>("No free member objects to return.", typeof(ObjectPoolBase<>));
                return default;
            }
            
            var newMember = Create();
            unavailableObjects.Add(newMember);
            return newMember;
        }


        /// <summary>
        /// Returns a member back to the pool for re-use.
        /// </summary>
        /// <param name="member">The member to return.</param>
        public virtual void Return(T member)
        {
            unavailableObjects?.Remove(member);
            freeObjects?.Add(member);
        }
        
        
        /// <summary>
        /// Resets all pool members to be inactive by returning any active.
        /// </summary>
        public void Reset()
        {
            foreach (var inUseMember in unavailableObjects)
            {
                if (inUseMember == null) continue;
                Return(inUseMember);
            }
            
            unavailableObjects?.Clear();
            freeObjects = new HashSet<T>(AllMembers);
        }
    }
}