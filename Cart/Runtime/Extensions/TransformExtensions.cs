/*
 * The Cart
 * Copyright (c) 2026 Carter Games
 *
 * This program is free software: you can redistribute it and/or modify it under the terms of the
 * GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version. 
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
 *
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <https://www.gnu.org/licenses/>. 
 */

using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Cart
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
        /// Sets the parent to the parent requested.
        /// </summary>
        /// <param name="transform">The transform to effect.</param>
        /// <param name="parent">The parent to set to.</param>
        /// <returns>The edited transform</returns>
        public static Transform SetParent(this Transform transform, Transform parent)
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
        
        
        /// <summary>
        /// Gets all the children transforms of the entered transform.
        /// </summary>
        /// <param name="parent">The parent transform.</param>
        /// <returns>A collection of all the children.</returns>
        public static IEnumerable<Transform> AllChildren(this Transform parent)
        {
            var list = new List<Transform>();

            for (var i = 0; i < parent.childCount; i++)
            {
                list.Add(parent.GetChild(i));
            }

            return list;
        }
    }
}