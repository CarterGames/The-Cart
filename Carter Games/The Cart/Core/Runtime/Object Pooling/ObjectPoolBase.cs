﻿/*
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

using System.Collections.Generic;
using CarterGames.Cart.Core.Logs;
using UnityEngine;

namespace CarterGames.Cart.Core
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
                CartLogger.Log<LogCategoryCore>("No free member objects to return.", typeof(ObjectPoolBase<>));
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