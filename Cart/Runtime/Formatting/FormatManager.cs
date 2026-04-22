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

namespace CarterGames.Cart
{
    public static class FormatManager
    {
        private static Dictionary<Type, Formatter> formatterLookup;

        public static IReadOnlyDictionary<Type, Formatter> FormatterLookup =>
            CacheRef.GetOrAssign(ref formatterLookup, GetFormatters);


        private static Dictionary<Type, Formatter> GetFormatters()
        {
            var dic = new Dictionary<Type, Formatter>();
            var classes = AssemblyHelper.GetClassesOfType<Formatter>();

            foreach (var entry in classes)
            {
                dic.Add(entry.GetType(), entry);
            }

            return dic;
        }
    }
}