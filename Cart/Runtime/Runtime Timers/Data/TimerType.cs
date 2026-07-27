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
    /// Defines the different timer types supported in the runtime timer setup.
    /// </summary>
    [Serializable]
    public enum TimerType
    {
        /// <summary>
        /// Timer not assigned (DEFAULT)
        /// </summary>
        Unassigned = 0,
        
        /// <summary>
        /// Countdown from a starting value to 0.
        /// </summary>
        Countdown = 1,
        
        /// <summary>
        /// Count up until stopped.
        /// </summary>
        Stopwatch = 2,
    }
}