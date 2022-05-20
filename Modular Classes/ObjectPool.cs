// ----------------------------------------------------------------------------
// ObjectPool.cs
// 
// Description: A modular class to handle basic generic object pooling.
// ----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Erissa.ModularComponents
{
    /// <summary>
    /// A modular class to handle basic generic object pooling.
    /// </summary>
    /// <typeparam name="T">The type to pool.</typeparam>
    public class ObjectPool<T>
    {
        private readonly List<T> memberObjects;
        private readonly HashSet<T> unavailableObjects;
        private readonly GameObject prefab;
        private readonly Transform parent;


        /// <summary>
        /// Checks to see if the pool is initialised.
        /// </summary>
        public bool IsInitialised => memberObjects != null;
        
        
        /// <summary>
        /// Defines whether or not the pool should expand when it is out of member objects.
        /// </summary>
        public bool ShouldExpand { get; set; }


        /// <summary>
        /// Initialises the object pool setup.
        /// </summary>
        /// <param name="prefab">The template to pool.</param>
        /// <param name="parent">The parent to assign to.</param>
        /// <param name="initialCount">How many should be spawned by default</param>
        public ObjectPool(GameObject prefab, Transform parent, int initialCount)
        {
            this.prefab = prefab;
            this.parent = parent;

            memberObjects = new List<T>();
            unavailableObjects = new HashSet<T>();

            for (var i = 0; i < initialCount; i++)
                memberObjects.Add(Object.Instantiate(prefab, parent).GetComponentInChildren<T>(true));
        }


        /// <summary>
        /// Creates a new member when called.
        /// </summary>
        /// <returns>The new member generated.</returns>
        private T Create()
        {
            var newMember = Object.Instantiate(prefab, parent).GetComponentInChildren<T>(true);
            memberObjects.Add(newMember);
            return newMember;
        }
        

        /// <summary>
        /// Gets the first free member or creates a new member to return.
        /// </summary>
        /// <returns>The member.</returns>
        public T Assign()
        {
            foreach (var t in memberObjects)
            {
                if (unavailableObjects.Contains(t)) continue;
                unavailableObjects.Add(t);
                return t;
            }

            if (!ShouldExpand)
            {
                Debug.LogError("* Object Pool * | No free member objects to return.");
                return default;
            }
            
            var newMember = Create();
            unavailableObjects.Add(newMember);
            return newMember;
        }


        /// <summary>
        /// Returns the member to the pool.
        /// </summary>
        /// <param name="member">The member to return.</param>
        public void Return(T member)
        {
            unavailableObjects.Remove(member);
        }


        /// <summary>
        /// Resets the pool to a default state.
        /// </summary>
        public void Reset()
        {
            unavailableObjects.Clear();
        }
    }
}