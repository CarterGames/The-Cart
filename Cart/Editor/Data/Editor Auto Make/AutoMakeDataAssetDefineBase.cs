using System;
using CarterGames.Cart.Data;
using UnityEditor;

namespace CarterGames.Cart.Editor
{
    public abstract class AutoMakeDataAssetDefineBase<T> : IAutoMakeDataAssetDefine<T> where T : DataAsset
    {
        private T cache;
        private SerializedObject objCache;

        public Type AssetType => typeof(T);
        
        public virtual AutoMakePathType PathType => AutoMakePathType.Data;

        public abstract string DataAssetFileName { get; }
        
        public virtual string DataAssetFilter => $"t:{AssetType.FullName} name={DataAssetFileName}";

        public virtual string DataAssetPath => DataAssetFileName;
        
        public string CompleteDataAssetPath => $"{GetBasePath()}/{DataAssetPath}";
        
        public T AssetRef => AutoMakeDataAssetManager.GetOrCreateAsset(this, ref cache);
        
        public SerializedObject ObjectRef => AutoMakeDataAssetManager.GetOrCreateAssetObject(this, ref objCache);

        public virtual void TryCreate()
        {
            AutoMakeDataAssetManager.GetOrCreateAsset(this, ref cache);
        }
        
        public virtual void OnCreated() { }

        private string GetBasePath()
        {
            return PathType switch
            {
                AutoMakePathType.Resources => AutoMakeDataAssetManager.FullPathResources,
                AutoMakePathType.Data => AutoMakeDataAssetManager.FullPathData,
                _ => string.Empty
            };
        }
    }
}