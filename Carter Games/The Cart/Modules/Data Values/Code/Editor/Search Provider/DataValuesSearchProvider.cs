#if CARTERGAMES_CART_MODULE_DATAVALUES && UNITY_EDITOR

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarterGames.Cart.Core.Events;
using CarterGames.Cart.Core.Management;
using CarterGames.Cart.Modules.DataValues.Events;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Modules.DataValues.Editor
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
            Debug.LogError(searchTreeEntry.userData);
            
            OnSearchTreeSelectionMade.Raise(searchTreeEntry);
            return true;
        }
    }
}

#endif