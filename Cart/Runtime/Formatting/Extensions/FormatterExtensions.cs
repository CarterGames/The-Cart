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

using CarterGames.Cart.Logs;

namespace CarterGames.Cart
{
    public static class FormatterExtensions
    {
        public static string Format<T>(this int value) where T : Formatter
        {
            if (FormatManager.FormatterLookup.TryGetValue(typeof(T), out var formatter))
            {
                return formatter.Format(value);
            }
            
            CartLogger.Log<LogCategoryCart>($"Cannot find formatter of type {typeof(T).Name}", typeof(FormatterExtensions));
            return string.Empty;
        }
        
        
        public static string Format<T>(this float value) where T : Formatter
        {
            if (FormatManager.FormatterLookup.TryGetValue(typeof(T), out var formatter))
            {
                return formatter.Format(value);
            }
            
            CartLogger.Log<LogCategoryCart>($"Cannot find formatter of type {typeof(T).Name}", typeof(FormatterExtensions));
            return string.Empty;
        }
        
        
        public static string Format<T>(this double value) where T : Formatter
        {
            if (FormatManager.FormatterLookup.TryGetValue(typeof(T), out var formatter))
            {
                return formatter.Format(value);
            }
            
            CartLogger.Log<LogCategoryCart>($"Cannot find formatter of type {typeof(T).Name}", typeof(FormatterExtensions));
            return string.Empty;
        }
    }
}