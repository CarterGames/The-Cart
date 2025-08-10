/*
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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace CarterGames.Cart.Core
{
    /// <summary>
    /// A money formatter that formats to a generic number setup.
    /// </summary>
    public sealed class MoneyFormatterGeneric : IMoneyFormatter
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IMoneyFormatter Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Formats the entry.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The formatted string.</returns>
        public string Format(double value)
        {
            return FormatValue(value);
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The builder to use in this script.
        /// </summary>
        private static readonly StringBuilder Builder = new StringBuilder();
        
        
        /// <summary>
        /// The first character to use once bast the Trillion count...
        /// </summary>
        private static readonly int ACharacter = Convert.ToInt32('a');
        
        
        /// <summary>
        /// The unit types to display before going to "aa", "ab", "ac" etc.....
        /// </summary>
        private static readonly Dictionary<int, string> StandardUnits = new Dictionary<int, string>
        {
            {0, ""},
            {1, "K"},
            {2, "M"},
            {3, "B"},
            {4, "T"}
        };
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static string GetSuffix(double value)
        {
            var n = (int)Math.Log(value, 1000);
            
            if (n < StandardUnits.Count)
            {
                return StandardUnits[n];
            }
            else
            {
                var unitInt = n - StandardUnits.Count;
                var secondUnit = unitInt % 26;
                var firstUnit = unitInt / 26;
                return Convert.ToChar(firstUnit + ACharacter) + Convert.ToChar(secondUnit + ACharacter).ToString();
            }
        }
        
        
        /// <summary>
        /// Formats the string into a currency style number...
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The currency string.</returns>
        private static string FormatValue(double value)
        {
            if (value < 1) return "0";
            if (value < 1000) return value.ToString("F0");
            
            var valueToFormat = value / Math.Pow(1000, Math.Floor(Math.Log(value, 1000)));
            var toReadFrom = valueToFormat.ToString("F2").Split('.')[0];
            
            if (toReadFrom.Length >= 3)
            {
                return valueToFormat.ToString("F0") + GetSuffix(value);
            }
            
            if (toReadFrom.Length >= 1)
            {
                if (toReadFrom.Length > 1)
                {
                    return $"{(valueToFormat.ToString("F1").Split('.')[0])}.{valueToFormat.ToString("F2").SplitAndGetLastElement('.')[0]}{GetSuffix(value)}".Replace(".0", string.Empty);
                }
                
                return (valueToFormat.ToString("F2") + GetSuffix(value)).Replace(".00", string.Empty);
            }
            
            return value.ToString("F0");
        }
    }
}