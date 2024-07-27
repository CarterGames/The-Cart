using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CarterGames.Cart.Core
{
    public class MoneyFormatterGeneric : IMoneyFormatter
    {
        public string Format(double value)
        {
            return FormatValue(value);
        }
        
        
        /// <summary>
        /// The builder to use in this script.
        /// </summary>
        private static readonly StringBuilder Builder = new StringBuilder();
        
        
        /// <summary>
        /// The first character to use once bast the Trillion count...
        /// </summary>
        private static readonly int ACharacter = Convert.ToInt32('a');
        
        
        /// <summary>
        /// The unit types to display before going to "aa", "ab", "ac" etc.....
        /// </summary>
        private static readonly Dictionary<int, string> StandardUnits = new Dictionary<int, string>
        {
            {0, ""},
            {1, "K"},
            {2, "M"},
            {3, "B"},
            {4, "T"}
        };
        
        
        /// <summary>
        /// Formats the string into a currency style number...
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The currency string.</returns>
        private static string FormatValue(double value)
        {
            if (value < 1d)
                return "0";

            var n = (int)Math.Log(value, 1000);
            var m = value / Math.Pow(1000, n);
            var unit = "";

            if (n < StandardUnits.Count)
            {
                unit = StandardUnits[n];
            }
            else
            {
                var unitInt = n - StandardUnits.Count;
                var secondUnit = unitInt % 26;
                var firstUnit = unitInt / 26;
                unit = Convert.ToChar(firstUnit + ACharacter) + Convert.ToChar(secondUnit + ACharacter).ToString();
            }


            var result = Math.Floor(m);

            var pre = Math.Floor(result).ToString(CultureInfo.InvariantCulture);
            var post = Floor(result.ToDecimalOnly(), 2);

            if (value > 999)
            {
                switch (pre.Length)
                {
                    case 3:

                        Builder.Clear();
                        Builder.Append(pre);
                        Builder.Append(unit);
                        return Builder.ToString();

                    case 2:

                        post = Math.Round(m.ToDecimalOnly(), 2) * 10;

                        if (post > 1 && post < 10)
                        {
                            Builder.Clear();
                            Builder.Append(pre);
                            Builder.Append(".");
                            Builder.Append(Math.Floor(post));

                            Builder.Append(unit);
                        }
                        else
                        {
                            Builder.Clear();
                            Builder.Append(pre);
                            Builder.Append(unit);
                        }

                        return Builder.ToString();

                    case 1:

                        post = Floor(m.ToDecimalOnly(), 2) * 10;

                        if (post.ToString(CultureInfo.InvariantCulture).Length.Equals(1))
                        {
                            if (post.Equals(0))
                            {
                                Builder.Clear();
                                Builder.Append(pre);
                                Builder.Append(unit);
                            }
                            else
                            {
                                Builder.Clear();
                                Builder.Append(pre);
                                Builder.Append(".");
                                Builder.Append(post);
                                Builder.Append("0");
                                Builder.Append(unit);
                            }
                        }
                        else
                        {
                            if (post.Equals(0))
                            {
                                Builder.Clear();
                                Builder.Append(pre);
                                Builder.Append(unit);
                            }
                            else
                            {
                                Builder.Clear();
                                Builder.Append(pre);
                                Builder.Append(".");

                                if (post.ToString(CultureInfo.InvariantCulture).Length.Equals(2))
                                {
                                    Builder.Append(post);
                                }
                                else
                                {
                                    if (post.ToString(CultureInfo.InvariantCulture).Length > 2)
                                    {
                                        if (post.ToString(CultureInfo.InvariantCulture).ToCharArray()[0].Equals('0'))
                                        {
                                            Builder.Append("0");
                                        }

                                        Builder.Append(post * 10);
                                    }
                                    else
                                    {
                                        Builder.Append("0");
                                        Builder.Append(post * 10);
                                    }
                                }

                                Builder.Append(unit);
                            }
                        }

                        return Builder.ToString();

                    default:
                        return string.Empty;
                }
            }
            else
            {
                Builder.Clear();
                Builder.Append(pre);
                Builder.Append(unit);
                return Builder.ToString();
            }
        }
    

        /// <summary>
        /// Floors a double for the conversion. 
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="decimalPlaces">The amount of decimal places to keep.</param>
        /// <returns>The converted value.</returns>
        private static double Floor(double value, int decimalPlaces)
        {
            var adjustment = Math.Pow(10, decimalPlaces);
            return Math.Floor(value * adjustment) / adjustment;
        }
    }
}