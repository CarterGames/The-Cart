using System;
using UnityEngine;

namespace CarterGames.Cart.Modules.Currency.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SelectAccountAttribute : PropertyAttribute
    {
        public SelectAccountAttribute() {}
    }
}