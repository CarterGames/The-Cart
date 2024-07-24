using UnityEngine;

namespace CarterGames.Cart.Core
{
    public static class GameObjectExtensions
    {
        public static T OrNull<T>(this T obj) where T : Object
        {
            return obj ? obj : null;
        }
    }
}