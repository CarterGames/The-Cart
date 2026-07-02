using System;
using UnityEngine;

namespace CarterGames.Cart.Runtime
{
    public abstract class SearchAttribute : PropertyAttribute
    {
        private Type searchProviderType;

        protected SearchAttribute(Type searchType)
        {
            searchProviderType = searchType;
        }
    }
}