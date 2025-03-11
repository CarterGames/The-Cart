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
using CarterGames.Cart.ThirdParty;
using UnityEngine;

namespace CarterGames.Cart.Modules.NotionData.Editor
{
    /// <summary>
    /// Handles a wrapper class for notion errors with the asset the error's on.
    /// </summary>
    [Serializable]
    public sealed class NotionRequestError
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private DataAsset asset;
        [SerializeField] private int errorCode;
        [SerializeField] private string code;
        [SerializeField] private string message;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The asset related to the error.
        /// </summary>
        public DataAsset Asset => asset;
        
        
        /// <summary>
        /// The HTTP error code response to the request.
        /// </summary>
        public int ErrorCode => errorCode;
        
        
        /// <summary>
        /// The code for the specific error the user ran into.
        /// </summary>
        public string Error => code;
        
        
        /// <summary>
        /// The message Notion sent back in relation to the error, usually gives decent context to the issue.
        /// </summary>
        public string Message => message;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Makes a new error class instance when called.
        /// </summary>
        /// <param name="asset">The related asset.</param>
        /// <param name="errorJson">The json to read for the error message.</param>
        public NotionRequestError(DataAsset asset, JSONNode errorJson)
        {
            this.asset = asset;
            errorCode = errorJson["errorCode"].AsInt;
            code = errorJson["code"];
            message = errorJson["message"];
        }
    }
}

#endif