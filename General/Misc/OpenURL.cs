// ----------------------------------------------------------------------------
// OpenURL.cs
// 
// Description: A helper class for opening websites, discord links and email.
// ----------------------------------------------------------------------------

using UnityEngine;

namespace Adriana
{
    public class OpenURL : MonoBehaviour
    {
        #region Instance Methods

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

        #endregion

        #region Static Methods

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

        #endregion
    }
}