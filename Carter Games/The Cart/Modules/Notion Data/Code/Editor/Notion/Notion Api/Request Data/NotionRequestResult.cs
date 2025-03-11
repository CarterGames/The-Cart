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

using System.Collections.Generic;
using CarterGames.Cart.ThirdParty;

namespace CarterGames.Cart.Modules.NotionData.Editor
{
	/// <summary>
	/// A data class that holds the result of a request to the Notion API.
	/// </summary>
	public sealed class NotionRequestResult
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private readonly List<KeyValuePair<string, JSONNode>> data;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// The amount of data received.
		/// </summary>
		public int DataCount => data.Count;


		/// <summary>
		/// The json received stored per property name in a list.
		/// </summary>
		public List<KeyValuePair<string, JSONNode>> Data => data;
		
		
		/// <summary>
		/// Gets if the response was requested to be silenced or not.
		/// </summary>
		public bool SilentResponse { get; private set; }

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Constructors
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Makes a new response data class instance when called.
		/// </summary>
		/// <param name="data">The data to set.</param>
		/// <param name="silentResponse">Should the response be silenced in the editor?</param>
		public NotionRequestResult(List<KeyValuePair<string, JSONNode>> data, bool silentResponse)
		{
			this.data = data;
			SilentResponse = silentResponse;
		}
	}
}

#endif