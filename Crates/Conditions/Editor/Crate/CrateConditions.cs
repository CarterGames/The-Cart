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

namespace CarterGames.Cart.Crates
{
	/// <summary>
	/// The definition for the conditions' crate.
	/// </summary>
	public sealed class CrateConditions : Crate
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
		/// <summary>
		/// The name of the crate.
		/// </summary>
		public override string CrateName => "Conditions";

		
		/// <summary>
		/// A description of what the crate does.
		/// </summary>
		public override string CrateDescription =>
			"An editor system to define modular boolean checks into the project entirely scriptable object based.";

		
		/// <summary>
		/// The author of the crate.
		/// </summary>
		public override string CrateAuthor => CrateConstants.CarterGamesAuthor;
	}
}