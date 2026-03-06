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
using System.Globalization;
using System.Text;
using UnityEngine;

namespace CarterGames.Cart
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