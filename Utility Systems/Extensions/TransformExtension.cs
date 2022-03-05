using UnityEngine;


namespace JTools
{
    public static class TransformExtension
    {
        /// <summary>
        /// Gets a line from a to b...
        /// </summary>
        /// <param name="original">The start point...</param>
        /// <param name="target">The end point...</param>
        /// <returns>Vector2</returns>
        public static Vector2 DirectionTo(this Vector2 original, Vector2 target)
        {
            return (target - (Vector2) original).normalized;
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
        /// <returns>Vector3</returns>
        public static Vector2 Line(this Vector2 original, Vector2 target)
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
        /// Returns a vector with the modified x value...
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
        /// Returns a vector with the modified x value...
        /// </summary>
        /// <param name="original">The initial vector to edit...</param>
        /// <param name="x">The new x value</param>
        /// <param name="y">The new y value</param>
        /// <param name="z">The new z value</param>
        /// <returns>Vector2</returns>
        public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
        }


        public static void With(this Transform original, Vector3? position = null, bool isLocalPos = false, Quaternion? rotation = null, bool isLocalRot = false, Vector3? scale = null)
        {
            if (isLocalPos)
                original.localPosition = position ?? original.localPosition;
            else
                original.position = position ?? original.position;
            
            if (isLocalRot)
                original.localRotation = rotation ?? original.localRotation;
            else
                original.rotation = rotation ?? original.rotation;
            
            original.localScale = scale ?? original.localScale;
        }


        public static Transform SetPosition(this Transform original, Vector3 position)
        {
            original.position = position;
            return original;
        }
        
        public static Transform SetLocalPosition(this Transform original, Vector3 position)
        {
            original.localPosition = position;
            return original;
        } 
        
        public static Transform SetRotation(this Transform original, Quaternion position)
        {
            original.rotation = position;
            return original;
        }
        
        public static Transform SetRotation(this Transform original, Vector3 position)
        {
            original.rotation = Quaternion.Euler(position);
            return original;
        }
        
        public static Transform SetLocalRotation(this Transform original, Quaternion position)
        {
            original.localRotation = position;
            return original;
        }
        
        public static Transform SetLocalRotation(this Transform original, Vector3 position)
        {
            original.localRotation = Quaternion.Euler(position);
            return original;
        }
        
        public static Transform SetScale(this Transform original, Vector3 position)
        {
            original.localScale = position;
            return original;
        }

        public static Transform ResetPosition(this Transform original, bool isLocal = false)
        {
            if (isLocal)
                original.localPosition = Vector3.zero;
            else
                original.position = Vector3.zero;
            
            return original;
        }
    }
}
