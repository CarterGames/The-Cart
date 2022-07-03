// ----------------------------------------------------------------------------
// VectorExtensions.cs
// 
// Description: An extension class for some common vector maths.
// ----------------------------------------------------------------------------

using UnityEngine;

namespace Scarlet.General
{
    public static class VectorExtensions
    {
        #region Direction To

        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector2</returns>
        public static Vector2 DirectionTo(this Vector2 original, Vector2 target)
        {
            return (target - original).normalized;
        }
        
        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector3</returns>
        public static Vector3 DirectionTo(this Vector3 original, Vector3 target)
        {
            return (target - original).normalized;
        }
        
        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector4</returns>
        public static Vector4 DirectionTo(this Vector4 original, Vector4 target)
        {
            return (target - original).normalized;
        }

        #endregion
        
        #region Line

        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector2</returns>
        public static Vector2 Line(this Vector2 original, Vector2 target)
        {
            return (target - original).normalized + original;
        }
        
        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector3</returns>
        public static Vector3 Line(this Vector3 original, Vector3 target)
        {
            return (target - original).normalized + original;
        }
        
        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector4</returns>
        public static Vector4 Line(this Vector4 original, Vector4 target)
        {
            return (target - original).normalized + original;
        }
        
        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <param name="distance">The length the line should extend to...</param>
        /// <returns>Vector2</returns>
        public static Vector2 Line(this Vector2 original, Vector2 target, float distance)
        {
            return ((target - original).normalized + original) * distance;
        }
        
        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <param name="distance">The length the line should extend to...</param>
        /// <returns>Vector3</returns>
        public static Vector3 Line(this Vector3 original, Vector3 target, float distance)
        {
            return ((target - original).normalized + original) * distance;
        }
        
        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <param name="distance">The length the line should extend to...</param>
        /// <returns>Vector4</returns>
        public static Vector4 Line(this Vector4 original, Vector4 target, float distance)
        {
            return ((target - original).normalized + original) * distance;
        }

        #endregion
        
        #region Midpoint

        /// <summary>
        /// Gets a middle point of a line between a & b
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector2</returns>
        public static Vector2 MidPoint(this Vector2 original, Vector2 target)
        {
            return (original + target) * .5f;
        }
        
        /// <summary>
        /// Gets a middle point of a line between a & b
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector3</returns>
        public static Vector3 MidPoint(this Vector3 original, Vector3 target)
        {
            return (original + target) * .5f;
        }
        
        /// <summary>
        /// Gets a middle point of a line between a & b
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector3</returns>
        public static Vector4 MidPoint(this Vector4 original, Vector4 target)
        {
            return (original + target) * .5f;
        }

        #endregion
        
        #region With

        /// <summary>
        /// Returns a vector with the modified x or y values...
        /// </summary>
        /// <param name="original">The initial vector to edit...</param>
        /// <param name="x">The new x value</param>
        /// <param name="y">The new y value</param>
        /// <returns>Vector2</returns>
        public static Vector2 With(this Vector2 original, float? x = null, float? y = null)
        {
            return new Vector2(x ?? original.x, y ?? original.y);
        }

        /// <summary>
        /// Returns a vector with the modified x, y, z value...
        /// </summary>
        /// <param name="original">The initial vector to edit...</param>
        /// <param name="x">The new x value</param>
        /// <param name="y">The new y value</param>
        /// <param name="z">The new z value</param>
        /// <returns>Vector3</returns>
        public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
        }

        /// <summary>
        /// Returns a vector with the modified w, x, y, z value...
        /// </summary>
        /// <param name="original">The initial vector to edit...</param>
        /// <param name="w">The new w value</param>
        /// <param name="x">The new x value</param>
        /// <param name="y">The new y value</param>
        /// <param name="z">The new z value</param>
        /// <returns>Vector4</returns>
        public static Vector4 With(this Vector4 original, float? w = null, float? x = null, float? y = null, float? z = null)
        {
            return new Vector4(w ?? original.w, x ?? original.x, y ?? original.y, z ?? original.z);
        }

        #endregion
    }
}