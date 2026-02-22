namespace CarterGames.Cart.Core.Editor
{
    public abstract class BuildHandler
    {
        public abstract int Priority { get; }

        public virtual bool OnPreBuild()
        {
            return false; 
        }
        
        public virtual bool OnPostBuild()
        {
            return false; 
        }
        
        public virtual void OnBuildStarted() { }
        public virtual void OnBuildCompleted() { }
    }
}