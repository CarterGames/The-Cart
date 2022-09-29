// ----------------------------------------------------------------------------
// CachedRef.cs
// 
// Description: A modular class to handle caching references of any class or object. 
// ----------------------------------------------------------------------------

using System;
using Scarlet.General.Logs;

namespace Scarlet.ModularComponents
{
    /// <summary>
    /// A modular class to handle caching references of any class or object. 
    /// </summary>
    /// <typeparam name="T">The type to cache.</typeparam>
    public class CachedRef<T>
    {
        private T cache;
        private Func<T> getCacheAction;
        
        
        /// <summary>
        /// Gets the value in the cache.
        /// </summary>
        public T Value
        {
            get
            {
                if (cache != null) return cache;
                
                if (getCacheAction == null)
                {
                    ScarletLogs.Error(typeof(CachedRef<>),"Get action not defined, have you initialised the class?");
                    return default;
                }
                
                cache = getCacheAction.Invoke();

                if (cache != null) return cache;
                
                ScarletLogs.Error(typeof(CachedRef<>),"Cache is still null after get action, is the get action correctly defined?");
                return default;
            }
        }

        
        /// <summary>
        /// Initialises the cache with the function to get the value on first run.
        /// </summary>
        /// <param name="getAction">The function to get the value.</param>
        public CachedRef(Func<T> getAction)
        {
            getCacheAction = getAction;
            cache = Value;
        }

        
        /// <summary>
        /// Sets the get action to the entered function.
        /// </summary>
        /// <param name="getAction">The function to get the value.</param>
        public void SetGetAction(Func<T> getAction)
        {
            getCacheAction = getAction;
        }

        
        /// <summary>
        /// Clears the cache value to its default value.
        /// </summary>
        public void ClearCache()
        {
            cache = default;
        }


        public static implicit operator T(CachedRef<T> cache)
        {
            return cache.Value;
        }
            
    }
}