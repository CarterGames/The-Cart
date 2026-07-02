using System;
using UnityEngine;

namespace CarterGames.Cart.Runtime
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class SearchStringAttribute : SearchAttribute
    {
        public SearchStringAttribute(Type searchType) : base(searchType)
        {
        }
    }
}