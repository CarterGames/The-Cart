using System;
using UnityEngine;

namespace CarterGames.Cart.Runtime
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SearchAssemblyClassDefAttribute : SearchAttribute
    {
        public SearchAssemblyClassDefAttribute(Type searchType) : base(searchType)
        {
        }
    }
}