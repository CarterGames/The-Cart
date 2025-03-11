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

using UnityEngine;

namespace CarterGames.Cart.Core
{
    /// <summary>
    /// A helper class for opening websites, discord links and email.
    /// </summary>
    [AddComponentMenu("Carter Games/The Cart/Core/Open URL")]
    public sealed class OpenURL : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Instanced Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Opens the URL entered, can be anything...
        /// </summary>
        /// <param name="url">The URL to open</param>
        public void OpenWebsite(string url)
        {
            Website(url);
        }
        

        /// <summary>
        /// Opens a Discord Invite link...
        /// </summary>
        /// <param name="invite">The invite string, the bit after the normal URL that unique to each invite</param>
        /// <remarks>
        /// You only need the enter the last bit of the URL after the last /
        /// </remarks>
        public void OpenDiscordInvite(string invite)
        {
            DiscordInvite(invite);
        }
        

        /// <summary>
        /// Opens the users email client with the message to set for you. 
        /// </summary>
        /// <param name="email">The email to mail to.</param>
        public void OpenEmail(string email)
        {
            Email(email);
        }
        
        
        /// <summary>
        /// Opens the users email client with the message to set for you. 
        /// </summary>
        /// <param name="email">The email to mail to.</param>
        /// <param name="subject">The subject of the email.</param>
        public void OpenEmail(string email, string subject)
        {
            EmailWithSubject(email, subject);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Static Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Static Variant
        /// Opens the URL entered, can be anything...
        /// </summary>
        /// <param name="url">The URL to open</param>
        public static void Website(string url)
        {
            Application.OpenURL(url);
        }
        
        
        /// <summary>
        /// Static Variant
        /// Opens a Discord Invite link...
        /// </summary>
        /// <param name="invite">The invite string, the bit after the normal URL that unique to each invite</param>
        /// <remarks>
        /// You only need the enter the last bit of the URL after the last /
        /// </remarks>
        public static void DiscordInvite(string invite)
        {
            Application.OpenURL($"https://discord.gg/{invite}");
        }
        
        
        /// <summary>
        /// Static Variant
        /// Opens the users email client with the message to set for you. 
        /// </summary>
        /// <param name="email">The email to mail to.</param>
        public static void Email(string email)
        {
            Application.OpenURL($"mailto:{email}");
        }
        
        
        /// <summary>
        /// Static Variant
        /// Opens the users email client with the message to set for you. 
        /// </summary>
        /// <param name="email">The email to mail to.</param>
        /// <param name="subject">The subject of the email.</param>
        public static void EmailWithSubject(string email, string subject)
        {
            Application.OpenURL($"mailto:{email}?subject={subject}");
        }
    }
}