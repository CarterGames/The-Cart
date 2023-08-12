using System;


namespace CarterGames.Common.General
{
    /// <summary>
    /// Handles getting referencing for fields etc, mostly to save writing.
    /// </summary>
    public static class CacheRef
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the cache if its not null, or assigns it via the func if it needs assigning.
        /// </summary>
        /// <param name="cache">The cache to edit.</param>
        /// <param name="getAction">The action to get the reference.</param>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <returns>The assigned cache.</returns>
        public static T GetOrAssign<T>(ref T cache, Func<T> getAction)
        {
            if (cache != null) return cache;
            cache = getAction.Invoke();
            return cache;
        }
        
        
        /// <summary>
        /// Gets the cache if its not null, or assigns it via the reference passed in like a new instance of a class etc.
        /// </summary>
        /// <param name="cache">The cache to edit.</param>
        /// <param name="reference">The reference to assign if the cache needs assigning.</param>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <returns>The assigned cache.</returns>
        public static T GetOrAssign<T>(ref T cache, T reference)
        {
            if (cache != null) return cache;
            cache = reference;
            return cache;
        }
    }
}