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

using System;

namespace CarterGames.Cart.Core
{
	/// <summary>
	/// A set of extension methods for DateTime.
	/// </summary>
	public static class DateTimeExtensions
	{
		/// <summary>
		/// Sets the date specifically in a datetime.
		/// </summary>
		/// <param name="dateTime">The date time to base off.</param>
		/// <param name="year">The year to set.</param>
		/// <param name="month">The month to set.</param>
		/// <param name="day">The day to set.</param>
		/// <returns>A new datetime with the required adjustments.</returns>
		public static DateTime SetDate(this DateTime dateTime, int? year, int? month, int? day)
		{
			return new DateTime(year ?? dateTime.Year, month ?? dateTime.Month, day ?? dateTime.Day, 
				dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond, dateTime.Kind);
		}


		/// <summary>
		/// Set the time specifically in a datetime.
		/// </summary>
		/// <param name="dateTime">The date time to base off.</param>
		/// <param name="hour">The hour to set.</param>
		/// <param name="minute">The minute to set.</param>
		/// <param name="second">The second to set.</param>
		/// <param name="millisecond">The millisecond to set.</param>
		/// <returns>A new datetime with the required adjustments.</returns>
		public static DateTime SetTime(this DateTime dateTime, int? hour, int? minute, int? second, int? millisecond = 0)
		{
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,
				hour ?? dateTime.Hour, minute ?? dateTime.Minute,
				second ?? dateTime.Second, millisecond ?? dateTime.Millisecond, dateTime.Kind);
		}


		/// <summary>
		/// Set the time specifically in a datetime.
		/// </summary>
		/// <param name="dateTime">The date time to base off.</param>
		/// <param name="timeSpan">The timespan to apply.</param>
		/// <returns>A new datetime with the required adjustments.</returns>
		public static DateTime SetTime(this DateTime dateTime, TimeSpan timeSpan)
		{
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,
				timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds, dateTime.Kind);
		}
	}
}