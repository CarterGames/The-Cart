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
using System.Collections.Generic;
using CarterGames.Cart.Logs;
using UnityEngine;

namespace CarterGames.Cart
{
    /// <summary>
    /// A manager class to handle getting static references to search providers in the project.
    /// </summary>
    public static class SearchProviderManager
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Stores a lookup of all the providers cached at this time.
        /// </summary>
        private static readonly Dictionary<Type, SearchScriptableObject> ProviderLookup = new Dictionary<Type, SearchScriptableObject>();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the provider of the entered type. If not currently in the cache it adds it to it when requested.
        /// </summary>
        /// <typeparam name="T">The search provider type to look for.</typeparam>
        /// <returns>SearchProvider of the type requested</returns>
        public static T GetProvider<T>() where T : SearchScriptableObject
        {
            if (Application.isPlaying)
            {
                CartLogger.LogWarning<CartLogs>($"Cannot get search providers while in play-mode.", typeof(SearchProviderManager));
                return null;
            }
            
            var typeInputted = typeof(T);

            if (ProviderLookup.TryGetValue(typeInputted, out var value))
            {
                return (T)value;
            }

            try
            {
                var result = ScriptableObject.CreateInstance(typeInputted);
                ProviderLookup.Add(typeInputted, (SearchScriptableObject) result);
                return (T) result;
            }
#pragma warning disable 0168
            catch (Exception e)
#pragma warning restore 0168
            {
                CartLogger.LogError<CartLogs>($"Unable to find a search provider of type {typeInputted}.", typeof(SearchProviderManager));
                return null;
            }
        }


        /// <summary>
        /// Tries to get the provider of the entered type. If not currently in the cache it adds it to it when requested.
        /// </summary>
        /// <param name="searchProvider">SearchProvider of the type requested.</param>
        /// <typeparam name="T">The search provider type to look for.</typeparam>
        /// <returns>Successful? (Bool)</returns>
        public static bool TryGetProvider<T>(out T searchProvider) where T : SearchScriptableObject
        {
            if (Application.isPlaying)
            {
                CartLogger.LogWarning<CartLogs>($"Cannot get search providers while in play-mode.", typeof(SearchProviderManager));
                searchProvider = null;
                return false;
            }
            
            searchProvider = GetProvider<T>();
            return searchProvider != null;
        }
    }
}