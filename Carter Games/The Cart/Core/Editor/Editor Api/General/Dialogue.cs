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

using System;
using UnityEditor;

namespace CarterGames.Cart.Core.Editor
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