using System;
using System.Collections.Generic;

namespace CarterGames.Cart.Data.Notion
{
    [Serializable]
    public class NotionArrayWrapper<T>
    {
        public List<T> list;
    }
}