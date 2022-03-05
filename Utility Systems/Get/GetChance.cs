using UnityEngine;

namespace JTools
{
    /// <summary>
    /// Gets chance based on the inputted values...
    /// </summary>
    public static class GetChance
    {
        /// <summary>
        /// Rolls a 50/50 check, and returns the result when using the getter.
        /// </summary>
        public static bool Roll5050 => System.Convert.ToBoolean(GetRandom.Int(0,1));
   
        
        /// <summary>
        /// Rolls out of 100 and returns whether or not the user won or lost.
        /// </summary>
        /// <param name="chanceOfSuccess">Int | The chance in percentage that the user will win.</param>
        /// <returns>Bool | True if the user won, false if not.</returns>
        public static bool Roll100(int chanceOfSuccess)
        {
            var _roll = Mathf.RoundToInt(GetRandom.Float(1f, 100f));
            return _roll <= chanceOfSuccess;
        }
        
        
        /// <summary>
        /// Rolls out of 100 and returns whether or not the user won or lost.
        /// </summary>
        /// <param name="chanceOfSuccess">Int | The chance in percentage that the user will win.</param>
        /// <returns>Bool | True if the user won, false if not.</returns>
        public static bool Roll100(float chanceOfSuccess)
        {
            var _roll = GetRandom.Float(1f, 100f);
            return _roll <= chanceOfSuccess;
        }

        
        /// <summary>
        /// Returns a roll based on a 1 in x style of of chance, like a drop table...
        /// </summary>
        /// <param name="x">Int | The chance /1 that this will be true...</param>
        /// <returns>Bool</returns>
        public static bool OneInX(int x)
        {
            var _roll = GetRandom.Int(1, x + 1);
            return _roll.Equals(1);
        }


        /// <summary>
        /// Returns a roll based with values between 0-1, so .5 would be 50% etc...
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ChanceTo1(float value)
        {
            var _roll = GetRandom.Float();
            return _roll <= value;
        }
    }
}