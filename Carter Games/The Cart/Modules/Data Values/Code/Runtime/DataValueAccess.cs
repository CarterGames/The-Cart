#if CARTERGAMES_CART_MODULE_DATAVALUES

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

using System.Linq;
using CarterGames.Cart.Core.Data;

namespace CarterGames.Cart.Modules.DataValues
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