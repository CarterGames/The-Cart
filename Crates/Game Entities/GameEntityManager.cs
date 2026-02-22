using System;
using CarterGames.Cart.Core.Events;

namespace CarterGames.Cart.Modules.GameEntities
{
    public static class GameEntityManager
    {
        private static Dictionary<string, GameEntity> allEntitiesByUidLookup;
        private static Dictionary<Type, GameEntity> allEntitiesByTypeLookup;

        
        public static bool IsInitialized { get; private set; }
        

        public static readonly Evt InitializedEvt = new Evt();
        public static readonly Evt RefreshedEvt = new Evt();
        

        private static void Initialize()
        {
            if (IsInitialized) return;
            
            IsInitialized = true;
            InitializedEvt.Raise();
        }

        
        private static T GetEntity<T>(string entityUid) where T : GameEntity
        {
            if (!allEntitiesByUidLookup.ContainsKey(entityUid)) return null;
            return allEntitiesByUidLookup[entityUid];
        }
        

        private static bool TryGetEntity<T>(string entityUid, out T entity) where T : GameEntity
        {
            entity = GetEntity<T>(entityUid);
            return entity != null;
        }
        
        
        private static IEnumable<T> GetEntities<T>() where T : GameEntity
        {
            if (!allEntitiesByTypeLookup.ContainsKey(typeof(T))) return null;
            return allEntitiesByTypeLookup[typeof(T)];
        }
        
        
        private static bool TryGetEntities<T>(out IEnumable<T> entities) where T : GameEntity
        {
            entities = GetEntities<T>();
            return entities != null;
        }
        

        private static void RefreshLookups()
        {
            RefreshedEvt.Raise();
        }
    }
}