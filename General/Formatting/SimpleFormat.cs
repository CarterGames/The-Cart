// ----------------------------------------------------------------------------
// SimpleFormat.cs
// 
// Description: A class that formats numbers into a generic K,M,B,T,aa,ab,ac format
//              with decimal points based on the character length.
//              So 1234 = 1.23K, 12345 = 12.3K, 123456 = 123K, 1234567 = 1.23M etc....
//
// Credit: https://gram.gs/gramlog/formatting-big-numbers-aa-notation/
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Scarlet.General
{
    public static class SimpleFormat
    {
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
        /// Formats the entered value into a K,M,B style format...
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <returns>The resulting string formatted</returns>
        public static string ToSimple(this double value)
        {
            return FormatValue(value);
        }


        /// <summary>
        /// Formats the entered value into a K,M,B style format...
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <returns>The resulting string formatted</returns>
        public static string ToSimple(this float value)
        {
            return FormatValue(value);
        }
        
        
        /// <summary>
        /// Formats the entered value into a K,M,B style format...
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <returns>The resulting string formatted</returns>
        public static string ToSimple(this int value)
        {
            return FormatValue(value);
        }


        private static string FormatValue(double value)
        {
            if (value < 1d)
                return "0";

            var n = (int)Math.Log(value, 1000);
            var m = value / Math.Pow(1000, n);
            var unit = "";

            if (n < StandardUnits.Count)
                unit = StandardUnits[n];
            else
            {
                var unitInt = n - StandardUnits.Count;
                var secondUnit = unitInt % 26;
                var firstUnit = unitInt / 26;
                unit = Convert.ToChar(firstUnit + ACharacter) + Convert.ToChar(secondUnit + ACharacter).ToString();
            }
            
            var _result = Math.Floor(m * 100) / 100;

            // Sorts the decimals out into the format the way I like it to be formatted...

            // If less than 1K, just show the normal number as is...
            if (!(value >= 1000))
                return _result.ToString("F0") + unit;

            // If the number is in the 100K 100M 100B range...
            if (_result >= 100)
                return _result.ToString("F0") + unit;

            // If the number is within the 10K, 10M, 10B range...
            if (_result >= 10)
            {
                // If the number is a round number like 12.0, 43.0, 75.0
                // Then show it like 12K, 43K, 75K etc.
                if ((_result % 1).Equals(0))
                    return _result.ToString("F0") + unit;

                
                // Else if not a whole number.... show the first decimal...
                // Like 12.5K, 34.4K, 76.2K etc.
                return _result.ToString("F1") + unit;
            }

            // Else
            // The value is withing the 1000 - 9999 range...

            // If the number is 1234, 2345, 3456 etc...
            // The format it like 1.23K, 2.34K & 3.45K etc.
            if (!(value % 100).Equals(0))
                return _result.ToString("F2") + unit;
            
            // If the number is 1200, 2300, 3400 etc...
            // The format it like 1.2K, 2.3K & 3.4K etc.
            if (!(value % 1000).Equals(0))
                return _result.ToString("F1") + unit;
            
            // Else
            // Show it without any decimals, so 1K, 2K ,3K etc.
            return _result.ToString("F0") + unit;
        }
    }
}