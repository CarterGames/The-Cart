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

using System;
using System.Collections.Generic;
using System.Linq;

namespace CarterGames.Cart.Core.Management.Editor
{
    public static class SettingsProviderHandler
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static Dictionary<Type, ISettingsProvider> providers;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static int ProvidersInProject
        {
            get
            {
                return AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(x => x.GetTypes()).Count(x => x.IsClass && typeof(ISettingsProvider).IsAssignableFrom(x));
            }
        }
        
        
        public static Dictionary<Type, ISettingsProvider> Providers
        {
            get
            {
                if (providers.IsEmptyOrNull())
                {
                    UpdateCache();
                }

                return providers;
            }
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static void UpdateCache()
        {
            if (providers == null)
            {
                providers = new Dictionary<Type, ISettingsProvider>();
            }
            
            if (providers.Count.Equals(ProvidersInProject)) return;
            
            var found = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass && typeof(ISettingsProvider).IsAssignableFrom(x))
                .Select(type => (ISettingsProvider)Activator.CreateInstance(type)).ToArray();
            
            providers = new Dictionary<Type, ISettingsProvider>();

            foreach (var element in found)
            {
                providers.Add(element.GetType(), element);
            }
        }


        public static ISettingsProvider GetProvider<T>() where T : ISettingsProvider
        {
            if (providers.IsEmptyOrNull())
            {
                UpdateCache();
            }

            if (providers.ContainsKey(typeof(T)))
            {
                return providers[typeof(T)];
            }

            return null;
        }
    }
}