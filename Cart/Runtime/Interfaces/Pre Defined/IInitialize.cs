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

namespace CarterGames.Cart
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