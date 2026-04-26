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
using CarterGames.Cart.Logs;
using UnityEngine;

namespace CarterGames.Cart
{
    /// <summary>
    /// A MonoBehaviour for using formatters through the inspector.
    /// </summary>
    /// <remarks>Extension methods are advised still for ease of use.</remarks>
    public class FormatterComponent : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] [SelectFormatter] private AssemblyClassDef formatter;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Formats the int into a string with the formatter assigned in the inspector.
        /// </summary>
        /// <param name="value">The value to format</param>
        /// <returns>String</returns>
        public string FormatValue(int value)
        {
            return formatter.GetTypeInstance<Formatter>().Format(value);
        }
        
       
        /// <summary>
        /// Formats the float into a string with the formatter assigned in the inspector.
        /// </summary>
        /// <param name="value">The value to format</param>
        /// <returns>String</returns>
        public string FormatValue(float value)
        {
            return formatter.GetTypeInstance<Formatter>().Format(value);
        }
        

        /// <summary>
        /// Formats the double into a string with the formatter assigned in the inspector.
        /// </summary>
        /// <param name="value">The value to format</param>
        /// <returns>String</returns>
        public string FormatValue(double value)
        {
            return formatter.GetTypeInstance<Formatter>().Format(value);
        }
        
        
        /// <summary>
        /// Formats the Timespan into a string with the formatter assigned in the inspector.
        /// </summary>
        /// <remarks>Only works with Time formatters</remarks>
        /// <param name="value">The value to format</param>
        /// <returns>String</returns>
        public string FormatValue(TimeSpan value)
        {
            if (!formatter.InheritsFrom(typeof(TimeFormatter)))
            {
                CartLogger.LogError<CartLogs>($"[Formatter Component]: Cannot format with type {formatter.StoredType} with timespan, it is not a valid TimeFormatter");
                return string.Empty;
            }
            
            return formatter.GetTypeInstance<TimeFormatter>().Format(value);
        }
    }
}