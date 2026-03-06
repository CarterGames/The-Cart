#if CARTERGAMES_CART_CRATE_DATAVALUES && UNITY_EDITOR

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
using System.Text;
using CarterGames.Cart.Events;
using CarterGames.Cart.Management;
using CarterGames.Cart.Crates.DataValues.Events;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Crates.DataValues.Editor
{
	public sealed class DataValuesSearchProvider: ScriptableObject, ISearchWindowProvider
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static readonly StringBuilder Builder = new StringBuilder();

        private static readonly Type[] DefaultIgnoredClasses = new Type[4]
        {
            typeof(DataVariable<>),
            typeof(DataValueList<>),
            typeof(DataValueDictionary<,>),
            typeof(DataValueEventBase)
        };

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Raises when an entry is selected.
        /// </summary>
        public static readonly Evt<SearchTreeEntry> OnSearchTreeSelectionMade = new Evt<SearchTreeEntry>();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The elements to exclude from the search provider.
        /// </summary>
        public static List<string> ToExclude { get; }  = new List<string>();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   ISearchWindowProvider Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Creates the search GUI when called.
        /// </summary>
        /// <param name="context">The window ctx.</param>
        /// <returns>A list of entries to show.</returns>
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var searchList = new List<SearchTreeEntry>();
            
            // The group that names the search window popup when searching...
            searchList.Add(new SearchTreeGroupEntry(new GUIContent("Select Data Value Type"), 0));


            foreach (var assetType in AssemblyHelper.GetClassesNamesOfType<DataValueAsset>().Reverse())
            {
                if (ToExclude.Contains(assetType.Name)) continue;
                if (DefaultIgnoredClasses.Contains(assetType)) continue;
                if (assetType.ContainsGenericParameters) continue;
                
                Builder.Clear();
                Builder.Append(" ");
                Builder.Append(assetType.Name);
                
                searchList.Add(new SearchTreeEntry(GUIContent.none)
                {
                    level = 1,
                    content = new GUIContent(Builder.ToString()),
                    userData = assetType
                });
            }

            return searchList;
        }


        /// <summary>
        /// Runs when an entry is selected.
        /// </summary>
        /// <param name="searchTreeEntry">The selected tree entry.</param>
        /// <param name="context">The context window.</param>
        /// <returns></returns>
        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            // Select the searched clip in the library & just show that result in the window.
            OnSearchTreeSelectionMade.Raise(searchTreeEntry);
            return true;
        }
    }
}

#endif