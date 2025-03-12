﻿#if CARTERGAMES_CART_MODULE_NOTIONDATA

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
using UnityEngine;

namespace CarterGames.Cart.Modules.NotionData
{
	/// <summary>
	/// A wrapper base class for converting a notion database property into a sprite by its name.
	/// </summary>
    [Serializable]
    public class NotionDataWrapperSprite : NotionDataWrapper<Sprite>
    {
	    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
	    |   Constructors
	    ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

	    public NotionDataWrapperSprite(string id) : base(id) { }
        
	    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
	    |   Operator
	    ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

	    /// <summary>
	    /// Converts the wrapper to its implementation type.
	    /// </summary>
	    /// <param name="dataWrapper">The wrapper to convert.</param>
	    /// <returns>The value of the wrapper.</returns>
	    public static implicit operator Sprite(NotionDataWrapperSprite dataWrapper)
	    {
		    return dataWrapper.Value;
	    }


	    /// <summary>
	    /// Converts the type to thr wrapper.
	    /// </summary>
	    /// <param name="reference">The value to convert.</param>
	    /// <returns>The wrapper with the value.</returns>
	    public static implicit operator NotionDataWrapperSprite(Sprite reference)
	    {
		    return new NotionDataWrapperSprite(reference.name);
	    }
	    
	    
	    /// <summary>
	    /// Assigns the reference when called.
	    /// </summary>
	    protected override void Assign()
	    {
#if UNITY_EDITOR
		    if (!string.IsNullOrEmpty(id))
		    {
			    var asset = UnityEditor.AssetDatabase.FindAssets(id);
                
			    if (asset.Length > 0)
			    {
				    var path = UnityEditor.AssetDatabase.GUIDToAssetPath(asset[0]);
				    value = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(path);
			    }
			    else
			    {
				    Debug.LogWarning($"Unable to find a reference with the name {id}");
			    }
		    }
		    else
		    {
			    Debug.LogWarning("Unable to assign a reference, the id was empty.");
		    }
#endif
	    }
    }
}

#endif