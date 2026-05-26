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


		/// <summary>
		/// Converts the date time entered into a Utc Unix Epoch timestamp (in long form to avoid the 2038 issue).
		/// </summary>
		/// <param name="dateTime">The date time to convert.</param>
		/// <returns>The Unix Epoch Utc of the entered time.</returns>
		public static long ToUnixEpoch(this DateTime dateTime)
		{
			return (long) dateTime.ToUniversalTime().Subtract(DateTimeHelper.UnixEpoch).TotalSeconds;
		}
	}
}