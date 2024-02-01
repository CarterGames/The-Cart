/*
 * Copyright (c) 2024 Carter Games
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

#if UNITY_EDITOR

using CarterGames.Cart.General;
using UnityEngine;

namespace CarterGames.Cart.Hierarchy
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