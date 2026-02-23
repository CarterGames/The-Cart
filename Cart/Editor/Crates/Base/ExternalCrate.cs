namespace CarterGames.Cart.Crates
{
    /// <summary>
    /// An class to define a packaged (external) crate.
    /// </summary>
    public abstract class ExternalCrate : Crate
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The target package to import.
        /// </summary>
        public abstract GitPackageInfo PackageInfo { get; }
    }
}