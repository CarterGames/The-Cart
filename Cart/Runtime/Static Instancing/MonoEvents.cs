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


using CarterGames.Cart.Events;

namespace CarterGames.Cart
{
	/// <summary>
	/// Handles signalling mono behaviour events out for systems to use without a mono base class.
	/// </summary>
	public static class MonoEvents
	{
		/// <summary>
		/// Raises on Mono Awake.
		/// </summary>
		public static readonly Evt Awake = new Evt();
		
		
		/// <summary>
		/// Raises on Mono Start.
		/// </summary>
		public static readonly Evt Start = new Evt();
		
		
		/// <summary>
		/// Raises on Mono OnDestroy (of the mono instance object in DND).
		/// </summary>
		public static readonly Evt OnDestroy = new Evt();
		
		
		/// <summary>
		/// Raises on Application Quit.
		/// </summary>
		public static readonly Evt<bool> ApplicationFocus = new Evt<bool>();
		
		
		/// <summary>
		/// Raises on Application Quit.
		/// </summary>
		public static readonly Evt ApplicationQuit = new Evt();
	}
}