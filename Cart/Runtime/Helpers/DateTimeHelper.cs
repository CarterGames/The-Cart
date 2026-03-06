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

namespace CarterGames.Cart
{
    /// <summary>
    /// A set of helper methods for date time that are not extension methods.
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Used to mimic DateTime.UnixEpoch from newer versions of C# (This lib doesn't base of a version that has the API).
        /// </summary>
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        
        
        /// <summary>
        /// Parses a Unix Epoch Utc timestamp into a date time for use.
        /// </summary>
        /// <param name="timestamp">The timestamp to convert.</param>
        /// <returns>The converted date time.</returns>
        public static DateTime ParseUnixEpochUtc(long timestamp)
        {
            return (UnixEpoch.AddSeconds(timestamp));
        }
    }
}