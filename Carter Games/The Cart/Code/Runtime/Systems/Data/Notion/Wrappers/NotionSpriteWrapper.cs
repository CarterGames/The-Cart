using System;
using UnityEngine;

namespace CarterGames.Cart.Data.Notion
{
    [Serializable]
    public class NotionSpriteWrapper : NotionWrapper<Sprite>
    {
        public NotionSpriteWrapper(string id) : base(id)
        { }
    }
}