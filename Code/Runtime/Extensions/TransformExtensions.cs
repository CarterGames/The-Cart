/*
 * Copyright (c) 2018-Present Carter Games
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;

namespace Scarlet.General
{
    /// <summary>
    /// A collection of extensions for the transform component. 
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// Gets the top most parent that isn't the root object.
        /// </summary>
        /// <param name="transform">The transform to effect.</param>
        /// <returns>The parent transform</returns>
        public static Transform GetTopParentNotRoot(this Transform transform)
        {
            var parent = transform.parent;

            if (parent == null) return transform;
            
            var root = transform.root;
            
            while (parent != root)
            {
                transform = parent;
                parent = parent.parent;
            }

            return transform;
        }
        
        
        /// <summary>
        /// Gets the total number of parent transform to the entered transform.
        /// </summary>
        /// <param name="transform">The transform to effect.</param>
        /// <returns>The count of parents to this transform.</returns>
        public static int GetParentCount(this Transform transform)
        {
            var parent = transform.parent;

            if (parent == null) return 0;
            
            var root = transform.root;
            var count = 1;
            
            while (parent != root)
            {
                transform = parent;
                parent = parent.parent;
                count++;
            }

            return count;
        }
        
        
        /// <summary>
        /// Sets the parent of the transform as well as setting the transform at the first in the index list of said parent.
        /// </summary>
        /// <param name="transform">The transform the effect.</param>
        /// <param name="target">The parent to set.</param>
        public static void SetParentAndFirstIndex(this Transform transform, Transform target)
        {
            transform.SetParent(target);
            transform.SetAsFirstSibling();
        }
        
        
        /// <summary>
        /// Sets the parent of the transform as well as setting the transform at the last in the index list of said parent.
        /// </summary>
        /// <param name="transform">The transform the effect.</param>
        /// <param name="target">The parent to set.</param>
        public static void SetParentAndLastIndex(this Transform transform, Transform target)
        {
            transform.SetParent(target);
            transform.SetAsLastSibling();
        }

        
        /// <summary>
        /// Sets the position & rotation but allows for local only edits as well.
        /// </summary>
        /// <param name="transform">The transform to effect.</param>
        /// <param name="target">The target transform to match to.</param>
        /// <param name="isLocal">Should the changes be local?</param>
        /// <returns>The edited transform</returns>
        public static Transform SetPosAndRot(this Transform transform, Transform target, bool isLocal = false)
        {
            if (isLocal)
            {
                transform.localPosition = target.localPosition;
                transform.localRotation = target.localRotation;

            }
            else
            {
                transform.position = target.position;
                transform.rotation = target.rotation;
            }

            return transform;
        }


        /// <summary>
        /// Tries to set the parent to the parent requested.
        /// </summary>
        /// <param name="transform">The transform to effect.</param>
        /// <param name="parent">The parent to set to.</param>
        /// <returns>The edited transform</returns>
        private static Transform TrySetParent(this Transform transform, Transform parent)
        {
            if (!transform.parent.Equals(parent))
            {
                transform.SetParent(parent);
            }
            
            return transform;
        }


        /// <summary>
        /// Sets the scale of the transform when called.
        /// </summary>
        /// <param name="transform">The transform to effect.</param>
        /// <param name="scale">The scale to set to.</param>
        /// <returns></returns>
        public static Transform SetScale(this Transform transform, Vector3 scale)
        {
            transform.localScale = scale;
            return transform;
        }
    }
}