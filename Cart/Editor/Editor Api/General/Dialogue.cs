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

using System;
using UnityEditor;

namespace CarterGames.Cart.Editor
{
    /// <summary>
    /// Handles displaying editor dialogue boxes.
    /// </summary>
    public static class Dialogue
    {
        /// <summary>
        /// Displays a box with a title, description & ctx 1 button.
        /// </summary>
        /// <param name="title">The title for the dialogue box.</param>
        /// <param name="content">The content for the dialogue box.</param>
        /// <param name="contTitle">The label for the action button.</param>
        /// <returns>If the action button was pressed.</returns>
        public static bool Display(string title, string content, string contTitle)
        {
            return EditorUtility.DisplayDialog(title, content, contTitle);
        }
        
        
        /// <summary>
        /// Displays a box with a title, description & ctx 2 buttons.
        /// </summary>
        /// <param name="title">The title for the dialogue box.</param>
        /// <param name="content">The content for the dialogue box.</param>
        /// <param name="contTitle">The label for the true button.</param>
        /// <param name="cancelTitle">The label for the false button.</param>
        /// <returns>If the action button was pressed.</returns>
        public static bool Display(string title, string content, string contTitle, string cancelTitle)
        {
            return EditorUtility.DisplayDialog(title, content, contTitle, cancelTitle);
        }
        
        
        /// <summary>
        /// Displays a box with a title, description & ctx 2 buttons.
        /// </summary>
        /// <param name="title">The title for the dialogue box.</param>
        /// <param name="content">The content for the dialogue box.</param>
        /// <param name="contTitle">The label for the action button.</param>
        /// <param name="contAction">The action to perform when the action button is pressed.</param>
        public static void Display(string title, string content, string contTitle, Action contAction)
        {
            if (EditorUtility.DisplayDialog(title, content, contTitle))
            {
                contAction?.Invoke();
            }
        }
        
        
        /// <summary>
        /// Displays a box with a title, description & ctx 2 buttons.
        /// </summary>
        /// <param name="title">The title for the dialogue box.</param>
        /// <param name="content">The content for the dialogue box.</param>
        /// <param name="contTitle">The label for the true button.</param>
        /// <param name="cancelTitle">The label for the false button.</param>
        /// <param name="contAction">The action to perform when the action button is pressed.</param>
        public static void Display(string title, string content, string contTitle, string cancelTitle, Action contAction)
        {
            if (EditorUtility.DisplayDialog(title, content, contTitle, cancelTitle))
            {
                contAction?.Invoke();
            }
        }
        
        
        /// <summary>
        /// Displays a box with a title, description & ctx 2 buttons.
        /// </summary>
        /// <param name="title">The title for the dialogue box.</param>
        /// <param name="content">The content for the dialogue box.</param>
        /// <param name="contTitle">The label for the true button.</param>
        /// <param name="cancelTitle">The label for the false button.</param>
        /// <param name="contAction">The action to perform when the true button is pressed.</param>
        /// <param name="cancelAction">The action to perform when the false button is pressed.</param>
        public static void Display(string title, string content, string contTitle, string cancelTitle, Action contAction, Action cancelAction)
        {
            if (EditorUtility.DisplayDialog(title, content, contTitle, cancelTitle))
            {
                contAction?.Invoke();
            }
            else
            {
                cancelAction?.Invoke();
            }
        }
    }
}