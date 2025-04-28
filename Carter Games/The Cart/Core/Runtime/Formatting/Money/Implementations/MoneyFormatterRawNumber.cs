using System.Globalization;

namespace CarterGames.Cart.Core
{
    public class MoneyFormatterRawNumber : IMoneyFormatter
    {
        public string Format(double value)
        {
            return value.ToString("N0");
        }
    }
}