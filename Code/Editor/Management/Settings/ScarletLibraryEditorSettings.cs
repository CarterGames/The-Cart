/*
 * Copyright (c) 2018-Present Carter Games
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

using UnityEngine;

namespace Scarlet.Management.Editor
{
    /// <summary>
    /// A data asset containing any editor specific settings for the package.
    /// </summary>
    [CreateAssetMenu(fileName = "Editor Settings Asset", menuName = "Scarlet Library/Management/Setttings (Editor)")]
    public sealed class ScarletLibraryEditorSettings : ScarletLibraryAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // Hierarchy Blocks
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        [SerializeField] private bool isHierarchySeparatorExpanded;
        [SerializeField] private string hierarchyHeaderPrefix = "<---";
        [SerializeField] private string hierarchySeparatorPrefix = "--->";
        [SerializeField] private Color hierarchyHeaderBackgroundColor = Color.gray;
        [SerializeField] private Color hierarchyHeaderTextColor = Color.white;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        // Hierarchy Blocks
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The hierarchy header prefix string.
        /// </summary>
        public string HierarchyHeaderPrefix => hierarchyHeaderPrefix;
        
        
        /// <summary>
        /// The hierarchy separator prefix string.
        /// </summary>
        public string HierarchySeparatorPrefix => hierarchySeparatorPrefix;
        
        
        /// <summary>
        /// The hierarchy header background color.
        /// </summary>
        public Color HierarchyHeaderBackgroundColor => hierarchyHeaderBackgroundColor;
        
        
        /// <summary>
        /// The hierarchy header text color.
        /// </summary>
        public Color HierarchyHeaderTextColor => hierarchyHeaderTextColor;
    }
}