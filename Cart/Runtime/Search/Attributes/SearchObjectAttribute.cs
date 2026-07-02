using System;

namespace CarterGames.Cart.Runtime
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SearchObjectAttribute : SearchAttribute
    {
        public SearchObjectAttribute(Type searchType) : base(searchType)
        {
        }
    }
}