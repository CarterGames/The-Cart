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

using CarterGames.Cart.Management;
using CarterGames.Cart.Random.AleaPRNG;
using UnityEngine;

namespace CarterGames.Cart.Random
{
    /// <summary>
    /// Gets random values for a lot of common types.
    /// </summary>
    public static class Rng
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static IRngProvider providerCache;

        
        private static IRngProvider Provider
        {
            get
            {
                if (providerCache != null) return providerCache;
                providerCache = CartSoAssetAccessor.GetAsset<CartSoRuntimeSettings>().RngRngProvider switch
                {
                    RngProviders.Unity => new UnityRngProvider(),
                    RngProviders.System => new SystemRngProvider(),
                    RngProviders.AleaPRNG => new AleaRngProvider(),
                    _ => null   // Not possible as the setup cannot be a not set value...
                };
                return providerCache;
            }
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Bool
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Get a random bool, true or false.
        /// </summary>
        /// <returns>bool</returns>
        public static bool Bool()
        {
            return Provider.Bool;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Int
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets a random int of either 0 or 1.
        /// </summary>
        /// <returns>int</returns>
        public static int Int01()
        {
            return Provider.Int(0, 1);
        }
        
        
        /// <summary>
        /// Gets a random int with a variance of +/- the entered variance from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>int</returns>
        public static int IntVariance(int startingValue, int variance)
        {
            return Provider.Int(startingValue - variance, startingValue + variance);
        }
        
        
        /// <summary>
        /// Gets a random int from 0 - the entered max value.
        /// </summary>
        /// <param name="max">The max value the int can be (inclusive).</param>
        /// <returns>int</returns>
        public static int Int(int max)
        {
            return Provider.Int(0, max);
        }
        
        
        /// <summary>
        /// Gets a random int from the entered min & max values.
        /// </summary>
        /// <param name="min">The max value the int can be.</param>
        /// <param name="max">The max value the int can be (inclusive).</param>
        /// <returns>int</returns>
        public static int Int(int min, int max)
        {
            return Provider.Int(min, max);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Float
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets a random float of between 0-1.
        /// </summary>
        /// <returns>float</returns>
        public static float Float01()
        {
            return Provider.Float(0f, 1f);
        }
        
        
        /// <summary>
        /// Gets a random float with a variance of +/- the entered variance from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>float</returns>
        public static float FloatVariance(float startingValue, float variance)
        {
            return Provider.Float(startingValue - variance, startingValue + variance);
        }
        
        
        /// <summary>
        /// Gets a random float from 0 - the entered max value.
        /// </summary>
        /// <param name="max">The max value the float can be (inclusive).</param>
        /// <returns>float</returns>
        public static float Float(float max)
        {
            return Provider.Float(0f, max);
        }
        
        
        /// <summary>
        /// Gets a random float from the entered min & max values.
        /// </summary>
        /// <param name="min">The max value the float can be.</param>
        /// <param name="max">The max value the float can be (inclusive).</param>
        /// <returns>float</returns>
        public static float Float(float min, float max)
        {
            return Provider.Float(min, max);
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Double
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets a random double of between 0-1.
        /// </summary>
        /// <returns>double</returns>
        public static double Double01()
        {
            return Provider.Double(0d, 1d);
        }
        
        
        /// <summary>
        /// Gets a random double with a variance of +/- the entered variance from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>double</returns>
        public static double DoubleVariance(double startingValue, double variance)
        {
            return Provider.Double(startingValue - variance, startingValue + variance);
        }
        
        
        /// <summary>
        /// Gets a random double from 0 - the entered max value.
        /// </summary>
        /// <param name="max">The max value the double can be (inclusive).</param>
        /// <returns>double</returns>
        public static double Double(double max)
        {
            return Provider.Double(0d, max);
        }
        
        
        /// <summary>
        /// Gets a random double from the entered min & max values.
        /// </summary>
        /// <param name="min">The max value the double can be.</param>
        /// <param name="max">The max value the double can be (inclusive).</param>
        /// <returns>double</returns>
        public static double Double(double min, double max)
        {
            return Provider.Double(min, max);
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Vector2
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets a random Vector2 with each value in the vector between 0-1.
        /// </summary>
        /// <returns>Vector2</returns>
        public static Vector2 Vector201()
        {
            return new Vector2(Float01(), Float01());
        }
        
        
        /// <summary>
        /// Gets a random Vector2 with a variance of +/- the entered variance for every vector value from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>Vector2</returns>
        public static Vector2 Vector2Variance(Vector2 startingValue, float variance)
        {
            var x = FloatVariance(startingValue.x, variance);
            var y = FloatVariance(startingValue.y, variance);

            return new Vector2(x, y);
        }
        
        
        /// <summary>
        /// Gets a random Vector2 with a variance of +/- the entered variance from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>Vector2</returns>
        public static Vector2 Vector2Variance(Vector2 startingValue, Vector2 variance)
        {
            var x = FloatVariance(startingValue.x, variance.x);
            var y = FloatVariance(startingValue.y, variance.y);

            return new Vector2(x, y);
        }
        
        
        /// <summary>
        /// Gets a random Vector2 with each axis being random from 0 - the entered max value.
        /// </summary>
        /// <param name="max">The max value the short can be (inclusive).</param>
        /// <returns>Vector2</returns>
        public static Vector2 Vector2(float max)
        {
            return new Vector2(Float(0f, max), Float(0f, max));
        }
        
        
        /// <summary>
        /// Gets a random Vector2 with each axis being random from 0 - the entered max value.
        /// </summary>
        /// <param name="min">The max value the Vector2 can be on all axis.</param>
        /// <param name="max">The max value the Vector2 can be on all axis (inclusive).</param>
        /// <returns>Vector2</returns>
        public static Vector2 Vector2(float min, float max)
        {
            return new Vector2(Float(min, max), Float(min, max));
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
            return new Vector2(Float(minX, maxX), Float(minY, maxY));
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Vector3
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets a random Vector3 with each value in the vector between 0-1.
        /// </summary>
        /// <returns>Vector3</returns>
        public static Vector3 Vector301()
        {
            return new Vector3(Float01(), Float01(), Float01());
        }
        
        
        /// <summary>
        /// Gets a random Vector3 with a variance of +/- the entered variance for every vector value from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>Vector3</returns>
        public static Vector3 Vector3Variance(Vector3 startingValue, float variance)
        {
            var x = FloatVariance(startingValue.x, variance);
            var y = FloatVariance(startingValue.y, variance);
            var z = FloatVariance(startingValue.z, variance);

            return new Vector3(x, y, z);
        }

        
        /// <summary>
        /// Gets a random Vector3 with a variance of +/- the entered variance for every vector value from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>Vector3</returns>
        public static Vector3 Vector3Variance(Vector3 startingValue, Vector3 variance)
        {
            var x = FloatVariance(startingValue.x, variance.x);
            var y = FloatVariance(startingValue.y, variance.y);
            var z = FloatVariance(startingValue.z, variance.z);

            return new Vector3(x, y, z);
        }

        
        /// <summary>
        /// Gets a random Vector3 from 0 - the entered max value.
        /// </summary>
        /// <param name="max">The max value the Vector3 can be (inclusive).</param>
        /// <returns>Vector3</returns>
        public static Vector3 Vector3(float max)
        {
            return new Vector3(Float(0f, max), Float(0f, max), Float(0f, max));
        }

        
        /// <summary>
        /// Gets a random Vector3 from the entered min & max values.
        /// </summary>
        /// <param name="min">The max value the Vector2 can be on all axis.</param>
        /// <param name="max">The max value the Vector2 can be on all axis (inclusive).</param>
        /// <returns>Vector3</returns>
        public static Vector3 Vector3(float min, float max)
        {
            return new Vector3(Float(min, max), Float(min, max), Float(min, max));
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
            return new Vector3(Float(minX, maxX), Float(minY, maxY), Float(minZ, maxZ));
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Vector4
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets a random Vector4 with each value in the vector between 0-1.
        /// </summary>
        /// <returns>Vector4</returns>
        public static Vector4 Vector401()
        {
            return new Vector4(Float01(), Float01(), Float01(), Float01());
        }

        
        /// <summary>
        /// Gets a random Vector4 with a variance of +/- the entered variance from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>short</returns>
        public static Vector4 Vector4Variance(Vector4 startingValue, float variance)
        {
            var x = FloatVariance(startingValue.x, variance);
            var y = FloatVariance(startingValue.y, variance);
            var z = FloatVariance(startingValue.z, variance);
            var w = FloatVariance(startingValue.w, variance);

            return new Vector4(x, y, z, w);
        }

        
        /// <summary>
        /// Gets a random Vector4 with a variance of +/- the entered variance from the starting value.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="variance">The amount of +/- from the starting value to apply.</param>
        /// <returns>short</returns>
        public static Vector4 Vector4Variance(Vector4 startingValue, Vector4 variance)
        {
            var x = FloatVariance(startingValue.x, variance.x);
            var y = FloatVariance(startingValue.y, variance.y);
            var z = FloatVariance(startingValue.z, variance.z);
            var w = FloatVariance(startingValue.w, variance.w);

            return new Vector4(x, y, z, w);
        }

        
        /// <summary>
        /// Gets a random Vector4 from 0 - the entered max value.
        /// </summary>
        /// <param name="max">The max value the Vector4 can be (inclusive).</param>
        /// <returns>short</returns>
        public static Vector4 Vector4(float max)
        {
            return new Vector4(Float(0f, max), Float(0f, max), Float(0f, max), Float(0f, max));
        }

        
        /// <summary>
        /// Gets a random Vector4 from the entered min & max values.
        /// </summary>
        /// <param name="min">The max value the float can be on all axis.</param>
        /// <param name="max">The max value the float can be on all axis (inclusive).</param>
        /// <returns>Vector4</returns>
        public static Vector4 Vector4(float min, float max)
        {
            return new Vector4(Float(min, max), Float(min, max), Float(min, max), Float(min, max));
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
            return new Vector4(Float(minX, maxX), Float(minY, maxY), Float(minZ, maxZ), Float(minW, maxW));
        }
    }
}
