using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Management;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.MetaData.Editor
{
    public class DataAssetMetaData : DataAsset
    {
        [SerializeField] private SerializableDictionary<string, TextAsset> data;


        private SerializableDictionary<string, TextAsset> GetAssets()
        {
            Debug.Log("Updating...");
            var lookup = new SerializableDictionary<string, TextAsset>();
            
            foreach (var defPath in InterfaceHelper.GetAllInterfacesInstancesOfType<IMetaDefinition>())
            {
                Debug.Log(defPath.Path);
                
                var assets = AssetDatabase.FindAssets("t:textasset", new[] { defPath.Path });

                Debug.Log(assets.Length);
                
                if (assets.Length <= 0) continue;

                foreach (var asset in assets)
                {
                    var path = AssetDatabase.GUIDToAssetPath(asset);

                    Debug.Log(path);
                    if (!path.Contains(".json")) continue;
                    Debug.Log("VALID");
                    
                    var actualFile = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset));
                    
                    lookup.Add(actualFile.name, (TextAsset) actualFile);
                }
            }

            return lookup;
        }


        public MetaData GetData(string id)
        {
            return GetDataUpdateIfNotPresent(id);
        }
        
        
        public bool TryGetData(string id, out MetaData asset)
        {
            asset = GetData(id);
            return asset != null;
        }


        private MetaData GetDataUpdateIfNotPresent(string id)
        {
            TextAsset value;
            
            if (!data.IsEmptyOrNull())
            {
                if (data.TryGetValue(id, out value))
                {
                    return JsonUtility.FromJson<MetaData>(value.text);
                }

                data = GetAssets();

                if (data.TryGetValue(id, out value))
                {
                    return JsonUtility.FromJson<MetaData>(value.text);
                }
            }

            data = GetAssets();

            if (data.TryGetValue(id, out value))
            {
                return JsonUtility.FromJson<MetaData>(value.text);
            }

            return null;
        }
    }
}