using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JTools
{
    /// <summary>
    /// Just like the TryGetComponent, but with options to get from parents and children.
    /// </summary>
    public static class TryGetExtension
    {
        /// <summary>
        /// Tries to get the component requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The component found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInParent<T>(this MonoBehaviour target, out T component)
        {
            var _check = target.GetComponentInParent<T>();
            component = _check;
            return _check != null;
        }
        
        
        /// <summary>
        /// Tries to get the component requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The component found</param>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInParent(this MonoBehaviour target, Type type, out Component component)
        {
            var _check = target.GetComponentInParent(type);
            component = _check;
            return _check != null;
        }  
        
        
        /// <summary>
        /// Tries to get the component requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The component found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInParent<T>(this Component target, out T component)
        {
            var _check = target.GetComponentInParent<T>();
            component = _check;
            return _check != null;
        }
        
        
        /// <summary>
        /// Tries to get the component requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The component found</param>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInParent(this Component target, Type type, out Component component)
        {
            var _check = target.GetComponentInParent(type);
            component = _check;
            return _check != null;
        }        
        
        
        /// <summary>
        /// Tries to get the component requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The component found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInParent<T>(this GameObject target, out T component)
        {
            var _check = target.GetComponentInParent<T>();
            component = _check;
            return _check != null;
        }
        
        
        /// <summary>
        /// Tries to get the component requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The component found</param>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInParent(this GameObject target, Type type, out Component component)
        {
            var _check = target.GetComponentInParent(type);
            component = _check;
            return _check != null;
        }
        
        
        /// <summary>
        /// Tries to get the components requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The components found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInParent<T>(this Component target, out List<T> component)
        {
            var _check = target.GetComponentsInParent<T>().ToList();
            component = _check;
            return _check.Count > 0;
        } 
        
        
        /// <summary>
        /// Tries to get the components requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The components found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInParent<T>(this GameObject target, out List<T> component)
        {
            var _check = target.GetComponentsInParent<T>().ToList();
            component = _check;
            return _check.Count > 0;
        }
        
        
        /// <summary>
        /// Tries to get the components requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The components found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInParent<T>(this MonoBehaviour target, out List<T> component)
        {
            var _check = target.GetComponentsInParent<T>().ToList();
            component = _check;
            return _check.Count > 0;
        }        
        
        
        /// <summary>
        /// Tries to get the components requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The components found</param>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInParent(this Component target, Type type, out List<Component> component)
        {
            var _check = target.GetComponentsInParent(type).ToList();
            component = _check;
            return _check.Count > 0;
        } 
        
        
        /// <summary>
        /// Tries to get the components requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The components found</param>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInParent(this GameObject target, Type type, out List<Component> component)
        {
            var _check = target.GetComponentsInParent(type).ToList();
            component = _check;
            return _check.Count > 0;
        }
        
        
        /// <summary>
        /// Tries to get the components requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The components found</param>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInParent(this MonoBehaviour target, Type type, out List<Component> component)
        {
            var _check = target.GetComponentsInParent(type).ToList();
            component = _check;
            return _check.Count > 0;
        } 
        
        
        /// <summary>
        /// Tries to get the component requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The component found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInChildren<T>(this GameObject target, out T component)
        {
            var _check = target.GetComponentInChildren<T>();
            component = _check;
            return _check != null;
        }
        
        /// <summary>
        /// Tries to get the component requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The component found</param>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInChildren(this GameObject target, Type type, out Component component)
        {
            var _check = target.GetComponentInChildren(type);
            component = _check;
            return _check != null;
        }
        
        
        /// <summary>
        /// Tries to get the component requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The component found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInChildren<T>(this Component target, out T component)
        {
            var _check = target.GetComponentInChildren<T>();
            component = _check;
            return _check != null;
        }
        
        /// <summary>
        /// Tries to get the component requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The component found</param>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInChildren(this Component target, Type type, out Component component)
        {
            var _check = target.GetComponentInChildren(type);
            component = _check;
            return _check != null;
        }
        
        
        /// <summary>
        /// Tries to get the component requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The component found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInChildren<T>(this MonoBehaviour target, out T component)
        {
            var _check = target.GetComponentInChildren<T>();
            component = _check;
            return _check != null;
        }
        
        /// <summary>
        /// Tries to get the component requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The component found</param>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInChildren(this MonoBehaviour target, Type type, out Component component)
        {
            var _check = target.GetComponentInChildren(type);
            component = _check;
            return _check != null;
        }
        
        
        /// <summary>
        /// Tries to get the components requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The components found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInChildren<T>(this GameObject target, out List<T> component)
        {
            var _check = target.GetComponentsInChildren<T>().ToList();
            component = _check;
            return _check.Count > 0;
        }

        /// <summary>
        /// Tries to get the components requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The components found</param>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInChildren(this GameObject target, Type type, out List<Component> component)
        {
            var _check = target.GetComponentsInChildren(type).ToList();
            component = _check;
            return _check.Count > 0;
        } 
        
        /// <summary>
        /// Tries to get the components requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The components found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInChildren<T>(this Component target, out List<T> component)
        {
            var _check = target.GetComponentsInChildren<T>().ToList();
            component = _check;
            return _check.Count > 0;
        }

        /// <summary>
        /// Tries to get the components requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The components found</param>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInChildren(this Component target, Type type, out List<Component> component)
        {
            var _check = target.GetComponentsInChildren(type).ToList();
            component = _check;
            return _check.Count > 0;
        }
        
        
        /// <summary>
        /// Tries to get the components requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The components found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInChildren<T>(this MonoBehaviour target, out List<T> component)
        {
            var _check = target.GetComponentsInChildren<T>().ToList();
            component = _check;
            return _check.Count > 0;
        }

        /// <summary>
        /// Tries to get the components requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The components found</param>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInChildren(this MonoBehaviour target, Type type, out List<Component> component)
        {
            var _check = target.GetComponentsInChildren(type).ToList();
            component = _check;
            return _check.Count > 0;
        }
    }
}