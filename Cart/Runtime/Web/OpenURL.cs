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

using UnityEngine;

namespace CarterGames.Cart
{
    /// <summary>
    /// A helper class for opening websites, discord links and email.
    /// </summary>
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