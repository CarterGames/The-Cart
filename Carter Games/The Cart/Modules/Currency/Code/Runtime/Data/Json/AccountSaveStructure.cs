using System;

namespace CarterGames.Cart.Modules.Currency
{
    [Serializable]
    public class AccountSaveStructure
    {
        public string key;
        public double starting;
        public double balance;
    }
}