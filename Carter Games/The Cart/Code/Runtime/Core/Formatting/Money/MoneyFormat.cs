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

namespace CarterGames.Cart.Core
{
    /// <summary>
    /// A formatter class to turn numbers into currency.
    /// </summary>
    public static class MoneyFormat
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static Formatter<IMoneyFormatter> formatter;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static Formatter<IMoneyFormatter> Formatter
        {
            get
            {
                if (formatter != null) return formatter;
                formatter = new Formatter<IMoneyFormatter>();
                return formatter;
            }
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Formats the int as money.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <typeparam name="T">The money formatter type to use.</typeparam>
        /// <returns>The formatted string</returns>
        public static string Format<T>(this int value) where T : IMoneyFormatter
        {
            return Formatter.Get<T>().Format(value);
        }
        
        
        /// <summary>
        /// Formats the float as money.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <typeparam name="T">The money formatter type to use.</typeparam>
        /// <returns>The formatted string</returns>
        public static string Format<T>(this float value) where T : IMoneyFormatter
        {
            return Formatter.Get<T>().Format(value);
        }
        
        
        /// <summary>
        /// Formats the double as money.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <typeparam name="T">The money formatter type to use.</typeparam>
        /// <returns>The formatted string</returns>
        public static string Format<T>(this double value) where T : IMoneyFormatter
        {
            return Formatter.Get<T>().Format(value);
        }
    }
}