﻿#if CARTERGAMES_CART_MODULE_NOTIONDATA && UNITY_EDITOR

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
using CarterGames.Cart.Core.Data;
using UnityEngine;

namespace CarterGames.Cart.Modules.NotionData.Editor
{
    /// <summary>
    /// Handles the settings asset for the notion data module.
    /// </summary>
    [Serializable]
    public sealed class DataAssetSettingsNotionData : DataAsset
    {
	    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
	    |   Fields
	    ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

	    [SerializeField] private NotionApiVersion apiVersion;
	    [SerializeField] private NotionApiReleaseVersion apiReleaseVersion;
	    [SerializeField] [Range(2, 25)] private int downloadTimeout = 10;

	    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
	    |   Properties
	    ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

	    /// <summary>
        /// The Notion release API version to use.
        /// </summary>
        public NotionApiReleaseVersion NotionAPIReleaseVersion => apiReleaseVersion;


	    /// <summary>
        /// The Notion API version to use.
        /// </summary>
        public NotionApiVersion NotionApiVersion => apiVersion;
	    
	    
	    /// <summary>
	    /// The time required to wait until a download attempt will fail automatically.
	    /// </summary>
	    public int DownloadTimeout => downloadTimeout;
    }
}

#endif