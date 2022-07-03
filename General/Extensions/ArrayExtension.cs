// ----------------------------------------------------------------------------
// ArrayExtension.cs
// 
// Description: An extension class for any array.
// ----------------------------------------------------------------------------

using System;
using UnityEngine;

namespace Scarlet.General
{
    /// <summary>
    /// An extension class for any array.
    /// </summary>
    public static class ArrayExtension
    {
        /// <summary>
        /// Checks to see if the array is empty or null.
        /// </summary>
        /// <param name="array">The array to compare.</param>
        /// <returns>Bool</returns>
        public static bool IsEmptyOrNull(this Array array)
        {
            if (array == null) return true;
            if (array.Length <= 0) return true;
            return false;
        }
    }
}