namespace CarterGames.Cart.Crates
{
    public class CrateNotionData : ExternalCrate
    {
        public override string CrateName => "Notion Data";
        public override string CrateDescription =>
            "A tool to download Notion databases into a Unity scriptable object for use in Unity projects. Handy for game data such as items, localization, skills and more!";
        public override string CrateAuthor => "Carter Games";

        public override string CrateDefine => "CARTERGAMES_NOTIONDATA";
        
        public override GitPackageInfo PackageInfo => new GitPackageInfo("Notion Data", "games.carter.notiondata",
            "https://github.com/CarterGames/NotionToUnity.git");
    }
}