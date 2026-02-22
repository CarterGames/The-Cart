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
* FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

using System;
using System.Globalization;
using UnityEngine;

namespace CarterGames.Cart.Core
{
    /// <summary>
    /// Stored a time value in a readable format.
    /// </summary>
    [Serializable]
    public struct SerializableTime
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        public const long TicksPerMillisecond = 10000;
        public const long TicksPerSecond = 10000000;
        public const long TicksPerMinute = 600000000;
        public const long TicksPerHour = 36000000000;
        public const long TicksPerDay = 864000000000;
        
        [SerializeField] private int days;
        [SerializeField] private int hours;
        [SerializeField] private int minutes;
        [SerializeField] private int seconds;
        [SerializeField] private int milliseconds;
        
        private long ticks;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The days stored in the time value.
        /// </summary>
        public int Days => days;
        
        
        /// <summary>
        /// The hours stored in the time value.
        /// </summary>
        public int Hours => hours;
        
        
        /// <summary>
        /// The minutes stored in the time value.
        /// </summary>
        public int Minutes => minutes;
        
        
        /// <summary>
        /// The seconds stored in the time value.
        /// </summary>
        public int Seconds => seconds;
        
        
        /// <summary>
        /// The milliseconds stored in the time value.
        /// </summary>
        public int Milliseconds => milliseconds;
        
        
        /// <summary>
        /// The ticks stored in the time value.
        /// </summary>
        public long Ticks => GetTicks();

        
        /// <summary>
        /// The total number of days stored.
        /// </summary>
        public double TotalDays => Math.Floor(TimeSpan.FromTicks(Ticks).TotalDays);
        
        
        /// <summary>
        /// The total number of hours stored.
        /// </summary>
        public double TotalHours => Math.Floor(TimeSpan.FromTicks(Ticks).TotalHours);
        
        
        /// <summary>
        /// The total number of minutes stored.
        /// </summary>
        public double TotalMinutes => Math.Floor(TimeSpan.FromTicks(Ticks).TotalMinutes);
        
        
        /// <summary>
        /// The total number of seconds stored.
        /// </summary>
        public double TotalSeconds => Math.Floor(TimeSpan.FromTicks(Ticks).TotalSeconds);
        
        
        /// <summary>
        /// The total number of milliseconds stored.
        /// </summary>
        public double TotalMilliSeconds => TimeSpan.FromTicks(Ticks).TotalMilliseconds;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Operators
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        /// <summary>
        /// Converts the time to a timespan.
        /// </summary>
        /// <param name="time">The time to use.</param>
        /// <returns>The time as a timespan.</returns>
        public static implicit operator TimeSpan(SerializableTime time)
        {
            return TimeSpan.FromTicks(time.Ticks);
        }
        
        
        /// <summary>
        /// Converts the time to a datetime.
        /// </summary>
        /// <param name="time">The time to use.</param>
        /// <returns>The time as a datetime.</returns>
        public static implicit operator DateTime(SerializableTime time)
        {
            var date = new DateTime();
            date = date.AddTicks(time.Ticks);
            return date;
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Returns a SerializableTime setup based on the seconds passed in.
        /// </summary>
        /// <param name="seconds">The seconds to set from.</param>
        /// <returns>SerializableTime</returns>
        public static SerializableTime FromSeconds(double seconds)
        {
            var time = new SerializableTime();
            var timeSpan = TimeSpan.FromSeconds(seconds);

            time.days = timeSpan.Days;
            time.hours = timeSpan.Hours;
            time.minutes = timeSpan.Minutes;
            time.seconds = timeSpan.Seconds;
            time.milliseconds = timeSpan.Milliseconds;
            time.ticks = timeSpan.Ticks;

            return time;
        }
        
        
        /// <summary>
        /// Returns a SerializableTime setup based on the ticks passed in.
        /// </summary>
        /// <param name="ticks">The ticks to set from.</param>
        /// <returns>SerializableTime</returns>
        public static SerializableTime FromTicks(long ticks)
        {
            var time = new SerializableTime();
            var timeSpan = TimeSpan.FromTicks(ticks);

            time.days = timeSpan.Days;
            time.hours = timeSpan.Hours;
            time.minutes = timeSpan.Minutes;
            time.seconds = timeSpan.Seconds;
            time.milliseconds = timeSpan.Milliseconds;
            time.ticks = timeSpan.Ticks;

            return time;
        }
        
        
        /// <summary>
        /// Makes a new serializable time from a timespan value entered.
        /// </summary>
        /// <param name="timeSpan">The timespan to set from.</param>
        public static SerializableTime FromTimeSpan(TimeSpan timeSpan)
        {
            var time = new SerializableTime
            {
                days = timeSpan.Days,
                hours = timeSpan.Hours,
                minutes = timeSpan.Minutes,
                seconds = timeSpan.Seconds,
                milliseconds = timeSpan.Milliseconds,
                ticks = timeSpan.Ticks
            };

            return time;
        }
        
        
        /// <summary>
        /// Makes a new serializable time from a datetime value entered.
        /// </summary>
        /// <param name="dateTime">The datetime to set from.</param>
        public static SerializableTime FromDateTime(DateTime dateTime)
        { 
            var time = new SerializableTime
            {
                days = dateTime.Day,
                hours = dateTime.Hour,
                minutes = dateTime.Minute,
                seconds = dateTime.Second,
                milliseconds = dateTime.Millisecond,
                ticks = dateTime.Ticks
            };

            return time;
        }


        /// <summary>
        /// Gets the ticks based on the time data stored.
        /// </summary>
        /// <returns>The ticks for this data.</returns>
        private long GetTicks()
        {
            if (ticks > 0) return ticks;

            if (milliseconds > 0)
            {
                ticks += milliseconds * TicksPerMillisecond;
            }
            
            if (seconds > 0)
            {
                ticks += seconds * TicksPerSecond;
            }
            
            if (minutes > 0)
            {
                ticks += minutes * TicksPerMinute;
            }
            
            if (hours > 0)
            {
                ticks += hours * TicksPerHour;
            }
            
            if (days > 0)
            {
                ticks += days * TicksPerDay;
            }

            return ticks;
        }


        public override string ToString()
        {
            var date = new DateTime();
            date = date.AddTicks(Ticks);
            return date.ToString(CultureInfo.InvariantCulture);
        }

        
        public string ToString(string format)
        {
            var date = new DateTime();
            date = date.AddTicks(Ticks);
            return date.ToString(format, CultureInfo.InvariantCulture);
        }

        
        public string ToString(IFormatProvider provider)
        {
            var date = new DateTime();
            date = date.AddTicks(Ticks);
            return date.ToString(provider);
        }
        
        
        public string ToString(string format, IFormatProvider provider)
        {
            var date = new DateTime();
            date = date.AddTicks(Ticks);
            return date.ToString(format, provider);
        }
    }
}