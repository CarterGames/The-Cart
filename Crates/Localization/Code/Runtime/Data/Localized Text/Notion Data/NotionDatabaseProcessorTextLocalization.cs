#if CARTERGAMES_CART_CRATE_LOCALIZATION && CARTERGAMES_NOTIONDATA

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
using System.Collections.Generic;
using System.Linq;
using CarterGames.NotionData;

namespace CarterGames.Cart.Crates.Localization
{
    /// <summary>
    /// A NotionDatabaseParser for converting Notion Data into TextLocalizationData.
    /// The standard parser will not work in this workflow.
    /// </summary>
    [Serializable]
    public sealed class NotionDatabaseProcessorTextLocalization : NotionDatabaseProcessor
    {
        /// <summary>
        /// Parses the data when called.
        /// </summary>
        /// <param name="result">The result of the download to use.</param>
        /// <returns>The parsed data to set on the asset.</returns>
        public override List<object> Process<T>(NotionDatabaseQueryResult result)
        {
            var list = new List<object>();
            
            foreach (var row in result.Rows)
            {
                var entries = new List<LocalizationEntry<string>>();

                foreach (var k in row.DataLookup.Keys.Where(t => t != "id"))
                {
                    var valueData = row.DataLookup[k];
                    entries.Add(new LocalizationEntry<string>(row.DataLookup[k].PropertyName, valueData.RichText().Value));
                }
                
                list.Add(new LocalizationData<string>(row.DataLookup["id"].RichText().Value, entries));
            }

            return list;
        }
    }
}

#endif