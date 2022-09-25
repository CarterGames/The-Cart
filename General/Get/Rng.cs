using System;
using UnityEngine;
using Random = System.Random;

namespace Scarlet.General
{
    /// <summary>
    /// Gets random values for a lot of common types.
    /// </summary>
    public static class Rng
    {
        /// <summary>
        /// The seed used the generate all the random values. 
        /// </summary>
        /// <remarks>This is intended to help with debugging as you can replicate the seed & get the same results as a user.</remarks>
        public static readonly int Seed = Guid.NewGuid().GetHashCode();


        /// <summary>
        /// The random to call values from.
        /// </summary>
        private static readonly Random R = new Random(Seed);
        
        
        //
        //  Bool
        //
        
        
        /// <summary>
        /// Get a random bool, true or false.
        /// </summary>
        /// <returns>bool</returns>
        public static bool Bool()
        {
            return Convert.ToBoolean(R.Next(1));
        }
        
        
        //
        //  Int
        //
        
        
        /// <summary>
        /// Gets a random int of either 0 or 1.
        /// </summary>
        /// <returns>int</returns>
        public static int Int01()
        {
            return R.Next(1);
        }
        
        
        /// <summary>
        /// Gets a random int with a variance of +/- the entered variance from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>int</returns>
        public static int IntVariance(int startingValue, int variance)
        {
            return R.Next(startingValue - variance, startingValue + variance + 1);
        }
        
        
        /// <summary>
        /// Gets a random int from 0 - the entered max value.
        /// </summary>
        /// <param name="max">The max value the int can be (inclusive).</param>
        /// <returns>int</returns>
        public static int Int(int max)
        {
            return R.Next(max + 1);
        }
        
        
        /// <summary>
        /// Gets a random int from the entered min & max values.
        /// </summary>
        /// <param name="min">The max value the int can be.</param>
        /// <param name="max">The max value the int can be (inclusive).</param>
        /// <returns>int</returns>
        public static int Int(int min, int max)
        {
            return R.Next(min, max + 1);
        }
        
        
        //
        //  Short
        //
        
        
        /// <summary>
        /// Gets a random short of either 0 or 1.
        /// </summary>
        /// <returns>short</returns>
        public static short Short01()
        {
            return Convert.ToInt16(Int01());
        }
        
        
        /// <summary>
        /// Gets a random short with a variance of +/- the entered variance from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>short</returns>
        public static short ShortVariance(short startingValue, short variance)
        {
            return Convert.ToInt16(R.Next(startingValue - variance, startingValue + variance + 1));
        }
        
        
        /// <summary>
        /// Gets a random short from 0 - the entered max value.
        /// </summary>
        /// <param name="max">The max value the short can be (inclusive).</param>
        /// <returns>short</returns>
        public static short Short(short max)
        {
            return Convert.ToInt16(R.Next(max + 1));
        }
        
        
        /// <summary>
        /// Gets a random short from the entered min & max values.
        /// </summary>
        /// <param name="min">The max value the short can be.</param>
        /// <param name="max">The max value the short can be (inclusive).</param>
        /// <returns>short</returns>
        public static short Short(short min, short max)
        {
            return Convert.ToInt16(R.Next(min, max + 1));
        }
        
        
        //
        //  Long
        //
        
        
        /// <summary>
        /// Gets a random long of either 0 or 1.
        /// </summary>
        /// <returns>long</returns>
        public static long Long01()
        {
            return Convert.ToInt64(Int01());
        }
        
        
        /// <summary>
        /// Gets a random long with a variance of +/- the entered variance from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>long</returns>
        public static long LongVariance(long startingValue, long variance)
        {
            return Convert.ToInt64(R.NextDouble() * ((startingValue + variance) - (startingValue - variance)) + (startingValue - variance));
        }
        
        
        /// <summary>
        /// Gets a random long from 0 - the entered max value.
        /// </summary>
        /// <param name="max">The max value the long can be (inclusive).</param>
        /// <returns>long</returns>
        public static long Long(long max)
        {
            return Convert.ToInt64(R.NextDouble() * max);
        }
        
        
        /// <summary>
        /// Gets a random long from the entered min & max values.
        /// </summary>
        /// <param name="min">The max value the long can be.</param>
        /// <param name="max">The max value the long can be (inclusive).</param>
        /// <returns>long</returns>
        public static long Long(long min, long max)
        {
            return Convert.ToInt64(R.NextDouble() * (max - min) + min);
        }
        
