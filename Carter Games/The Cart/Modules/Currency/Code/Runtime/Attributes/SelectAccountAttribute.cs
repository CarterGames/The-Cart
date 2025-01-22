#if CARTERGAMES_CART_MODULE_CURRENCY

using System;
using UnityEngine;

namespace CarterGames.Cart.Modules.Currency
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SelectAccountAttribute : PropertyAttribute
    {
        public SelectAccountAttribute() {}
    }
}

#endif