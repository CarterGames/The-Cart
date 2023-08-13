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

namespace CarterGames.Common.General
{
    /// <summary>
    /// A base MonoBehaviour with some of the common interfaces implemented already. 
    /// </summary>
    public abstract class CommonMonoBase : MonoBehaviour, IInitialize, IDispose
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private bool autoInitialize;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void OnEnable()
        {
            if (!autoInitialize) return;
#if UNITY_2021_2_OR_NEWER
            GetComponent<IInitialize>().Initialize();
#else
            Initialize();
#endif
        }


        private void OnDisable()
        {
            Dispose();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IInitialize Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        public bool IsInitialized { get; set; }
        
#if UNITY_2021_2_OR_NEWER
        public virtual void OnInitialize()
        {
            
        }
#else
        public virtual void Initialize()
        {
            if (IsInitialized) return;
            IsInitialized = true;
        }
#endif

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IDispose Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public virtual void Dispose()
        {
            
        }
    }
}