// ----------------------------------------------------------------------------
// ArrayExtension.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 15/02/2022
// ----------------------------------------------------------------------------

using System;

namespace JTools
{
    public static class ArrayExtension
    {
        public static bool IsEmptyOrNull(this Array array)
        {
            if (array == null) return true;
            if (array.Length <= 0) return true;
            return false;
        }
    }
}