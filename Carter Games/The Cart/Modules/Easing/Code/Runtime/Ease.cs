/*
 * Copyright (c) 2024 Carter Games
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

using System;

namespace CarterGames.Cart.Modules.Easing
{
    /// <summary>
    /// The main ease class used to read an ease value based on a normalised 0-1 value entered.
    /// </summary>
    public class Ease
    {
        /// <summary>
        /// Reads the current value.
        /// </summary>
        /// <param name="data">The ease data to read from.</param>
        /// <param name="current">The current value between 0-1 to read from.</param>
        /// <returns>The value eased.</returns>
        public static double ReadValue(EaseData data, double current)
        {
            return ReadValue(data.easeType, current);
        }


        /// <summary>
        /// Reads the current value.
        /// </summary>
        /// <param name="type">The ease type to read from.</param>
        /// <param name="current">The current value between 0-1 to read from.</param>
        /// <returns>The value eased.</returns>
        public static double ReadValue(EaseType type, double current)
        {
            switch (type)
            {
                case EaseType.None:
                    return current;
                case EaseType.Linear:
                    return Linear(current);
                case EaseType.InSine:
                    return EaseInSine(current);
                case EaseType.InCubic:
                    return EaseInCubic(current);
                case EaseType.InQuint:
                    return EaseInQuint(current);
                case EaseType.InCirc:
                    return EaseInCirc(current);
                case EaseType.InElastic:
                    return EaseInElastic(current);
                case EaseType.InQuad:
                    return EaseInQuad(current);
                case EaseType.InQuart:
                    return EaseInQuart(current);
                case EaseType.InExpo:
                    return EaseInExpo(current);
                case EaseType.InBack:
                    return EaseInBack(current);
                case EaseType.InBounce:
                    return EaseInBounce(current);
                case EaseType.OutSine:
                    return EaseOutSine(current);
                case EaseType.OutCubic:
                    return EaseOutCubic(current);
                case EaseType.OutQuint:
                    return EaseOutQuint(current);
                case EaseType.OutCirc:
                    return EaseOutCirc(current);
                case EaseType.OutElastic:
                    return EaseOutElastic(current);
                case EaseType.OutQuad:
                    return EaseOutQuad(current);
                case EaseType.OutQuart:
                    return EaseOutQuart(current);
                case EaseType.OutExpo:
                    return EaseOutExpo(current);
                case EaseType.OutBack:
                    return EaseOutBack(current);
                case EaseType.OutBounce:
                    return EaseOutBounce(current);
                case EaseType.InOutSine:
                    return EaseInOutSine(current);
                case EaseType.InOutCubic:
                    return EaseInOutCubic(current);
                case EaseType.InOutQuint:
                    return EaseInOutQuint(current);
                case EaseType.InOutCirc:
                    return EaseInOutCirc(current);
                case EaseType.InOutElastic:
                    return EaseInOutElastic(current);
                case EaseType.InOutQuad:
                    return EaseInOutQuad(current);
                case EaseType.InOutQuart:
                    return EaseInOutQuart(current);
                case EaseType.InOutExpo:
                    return EaseInOutExpo(current);
                case EaseType.InOutBack:
                    return EaseInOutBack(current);
                case EaseType.InOutBounce:
                    return EaseInOutBounce(current);
                default:
                    return current;
            }
        }


        /// <summary>
        /// Reads the current value.
        /// </summary>
        /// <param name="type">The ease type to read from.</param>
        /// <param name="current">The current value between 0-1 to read from.</param>
        /// <returns>The value eased.</returns>
        public static double ReadValue(InEaseData type, double current)
        {
            return ReadValue(type.easeType, true, current);
        }


        /// <summary>
        /// Reads the current value.
        /// </summary>
        /// <param name="type">The ease type to read from.</param>
        /// <param name="current">The current value between 0-1 to read from.</param>
        /// <returns>The value eased.</returns>
        public static double ReadValue(OutEaseData type, double current)
        {
            return ReadValue(type.easeType, false, current);
        }


        /// <summary>
        /// Reads the current value.
        /// </summary>
        /// <param name="type">The ease type to read from.</param>
        /// <param name="current">The current value between 0-1 to read from.</param>
        /// <returns>The value eased.</returns>
        public static double ReadValue(SimpleEaseType type, bool isIn, double current)
        {
            switch (type)
            {
                case SimpleEaseType.None:
                    return current;
                case SimpleEaseType.Linear:
                    return Linear(current);
                case SimpleEaseType.Sine:
                    return isIn ? EaseInSine(current) : EaseOutSine(current);
                case SimpleEaseType.Cubic:
                    return isIn ? EaseInCubic(current) : EaseOutCubic(current);
                case SimpleEaseType.Quint:
                    return isIn ? EaseInQuint(current) : EaseOutQuint(current);
                case SimpleEaseType.Circ:
                    return isIn ? EaseInCirc(current) : EaseOutCirc(current);
                case SimpleEaseType.Elastic:
                    return isIn ? EaseInElastic(current) : EaseOutElastic(current);
                case SimpleEaseType.Quad:
                    return isIn ? EaseInQuad(current) : EaseOutQuad(current);
                case SimpleEaseType.Quart:
                    return isIn ? EaseInQuart(current) : EaseOutQuart(current);
                case SimpleEaseType.Expo:
                    return isIn ? EaseInExpo(current) : EaseOutExpo(current);
                case SimpleEaseType.Back:
                    return isIn ? EaseInBack(current) : EaseOutBack(current);
                case SimpleEaseType.Bounce:
                    return isIn ? EaseInBounce(current) : EaseOutBounce(current);
                default:
                    return current;
            }
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Ease Calculation Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static double Linear(double value, double min = 0d, double max = 1d)
        {
            return value / (max - min);
        }

        private static double EaseInSine(double x)
        {
            return 1 - Math.Cos((x * Math.PI) / 2);
        }

        private static double EaseInCubic(double x)
        {
            return x * x * x;
        }

        private static double EaseInQuint(double x)
        {
            return x * x * x * x * x;
        }

        private static double EaseInCirc(double x)
        {
            return 1 - Math.Sqrt(1 - Math.Pow(x, 2));
        }

        private static double EaseInElastic(double x)
        {
            const double c4 = (2 * Math.PI) / 3;

            return x == 0 
                ? 0 
                : Math.Abs(x - 1) < .0001
                    ? 1 
                    : -Math.Pow(2, 10 * x - 10) * Math.Sin((float)(x * 10 - 10.75) * c4);
        }

        private static double EaseInQuad(double x)
        {
            return x * x;
        }

        private static double EaseInQuart(double x)
        {
            return x * x * x * x;
        }

        private static double EaseInExpo(double x)
        {
            return x == 0 ? 0 : Math.Pow(2, 10 * x - 10);
        }

        private static double EaseInBack(double x)
        {
            const double c1 = 1.70158;
            const double c3 = c1 + 1;

            return c3 * x * x * x - c1 * x * x;
        }

        private static double EaseInBounce(double x)
        {
            return 1 - EaseOutBounce(1 - x);
        }

        private static double EaseOutSine(double x)
        {
            return Math.Sin((x * Math.PI) / 2);
        }

        private static double EaseOutCubic(double x)
        {
            return 1 - Math.Pow(1 - x, 3);
        }

        private static double EaseOutQuint(double x)
        {
            return 1 - Math.Pow(1 - x, 5);
        }

        private static double EaseOutCirc(double x)
        {
            return 1 - Math.Sqrt(1 - Math.Pow(x - 1, 2));
        }

        private static double EaseOutElastic(double x)
        {
            const double c4 = (2 * Math.PI) / 3;

            return x == 0 
                ? 0 
                : Math.Abs(x - 1) < .0001
                    ? 1 
                    : Math.Pow(2, -10 * x) * Math.Sin((float)(x * 10 - .75) * c4) + 1;
        }

        private static double EaseOutQuad(double x)
        {
            return 1 - (1 - x) * (1 - x);
        }

        private static double EaseOutQuart(double x)
        {
            return 1 - Math.Pow(1 - x, 4);
        }

        private static double EaseOutExpo(double x)
        {
            return Math.Abs(x - 1) < .0001 ? 1 : 1 - Math.Pow(2, -10 * x);
        }

        private static double EaseOutBack(double x)
        {
            const double c1 = 1.70158;
            const double c3 = c1 + 1;

            return 1 + c3 * Math.Pow(x - 1, 3) + c1 * Math.Pow(x - 1, 2);
        }

        private static double EaseOutBounce(double x)
        {
            const double n1 = 7.5625;
            const double d1 = 2.75;

            if (x < 1 / d1)
                return n1 * x * x;
            if (x < 2 / d1)
                return n1 * (x -= 1.5 / d1) * x + .75;
            if (x < 2.5 / d1)
                return n1 * (x -= 2.25 / d1) * x + .9375;

            return n1 * (x -= 2.625 / d1) * x + .984375;
        }

        private static double EaseInOutSine(double x)
        {
            return -(Math.Cos(Math.PI * x) - 1) / 2;
        }

        private static double EaseInOutCubic(double x)
        {
            return x < .5 ? 4 * x * x * x : 1 - Math.Pow(-2 * x + 2, 3) / 2;
        }

        private static double EaseInOutQuint(double x)
        {
            return x < .5 ? 16 * x * x * x * x * x : 1 - Math.Pow(-2 * x + 2, 5) / 2;
        }

        private static double EaseInOutCirc(double x)
        {
            return x < .5
                ? (1 - Math.Sqrt(1 - Math.Pow(2 * x, 2))) / 2
                : (Math.Sqrt(1 - Math.Pow(-2 * x + 2, 2)) + 1) / 2;
        }

        private static double EaseInOutElastic(double x)
        {
            const double c5 = (2 * Math.PI) / 4.5;

            return x == 0 
                ? 0 
                : Math.Abs(x - 1) < .0001
                    ? 1 
                    : x < .5
                        ? -(Math.Pow(2, 20 * x - 10) * Math.Sin((20 * x - 11.125) * c5)) / 2
                        : (Math.Pow(2, -20 * x + 10) * Math.Sin((20 * x - 11.125) * c5)) / 2 + 1;
        }

        private static double EaseInOutQuad(double x)
        {
            return x < .5 ? 2 * x * x : 1 - Math.Pow(-2 * x + 2, 2) / 2;
        }

        private static double EaseInOutQuart(double x)
        {
            return x < .5 ? 8 * x * x * x * x : 1 - Math.Pow(-2 * x + 2, 4) / 2;
        }

        private static double EaseInOutExpo(double x)
        {
            return x == 0 
                ? 0 
                : Math.Abs(x - 1) < .0001
                    ? 1
                    : x < .5 
                        ? Math.Pow(2, 20 * x -10) / 2
                        : (2 - Math.Pow(2, -20 * x + 10)) / 2;

        }

        private static double EaseInOutBack(double x)
        {
            const double c1 = 1.70158;
            const double c2 = c1 + 1.525;

            return x < .5
                ? (Math.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2
                : (Math.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
        }

        private static double EaseInOutBounce(double x)
        {
            return x < .5
                ? (1 - EaseOutBounce(1 - 2 * x)) / 2
                : (1 + EaseOutBounce(2 * x - 1)) / 2;
        }
    }
}