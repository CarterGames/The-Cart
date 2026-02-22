namespace CarterGames.Cart.Editor
{
    public abstract class PreBuildCheckBase
    {
        public abstract int Priority { get; }
        public abstract PreBuildCheckResult ValidateCanBuild();
        public virtual void OnBuildStart() {}
    }
}