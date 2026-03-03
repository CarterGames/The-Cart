#if CARTERGAMES_CART_CRATE_HIERARCHY && UNITY_EDITOR

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

using CarterGames.Cart;
using UnityEngine;

namespace CarterGames.Cart.Crates.Hierarchy
{
    /// <summary>
    /// (EDITOR ONLY) Handles the settings for a hierarchy header object.
    /// </summary>
    public sealed class HierarchyHeaderSettings : MonoBehaviour
    {
	    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
	    |   Fields
	    ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

	    [SerializeField] private string label;
	    [SerializeField] private HierarchyTitleTextAlign textAlign;
	    [SerializeField, ColorUsage(false)] private Color backgroundColor = Color.gray;
	    [SerializeField, ColorUsage(false)] private Color labelColor = Color.white;
	    [SerializeField] private bool boldLabel = true;
	    [SerializeField] private bool fullWidth;

	    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
	    |   Properties
	    ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

	    /// <summary>
        /// The label to show in the hierarchy.
        /// </summary>
        public string Label => label;


	    /// <summary>
        /// The text alignment for the header.
        /// </summary>
        public HierarchyTitleTextAlign TextAlign => textAlign;


	    /// <summary>
        /// The background color of the hierarchy line.
        /// </summary>
        public Color BackgroundColor => backgroundColor.With(a: 255f);


	    /// <summary>
        /// The color of the text label in the hierarchy.
        /// </summary>
        public Color LabelColor => labelColor.With(a: 255f);


	    /// <summary>
        /// Defines if the label should be bold or not.
        /// </summary>
        public bool BoldLabel => boldLabel;


	    /// <summary>
        /// Defines if the label should take up the full hierarchy width.
        /// </summary>
        public bool FullWidth => fullWidth;
    }
}

#endif