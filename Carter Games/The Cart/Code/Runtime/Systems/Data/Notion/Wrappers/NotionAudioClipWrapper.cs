using System;
using UnityEngine;

namespace CarterGames.Cart.Data.Notion
{
    [Serializable]
    public class NotionAudioClipWrapper : NotionWrapper<AudioClip>
    {
        public NotionAudioClipWrapper(string id) : base(id)
        { }
    }
}