using UnityEngine;

namespace JTools
{
    public static class FloatExtension
    {
        /// <summary>
        /// Turns an int into a negative variant of that int or vise versa
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static float Invert(this float original)
        {
            return original * -1;
        }
        
        
        /// <summary>
        /// Inverts a 0-1 range to 1-0
        /// </summary>
        /// <param name="original">The value to change</param>
        /// <returns>Float</returns>
        public static float Invert01(this float original)
        {
            return Mathf.Clamp01(1f - original);
        }
        
        
        /// <summary>
        /// Floors the value entered.
        /// </summary>
        /// <param name="original">The value to edit.</param>
        /// <returns>Int</returns>
        public static int FloorToInt(this float original)
        {
            return Mathf.FloorToInt(original);
        }
        
        
        /// <summary>
        /// Floors the value entered.
        /// </summary>
        /// <param name="original">The value to edit.</param>
        /// <returns>Float</returns>
        public static float Floor(this float original)
        {
            return Mathf.Floor(original);
        }
        
        
        /// <summary>
        /// Ceilings the value entered.
        /// </summary>
        /// <param name="original">The value to edit.</param>
        /// <returns>Int</returns>
        public static int CeilToInt(this float original)
        {
            return Mathf.CeilToInt(original);
        }
        
        
        /// <summary>
        /// Ceilings the value entered.
        /// </summary>
        /// <param name="original">The value to edit.</param>
        /// <returns>Float</returns>
        public static float Ceil(this float original)
        {
            return Mathf.Ceil(original);
        }
        
        
        /// <summary>
        /// Rounds the value entered.
        /// </summary>
        /// <param name="original">The value to edit.</param>
        /// <returns>Int</returns>
        public static int RoundToInt(this float original)
        {
            return Mathf.RoundToInt(original);
        }
        
        
        /// <summary>
        /// Rounds the value entered.
        /// </summary>
        /// <param name="original">The value to edit.</param>
        /// <returns>Float</returns>
        public static float Round(this float original)
        {
            return Mathf.Round(original);
        }
        
        
        /// <summary>
        /// Abs's the value entered.
        /// </summary>
        /// <param name="original">The value to edit.</param>
        /// <returns>Float</returns>
        public static float Abs(this float original)
        {
            return Mathf.Abs(original);
        }
        
        
        public static float Normalise(this float a, float max, float min = 0f)
        {
            return (a - min) / (max - min);
        }
    }
}