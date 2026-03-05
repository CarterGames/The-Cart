#if CARTERGAMES_CART_CRATE_HIERARCHYDECORATORS && UNITY_EDITOR

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

using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Hierarchy
{
    /// <summary>
    /// (EDITOR ONLY) Handles the settings for a hierarchy header object.
    /// </summary>
    public sealed class HierarchySeparatorSettings : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField, ColorUsage(false)] private Color32 backgroundColor;
        [SerializeField] private bool fullWidth;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The background color of the hierarchy line.
        /// </summary>
        public Color32 BackgroundColor => backgroundColor;


        /// <summary>
        /// Defines if the label should take up the full hierarchy width.
        /// </summary>
        public bool FullWidth => fullWidth;


        private void OnValidate()
        {
            if (backgroundColor == Color.clear)
            {
                backgroundColor = EditorGUIUtility.isProSkin
                    ? new Color32(46, 46, 46, 255)
                    : new Color32(184, 184, 184, 255);
            }
        }
    }
}

#endif