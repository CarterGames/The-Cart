using System;
using CarterGames.Cart.Logs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarterGames.Cart.Data.Notion
{
    [Serializable]
    public class NotionWrapper<T> where T : Object
    {
        [SerializeField] private string id;
        [SerializeField] private T value;

        
        public T Value => value;


        public NotionWrapper(string id)
        {
            this.id = id;
        }
        

        private void Assign()
        {
#if UNITY_EDITOR
            
            if (!string.IsNullOrEmpty(id))
            {
                var asset = UnityEditor.AssetDatabase.FindAssets(id)[0];
                
                if (asset.Length > 0)
                {
                    var path = UnityEditor.AssetDatabase.GUIDToAssetPath(asset);
                    value = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
                }
                else
                {
                    CartLogs.Warning<NotionWrapper<T>>($"Unable to find a reference with the name {id}");
                }
            }
            else
            {
                CartLogs.Warning<NotionWrapper<T>>("Unable to assign a reference, the id was empty.");
            }
#endif
        }


        public static implicit operator T(NotionWrapper<T> wrapper)
        {
            return wrapper.Value;
        }
        
        
        public static implicit operator NotionWrapper<T>(T reference)
        {
            return new NotionWrapper<T>(reference.name);
        }
    }
}