namespace CarterGames.Cart.Crates
{
    public static class CrateConstants
    {
        public const string CarterGamesAuthor = "Carter Games";
        public const string CrateDefine = "CARTERGAMES_CART_CRATE";

        
        public static string GetDefine(string defineEnd)
        {
            return $"{CrateDefine}_{defineEnd}";
        }
    }
}