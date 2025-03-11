#if CARTERGAMES_CART_MODULE_DICE

/*
 * Copyright (c) 2025 Carter Games
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using CarterGames.Cart.Core.Random;

namespace CarterGames.Cart.Modules.Dice
{
    /// <summary>
    /// A helper class for getting common dice rolls.
    /// </summary>
    public static class Dice
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Single Rolls
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Rolls a single D4 and returns the result...
        /// </summary>
        /// <returns>Int</returns>
        public static int D4() => GetSingleRoll(4);


        /// <summary>
        /// Rolls a single D6 and returns the result...
        /// </summary>
        /// <returns>Int</returns>
        public static int D6() => GetSingleRoll(6);


        /// <summary>
        /// Rolls a single D8 and returns the result...
        /// </summary>
        /// <returns>Int</returns>
        public static int D8() => GetSingleRoll(8);


        /// <summary>
        /// Rolls a single D10 and returns the result...
        /// </summary>
        /// <returns>Int</returns>
        public static int D10() => GetSingleRoll(10);


        /// <summary>
        /// Rolls a single D12 and returns the result...
        /// </summary>
        /// <returns>Int</returns>
        public static int D12() => GetSingleRoll(12);


        /// <summary>
        /// Rolls a single D20 and returns the result...
        /// </summary>
        /// <returns>Int</returns>
        public static int D20() => GetSingleRoll(20);


        /// <summary>
        /// Rolls a single D100 and returns the result...
        /// </summary>
        /// <returns>Int</returns>
        public static int D100() => GetSingleRoll(100);


        /// <summary>
        /// Rolls a single Custom Dice and returns the result...
        /// </summary>
        /// <param name="sidesToDice">The number of sides the dice should have</param>
        /// <returns>Int</returns>
        public static int Custom(int sidesToDice) => GetSingleRoll(sidesToDice);


        /// <summary>
        /// Rolls a single dice...
        /// </summary>
        /// <param name="sidesToDice">The amount of sides to the dice...</param>
        /// <returns>The roll</returns>
        private static int GetSingleRoll(int sidesToDice) => Rng.Int(1, sidesToDice);

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Multiple Rolls
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Rolls multiple D4 and returns the result...
        /// </summary>
        /// <param name="numberOfRolls">Int | Number of times to roll</param>
        /// <returns>List of Ints</returns>
        public static int[] D4(int numberOfRolls) => GetMultipleRolls(100, numberOfRolls);


        /// <summary>
        /// Rolls multiple D6 and returns the result...
        /// </summary>
        /// <param name="numberOfRolls">Int | Number of times to roll</param>
        /// <returns>List of Ints</returns>
        public static int[] D6(int numberOfRolls) => GetMultipleRolls(100, numberOfRolls);


        /// <summary>
        /// Rolls multiple D8 and returns the result...
        /// </summary>
        /// <param name="numberOfRolls">Int | Number of times to roll</param>
        /// <returns>List of Ints</returns>
        public static int[] D8(int numberOfRolls) => GetMultipleRolls(100, numberOfRolls);


        /// <summary>
        /// Rolls multiple D10 and returns the result...
        /// </summary>
        /// <param name="numberOfRolls">Int | Number of times to roll</param>
        /// <returns>List of Ints</returns>
        public static int[] D10(int numberOfRolls) => GetMultipleRolls(10, numberOfRolls);


        /// <summary>
        /// Rolls multiple D12 and returns the result...
        /// </summary>
        /// <param name="numberOfRolls">Int | Number of times to roll</param>
        /// <returns>List of Ints</returns>
        public static int[] D12(int numberOfRolls) => GetMultipleRolls(12, numberOfRolls);


        /// <summary>
        /// Rolls multiple D20 and returns the result...
        /// </summary>
        /// <param name="numberOfRolls">Int | Number of times to roll</param>
        /// <returns>List of Ints</returns>
        public static int[] D20(int numberOfRolls) => GetMultipleRolls(20, numberOfRolls);


        /// <summary>
        /// Rolls multiple D100 and returns the result...
        /// </summary>
        /// <param name="numberOfRolls">Int | Number of times to roll</param>
        /// <returns>List of Ints</returns>
        public static int[] D100(int numberOfRolls) => GetMultipleRolls(100, numberOfRolls);


        /// <summary>
        /// Rolls multiple Custom Dice and returns the result...
        /// </summary>
        /// <param name="numberOfRolls">Int | Number of times to roll</param>
        /// <param name="sidesToDice">The amount of sides to the dice...</param>
        /// <returns>List of Ints</returns>
        public static int[] Custom(int sidesToDice, int numberOfRolls) => GetMultipleRolls(sidesToDice, numberOfRolls);


        /// <summary>
        /// Rolls multiple dice...
        /// </summary>
        /// <param name="sidesToDice">The amount of sides to the dice...</param>
        /// <param name="numberOfRolls">The number of rolls of the dice...</param>
        /// <returns>The roll</returns>
        private static int[] GetMultipleRolls(int sidesToDice, int numberOfRolls)
        {
            var temp = new int[numberOfRolls];

            for (var i = 0; i < numberOfRolls; i++)
            {
                temp[i] = Rng.Int(1, sidesToDice);
            }

            return temp;
        }
    }
}

#endif