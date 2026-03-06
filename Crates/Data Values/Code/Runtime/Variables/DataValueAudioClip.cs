#if CARTERGAMES_CART_CRATE_DATAVALUES

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

namespace CarterGames.Cart.Crates.DataValues
{
	[Serializable]
	[CreateAssetMenu(fileName = "Data Value AudioClip", menuName = "Carter Games/The Cart/Crates/Data Values/Variables/Data Value AudioClip", order = 0)]
	public class DataValueAudioClip : DataVariable<AudioClip> {}
}

#endif