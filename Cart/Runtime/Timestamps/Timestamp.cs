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
using UnityEngine;

namespace CarterGames.Cart.Runtime
{
    /// <summary>
    /// A timestamp class to aid with timestamps to dates and back again.
    /// </summary>
    [Serializable]
    public class Timestamp
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private long value;
        [SerializeField] [HideInInspector] private SerializableDateTime dateTime;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The timestamp value.
        /// </summary>
        public long Value
        {
            get => value;
            set
            {
                this.value = value;
                dateTime = new SerializableDateTime(DateTimeHelper.ParseUnixEpochUtc(value));
            }
        }


        /// <summary>
        /// The date time the timestamp converts to.
        /// </summary>
        public DateTime DateTime
        {
            get
            {
                if (dateTime.Year <= 0)
                {
                    dateTime = new SerializableDateTime(DateTimeHelper.ParseUnixEpochUtc(value));
                }
                
                return dateTime;
            }
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Creates a timestamp class from the timestamp.
        /// </summary>
        /// <param name="timestamp">The value to make the timestamp from.</param>
        public Timestamp(long timestamp)
        {
            value = timestamp;
            dateTime = new SerializableDateTime(DateTimeHelper.ParseUnixEpochUtc(value));
        }
        
        
        /// <summary>
        /// Creates a timestamp from the DateTime entered.
        /// </summary>
        /// <param name="dateTime">The value to make the timestamp from.</param>
        public Timestamp(DateTime dateTime)
        {
            value = dateTime.ToUnixEpoch();
            this.dateTime = new SerializableDateTime(dateTime);
        }
    }
}