        //
        //  Float
        //
        
        /// <summary>
        /// Gets a random float of between 0-1.
        /// </summary>
        /// <returns>float</returns>
        public static float Float01()
        {
            return (float) R.NextDouble() * 1f;
        }
        
        
        /// <summary>
        /// Gets a random float with a variance of +/- the entered variance from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>float</returns>
        public static float FloatVariance(float startingValue, float variance)
        {
            return (float) R.NextDouble() * ((startingValue + variance) - (startingValue - variance)) + (startingValue - variance);
        }
        
        
        /// <summary>
        /// Gets a random float from 0 - the entered max value.
        /// </summary>
        /// <param name="max">The max value the float can be (inclusive).</param>
        /// <returns>float</returns>
        public static float Float(float max)
        {
            return (float) R.NextDouble() * max;
        }
        
        
        /// <summary>
        /// Gets a random float from the entered min & max values.
        /// </summary>
        /// <param name="min">The max value the float can be.</param>
        /// <param name="max">The max value the float can be (inclusive).</param>
        /// <returns>float</returns>
        public static float Float(float min, float max)
        {
            return (float) R.NextDouble() * (max - min) + min;
        }
        
        //
        //  Double
        //
        
        
        /// <summary>
        /// Gets a random double of between 0-1.
        /// </summary>
        /// <returns>double</returns>
        public static double Double01()
        {
            return R.NextDouble() * 1d;
        }
        
        
        /// <summary>
        /// Gets a random double with a variance of +/- the entered variance from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>double</returns>
        public static double DoubleVariance(double startingValue, double variance)
        {
            return R.NextDouble() * ((startingValue + variance) - (startingValue - variance)) + (startingValue - variance);
        }
        
        
        /// <summary>
        /// Gets a random double from 0 - the entered max value.
        /// </summary>
        /// <param name="max">The max value the double can be (inclusive).</param>
        /// <returns>double</returns>
        public static double Double(double max)
        {
            return R.NextDouble() * max;
        }
        
        
        /// <summary>
        /// Gets a random double from the entered min & max values.
        /// </summary>
        /// <param name="min">The max value the double can be.</param>
        /// <param name="max">The max value the double can be (inclusive).</param>
        /// <returns>double</returns>
        public static double Double(double min, double max)
        {
            return R.NextDouble() * (max - min) + min;
        }
        
        
        //
        //  Vector2
        //
        
        
        /// <summary>
        /// Gets a random Vector2 with each value in the vector between 0-1.
        /// </summary>
        /// <returns>Vector2</returns>
        public static Vector2 Vector201()
        {
            return new Vector2((float) R.NextDouble() * 1f, (float) R.NextDouble() * 1f);
        }
        
        
        /// <summary>
        /// Gets a random Vector2 with a variance of +/- the entered variance for every vector value from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>Vector2</returns>
        public static Vector2 Vector2Variance(Vector2 startingValue, float variance)
        {
            return new Vector2(
                (float)R.NextDouble() * ((startingValue.x + variance) - (startingValue.x - variance)) +
                (startingValue.x - variance),
                (float)R.NextDouble() * ((startingValue.y + variance) - (startingValue.y - variance)) +
                (startingValue.y - variance));
        }
        
        
        /// <summary>
        /// Gets a random Vector2 with a variance of +/- the entered variance from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>Vector2</returns>
        public static Vector2 Vector2Variance(Vector2 startingValue, Vector2 variance)
        {
            return new Vector2(
                (float)R.NextDouble() * ((startingValue.x + variance.x) - (startingValue.x - variance.x)) +
                (startingValue.x - variance.x),
                (float)R.NextDouble() * ((startingValue.y + variance.y) - (startingValue.y - variance.y)) +
                (startingValue.y - variance.y));
        }
        
        
        /// <summary>
        /// Gets a random Vector2 with each axis being random from 0 - the entered max value.
        /// </summary>
        /// <param name="max">The max value the short can be (inclusive).</param>
        /// <returns>Vector2</returns>
        public static Vector2 Vector2(float max)
        {
            return new Vector2((float) R.NextDouble() * max, (float) R.NextDouble() * max);
        }
        
        
        /// <summary>
        /// Gets a random Vector2 with each axis being random from 0 - the entered max value.
        /// </summary>
        /// <param name="min">The max value the Vector2 can be on all axis.</param>
        /// <param name="max">The max value the Vector2 can be on all axis (inclusive).</param>
        /// <returns>Vector2</returns>
        public static Vector2 Vector2(float min, float max)
        {
            return new Vector2((float)R.NextDouble() * (max - min) + min, (float)R.NextDouble() * (max - min) + min);
        }
        
        
        /// <summary>
        /// Gets a random Vector2 with each axis being random from the entered min/max value for each axis.
        /// </summary>
        /// <param name="minX">The max value the Vector2 can be on the X axis.</param>
        /// <param name="maxX">The max value the Vector2 can be on the X axis (inclusive).</param>
        /// <param name="minY">The max value the Vector2 can be on the Y axis.</param>
        /// <param name="maxY">The max value the Vector2 can be on the Y axis (inclusive).</param>
        /// <returns>Vector2</returns>
        public static Vector2 Vector2(float minX, float maxX, float minY, float maxY)
        {
            return new Vector2((float)R.NextDouble() * (maxX - minX) + minX,
                (float)R.NextDouble() * (maxY - minY) + minY);
        }
        
