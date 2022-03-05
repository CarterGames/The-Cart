using UnityEngine;

namespace JTools
{
    public static class IntExtension
    {
        /// <summary>
        /// Inverts the value...
        /// </summary>
        /// <param name="original">The value to edit...</param>
        /// <returns>The inverted value</returns>
        public static int Invert(this int original)
        {
            return original * -1;
        }
        
        
        /// <summary>
        /// Inverts the value...
        /// </summary>
        /// <param name="original">The value to edit...</param>
        /// <returns>The inverted value</returns>
        public static int Invert01(this int original)
        {
            return (int) Mathf.Clamp01(1f - original);
        }


        public static bool ToBool(this int original)
        {
            return System.Convert.ToBoolean(original);
        }
        
        
        public static int Normalise(this int a, int max, int min = 0)
        {
            return (a - min) / (max - min);
        }
    }
}