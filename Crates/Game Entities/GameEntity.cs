using CarterGames.Cart.Core.Events;

namespace CarterGames.Cart.Modules.GameEntities
{
    public abstract class GameEntity
    {
        protected GameObject entityObject; 
        
        public bool IsSpawned { get; private set; }
        
        public readonly Evt<string> StateChangedEvt = new Evt<string>();
        
        
        protected virtual void OnEntityStateChanged(string state) { }

        
        public void CreateEntityInScene()
        {
            if (IsSpawned) return;
            // entityObject = Object.Instantiate()
            IsSpawned = true;
        }
    }
}