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