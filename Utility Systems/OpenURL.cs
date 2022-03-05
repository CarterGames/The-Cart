using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JTools
{
    public class OpenURL : MonoBehaviour
    {
        /// <summary>
        /// Opens the URL entered, can be anything...
        /// </summary>
        /// <param name="url">The URL to open</param>
        public void OpenWebsite(string url)
        {
            Application.OpenURL(url);
        }
        
        
        /// <summary>
        /// Opens a Discord Invite link...
        /// </summary>
        /// <param name="invite">The invite string, the bit after the normal URL that unique to each invite</param>
        /// <remarks>
        /// You only need the enter the last bit of the URL after the  last /
        /// </remarks>
        public void OpenDiscordInvite(string invite)
        {
            Application.OpenURL($"https://discord.gg/{invite}");
        }
    }
}