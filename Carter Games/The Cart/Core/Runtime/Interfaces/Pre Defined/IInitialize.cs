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

namespace CarterGames.Cart.Core
{
    /// <summary>
    /// Implement to add basic initialization to a class. 
    /// </summary>
    public interface IInitialize
    {
#if UNITY_2021_2_OR_NEWER
        /// <summary>
        /// Gets if the class is initialized.
        /// </summary>
        bool IsInitialized { get; set; }
        

        /// <summary>
        /// Initializes the class when called.
        /// </summary>
        void Initialize()
        {
            if (IsInitialized) return;
            IsInitialized = true;

            OnInitialize();
        }

        
        /// <summary>
        /// Runs only when the class is initialized.
        /// </summary>
        void OnInitialize();
#else
        /// <summary>
        /// Gets if the class is initialized.
        /// </summary>
        bool IsInitialized { get; set; }
        
        
        /// <summary>
        /// Initializes the class when called.
        /// </summary>
        void Initialize();
#endif
    }
}