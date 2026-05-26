#if CARTERGAMES_CART_CRATE_PARAMETERS

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

namespace CarterGames.Cart.Crates.Parameters
{
	/// <summary>
	/// The base class for a parameter in the parameters setup.
	/// </summary>
	public abstract class Parameter
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// The key for the parameter.
		/// </summary>
		public virtual string Key => GetType().FullName;
		
		
		/// <summary>
		/// Gets if the value is populated.
		/// </summary>
		public abstract bool HasValue { get; }
		
		
		/// <summary>
		/// Gets the value in object form.
		/// </summary>
		public abstract object ValueObject { get; }
		
		
		/// <summary>
		/// Gets the value in string form.
		/// </summary>
		public string ValueString => HasValue 
			? ValueObject.ToString() 
			: string.Empty;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Events
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Raises when the parameter value is updated.
		/// </summary>
		public readonly Evt UpdatedEvt = new Evt();

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Initializes the parameter when called.
		/// </summary>
		public void Initialize() => OnInitialize();
		
		
		/// <summary>
		/// Disposes of the parameter when called.
		/// </summary>
		public void Dispose() => OnDisposedOf();
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods (Overridable)
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Override to add logic on initializing the parameter.
		/// </summary>
		protected virtual void OnInitialize() {}
		
		
		/// <summary>
		/// Override to add logic on disposing the parameter.
		/// </summary>
		protected virtual void OnDisposedOf() {}
	}
}

#endif