        //
        //  Vector3
        //
        
        
        /// <summary>
        /// Gets a random Vector3 with each value in the vector between 0-1.
        /// </summary>
        /// <returns>Vector3</returns>
        public static Vector3 Vector301()
        {
            return new Vector3((float)R.NextDouble() * 1f, (float)R.NextDouble() * 1f, (float)R.NextDouble() * 1f);
        }
        
        
        /// <summary>
        /// Gets a random Vector3 with a variance of +/- the entered variance for every vector value from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>Vector3</returns>
        public static Vector3 Vector3Variance(Vector3 startingValue, float variance)
        {
            return new Vector3(
                (float)R.NextDouble() * ((startingValue.x + variance) - (startingValue.x - variance)) +
                (startingValue.x - variance),
                (float)R.NextDouble() * ((startingValue.y + variance) - (startingValue.y - variance)) +
                (startingValue.y - variance),
                (float)R.NextDouble() * ((startingValue.z + variance) - (startingValue.z - variance)) +
                (startingValue.z - variance));
        }

        
        /// <summary>
        /// Gets a random Vector3 with a variance of +/- the entered variance for every vector value from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>Vector3</returns>
        public static Vector3 Vector3Variance(Vector3 startingValue, Vector3 variance)
        {
            return new Vector3(
                (float)R.NextDouble() * ((startingValue.x + variance.x) - (startingValue.x - variance.x)) +
                (startingValue.x - variance.x),
                (float)R.NextDouble() * ((startingValue.y + variance.y) - (startingValue.y - variance.y)) +
                (startingValue.y - variance.y), 
                (float)R.NextDouble() * ((startingValue.z + variance.z) - (startingValue.z - variance.z)) +
                (startingValue.z - variance.z));
        }

        
        /// <summary>
        /// Gets a random Vector3 from 0 - the entered max value.
        /// </summary>
        /// <param name="max">The max value the Vector3 can be (inclusive).</param>
        /// <returns>Vector3</returns>
        public static Vector3 Vector3(float max)
        {
            return new Vector3((float)R.NextDouble() * max, (float)R.NextDouble() * max, (float)R.NextDouble() * max);
        }

        
        /// <summary>
        /// Gets a random Vector3 from the entered min & max values.
        /// </summary>
        /// <param name="min">The max value the Vector2 can be on all axis.</param>
        /// <param name="max">The max value the Vector2 can be on all axis (inclusive).</param>
        /// <returns>Vector3</returns>
        public static Vector3 Vector3(float min, float max)
        {
            return new Vector3((float)R.NextDouble() * (max - min) + min, (float)R.NextDouble() * (max - min) + min,
                (float)R.NextDouble() * (max - min) + min);
        }

        
        /// <summary>
        /// Gets a random Vector3 from the entered min & max values. 
        /// </summary>
        /// <param name="minX">The max value the Vector3 can be on the X axis.</param>
        /// <param name="maxX">The max value the Vector3 can be on the X axis (inclusive).</param>
        /// <param name="minY">The max value the Vector3 can be on the Y axis.</param>
        /// <param name="maxY">The max value the Vector3 can be on the Y axis (inclusive).</param>
        /// <param name="minZ">The max value the Vector3 can be on the Z axis.</param>
        /// <param name="maxZ">The max value the Vector3 can be on the Z axis (inclusive).</param>
        /// <returns></returns>
        public static Vector3 Vector3(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
        {
            return new Vector3((float)R.NextDouble() * (maxX - minX) + minX,
                (float)R.NextDouble() * (maxY - minY) + minY,
                (float)R.NextDouble() * (maxZ - minZ) + minZ);
        }
        
        
        //
        //  Vector4
        //

        
        /// <summary>
        /// Gets a random Vector4 with each value in the vector between 0-1.
        /// </summary>
        /// <returns>Vector4</returns>
        public static Vector4 Vector401()
        {
            return new Vector4((float)R.NextDouble() * 1f, (float)R.NextDouble() * 1f, (float)R.NextDouble() * 1f,
                (float)R.NextDouble() * 1f);
        }

        
        /// <summary>
        /// Gets a random Vector4 with a variance of +/- the entered variance from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>short</returns>
        public static Vector4 Vector4Variance(Vector4 startingValue, float variance)
        {
            return new Vector4(
                (float)R.NextDouble() * ((startingValue.x + variance) - (startingValue.x - variance)) +
                (startingValue.x - variance),
                (float)R.NextDouble() * ((startingValue.y + variance) - (startingValue.y - variance)) +
                (startingValue.y - variance),
                (float)R.NextDouble() * ((startingValue.z + variance) - (startingValue.z - variance)) +
                (startingValue.z - variance),
                (float)R.NextDouble() * ((startingValue.w + variance) - (startingValue.w - variance)) +
                (startingValue.w - variance));
        }

        
        /// <summary>
        /// Gets a random Vector4 with a variance of +/- the entered variance from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>short</returns>
        public static Vector4 Vector4Variance(Vector4 startingValue, Vector4 variance)
        {
            return new Vector4(
                (float)R.NextDouble() * ((startingValue.x + variance.x) - (startingValue.x - variance.x)) +
                (startingValue.x - variance.x),
                (float)R.NextDouble() * ((startingValue.y + variance.y) - (startingValue.y - variance.y)) +
                (startingValue.y - variance.y), 
                (float)R.NextDouble() * ((startingValue.z + variance.z) - (startingValue.z - variance.z)) +
                (startingValue.z - variance.z),
                (float)R.NextDouble() * ((startingValue.w + variance.w) - (startingValue.w - variance.w)) +
                (startingValue.w - variance.w));
        }

        
        /// <summary>
        /// Gets a random Vector4 from 0 - the entered max value.
        /// </summary>
        /// <param name="max">The max value the Vector4 can be (inclusive).</param>
        /// <returns>short</returns>
        public static Vector4 Vector4(float max)
        {
            return new Vector4((float)R.NextDouble() * max, (float)R.NextDouble() * max, (float)R.NextDouble() * max,
                (float)R.NextDouble() * max);
        }

        
        /// <summary>
        /// Gets a random Vector4 from the entered min & max values.
        /// </summary>
        /// <param name="min">The max value the float can be on all axis.</param>
        /// <param name="max">The max value the float can be on all axis (inclusive).</param>
        /// <returns>Vector4</returns>
        public static Vector4 Vector4(float min, float max)
        {
            return new Vector4((float)R.NextDouble() * (max - min) + min, (float)R.NextDouble() * (max - min) + min,
                (float)R.NextDouble() * (max - min) + min, (float)R.NextDouble() * (max - min) + min);
        }

        
        /// <summary>
        /// Gets a random Vector4 from the entered min & max values.
        /// </summary>
        /// <param name="minX">The max value the Vector4 can be on the X axis.</param>
        /// <param name="maxX">The max value the Vector4 can be on the X axis (inclusive).</param>
        /// <param name="minY">The max value the Vector4 can be on the Y axis.</param>
        /// <param name="maxY">The max value the Vector4 can be on the Y axis (inclusive).</param>
        /// <param name="minZ">The max value the Vector4 can be on the Z axis.</param>
        /// <param name="maxZ">The max value the Vector4 can be on the Z axis (inclusive).</param>
        /// <param name="minW">The max value the Vector4 can be on the W axis.</param>
        /// <param name="maxW">The max value the Vector4 can be on the W axis (inclusive).</param>
        /// <returns>Vector4</returns>
        public static Vector4 Vector4(float minX, float maxX, float minY, float maxY, float minZ, float maxZ,
            float minW, float maxW)
        {
            return new Vector4((float)R.NextDouble() * (maxX - minX) + minX,
                (float)R.NextDouble() * (maxY - minY) + minY,
                (float)R.NextDouble() * (maxZ - minZ) + minZ,
                (float)R.NextDouble() * (maxW - minW) + minW);
        }
    }
}
