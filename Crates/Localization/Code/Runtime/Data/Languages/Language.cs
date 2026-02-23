#if CARTERGAMES_CART_CRATE_LOCALIZATION

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
using System.Globalization;
using UnityEngine;

namespace CarterGames.Cart.Crates.Localization
{
	/// <summary>
	/// Defines a language the system can use.
	/// </summary>
	[Serializable]
	public struct Language : IEquatable<Language>
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		[SerializeField] private string code;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// The display name of the language on GUI etc.
		/// </summary>
		public string DisplayName => CultureInfo.GetCultureInfoByIetfLanguageTag(code).DisplayName;
		
		
		/// <summary>
		/// The code for the language which is used in the backend.
		/// </summary>
		public string Code => code;

		
		/// <summary>
		/// The default language (en-US) to use when no others are set.
		/// </summary>
		public static Language Default => new Language("en-US");

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Constructors
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Creates a new language when called.
		/// </summary>
		/// <param name="code">The code for the language.</param>
		public Language(string code)
		{
			this.code = code;
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   IEquatable
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public bool Equals(Language other)
		{
			return code == other.code;
		}

		public override bool Equals(object obj)
		{
			return obj is Language other && Equals(other);
		}

		public override int GetHashCode()
		{
			return (code != null ? code.GetHashCode() : 0);
		}
	}
}

#endif