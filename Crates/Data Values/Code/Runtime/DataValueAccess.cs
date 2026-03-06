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

using System.Linq;
using CarterGames.Cart.Data;

namespace CarterGames.Cart.Crates.DataValues
{
    public static class DataValueAccess
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        // A cache of all the assets found...
        private static DataValueIndex indexCache;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets the index for the setup.
        /// </summary>
        private static DataValueIndex Index
        {
            get
            {
                if (indexCache != null) return indexCache;
                indexCache = DataAccess.GetAsset<DataValueIndex>();
                return indexCache;
            }
        }


        /// <summary>
        /// Gets all the values in the index.
        /// </summary>
        public static DataValueAsset[] AllValues => Index.Lookup.Values.ToArray();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets the data value requested.
        /// </summary>
        /// <typeparam name="T">The asset to get.</typeparam>
        /// <returns>The asset if it exists.</returns>
        public static T GetDataValue<T>(string key) where T : DataValueAsset
        {
            if (Index.Lookup.ContainsKey(key))
            {
                return (T)Index.Lookup[key];
            }

            return null;
        }
    }
}

#endif