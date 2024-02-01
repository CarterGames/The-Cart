using System;
using UnityEngine;

namespace CarterGames.Cart.Data.Notion
{
    [Serializable]
    public class NotionPrefabWrapper : NotionWrapper<GameObject>
    {
        public NotionPrefabWrapper(string id) : base(id)
        { }
    }
}