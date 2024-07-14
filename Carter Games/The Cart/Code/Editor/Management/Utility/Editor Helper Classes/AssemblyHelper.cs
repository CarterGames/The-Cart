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
using System.Reflection;

namespace CarterGames.Cart.Core.Management.Editor
{
    public static class AssemblyHelper
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static Assembly[] cartAssembliesCache;
        private static Assembly[] CartAssemblies => CacheRef.GetOrAssign(ref cartAssembliesCache, GetCartAssemblies);
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static Assembly[] GetCartAssemblies()
        {
            return new Assembly[3]
            {
                Assembly.Load("CarterGames.TheCart.Modules"),
                Assembly.Load("CarterGames.TheCart.Editor"),
                Assembly.Load("CarterGames.TheCart.Runtime")
            };
        }


        public static int CountClassesOfType<T>(bool internalCheckOnly = true)
        {
            var assemblies = internalCheckOnly ? CartAssemblies : AppDomain.CurrentDomain.GetAssemblies();
                
            return assemblies.SelectMany(x => x.GetTypes())
                .Count(x => x.IsClass && typeof(ISettingsProvider).IsAssignableFrom(x));
        }
        
        
        public static IEnumerable<T> GetClassesOfType<T>(bool internalCheckOnly = true)
        {
            var assemblies = internalCheckOnly ? CartAssemblies : AppDomain.CurrentDomain.GetAssemblies();

            return assemblies.SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass && typeof(T).IsAssignableFrom(x) && x.FullName != typeof(T).FullName)
                .Select(type => (T)Activator.CreateInstance(type));
        }
    }
}