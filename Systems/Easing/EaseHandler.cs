// ----------------------------------------------------------------------------
// EaseHandler.cs
// 
// Description: A script to handle ease types
// ----------------------------------------------------------------------------

using System;

namespace Scarlet.Easing
{
    // Reference: https://easings.net/
    public sealed class EaseHandler
    {
        public static double Linear(double value, double min = 0d, double max = 1d)
        {
            return value / (max - min);
        }
        
        public static double EaseInSine(double x)
        {
            return 1 - Math.Cos((x * Math.PI) / 2);
        }
        
        public static double EaseInCubic(double x)
        {
            return x * x * x;
        }

        public static double EaseInQuint(double x)
        {
            return x * x * x * x * x;
        }
        
        public static double EaseInCirc(double x)
        {
            return 1 - Math.Sqrt(1 - Math.Pow(x, 2));
        }
        
        public static double EaseInElastic(double x)
        {
            const double c4 = (2 * Math.PI) / 3;

            return x == 0 
                ? 0 
                : Math.Abs(x - 1) < .0001
                    ? 1 
                    : -Math.Pow(2, 10 * x - 10) * Math.Sin((float)(x * 10 - 10.75) * c4);
        }
        
        public static double EaseInQuad(double x)
        {
            return x * x;
        }
        
        public static double EaseInQuart(double x)
        {
            return x * x * x * x;
        }
        
        public static double EaseInExpo(double x)
        {
            return x == 0 ? 0 : Math.Pow(2, 10 * x - 10);
        }
        
        public static double EaseInBack(double x)
        {
            const double c1 = 1.70158;
            const double c3 = c1 + 1;

            return c3 * x * x * x - c1 * x * x;
        }
        
        public static double EaseInBounce(double x)
        {
            return 1 - EaseOutBounce(1 - x);
        }
        
        public static double EaseOutSine(double x)
        {
            return Math.Sin((x * Math.PI) / 2);
        }
        
        public static double EaseOutCubic(double x)
        {
            return 1 - Math.Pow(1 - x, 3);
        }

        public static double EaseOutQuint(double x)
        {
            return 1 - Math.Pow(1 - x, 5);
        }
        
        public static double EaseOutCirc(double x)
        {
            return 1 - Math.Sqrt(1 - Math.Pow(x - 1, 2));
        }
        
        public static double EaseOutElastic(double x)
        {
            const double c4 = (2 * Math.PI) / 3;

            return x == 0 
                ? 0 
                : Math.Abs(x - 1) < .0001
                    ? 1 
                    : Math.Pow(2, -10 * x) * Math.Sin((float)(x * 10 - .75) * c4) + 1;
        }
        
        public static double EaseOutQuad(double x)
        {
            return 1 - (1 - x) * (1 - x);
        }
        
        public static double EaseOutQuart(double x)
        {
            return 1 - Math.Pow(1 - x, 4);
        }
        
        public static double EaseOutExpo(double x)
        {
            return Math.Abs(x - 1) < .0001 ? 1 : 1 - Math.Pow(2, -10 * x);
        }
        
        public static double EaseOutBack(double x)
        {
            const double c1 = 1.70158;
            const double c3 = c1 + 1;

            return 1 + c3 * Math.Pow(x - 1, 3) + c1 * Math.Pow(x - 1, 2);
        }
        
        public static double EaseOutBounce(double x)
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
        
        public static double EaseInOutSine(double x)
        {
            return -(Math.Cos(Math.PI * x) - 1) / 2;
        }
        
        public static double EaseInOutCubic(double x)
        {
            return x < .5 ? 4 * x * x * x : 1 - Math.Pow(-2 * x + 2, 3) / 2;
        }

        public static double EaseInOutQuint(double x)
        {
            return x < .5 ? 16 * x * x * x * x * x : 1 - Math.Pow(-2 * x + 2, 5) / 2;
        }
        
        public static double EaseInOutCirc(double x)
        {
            return x < .5
                ? (1 - Math.Sqrt(1 - Math.Pow(2 * x, 2))) / 2
                : (Math.Sqrt(1 - Math.Pow(-2 * x + 2, 2)) + 1) / 2;
        }
        
        public static double EaseInOutElastic(double x)
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
        
        public static double EaseInOutQuad(double x)
        {
            return x < .5 ? 2 * x * x : 1 - Math.Pow(-2 * x + 2, 2) / 2;
        }
        
        public static double EaseInOutQuart(double x)
        {
            return x < .5 ? 8 * x * x * x * x : 1 - Math.Pow(-2 * x + 2, 4) / 2;
        }
        
        public static double EaseInOutExpo(double x)
        {
            return x == 0 
                ? 0 
                : Math.Abs(x - 1) < .0001
                    ? 1
                    : x < .5 
                        ? Math.Pow(2, 20 * x -10) / 2
                        : (2 - Math.Pow(2, -20 * x + 10)) / 2;

        }
        
        public static double EaseInOutBack(double x)
        {
            const double c1 = 1.70158;
            const double c2 = c1 + 1.525;

            return x < .5
                ? (Math.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2
                : (Math.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
        }
        
        public static double EaseInOutBounce(double x)
        {
            return x < .5
                ? (1 - EaseOutBounce(1 - 2 * x)) / 2
                : (1 + EaseOutBounce(2 * x - 1)) / 2;
        }
    }
}