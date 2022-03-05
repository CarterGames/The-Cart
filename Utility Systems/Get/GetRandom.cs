using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace JTools
{
    /// <summary>
    /// Static Class | Get a random.... choose a property/method to get a random value for it...
    /// </summary>
    public static class GetRandom
    {
        /// <summary>
        /// The characters to use in the random string getters...
        /// </summary>
        private const string NormalGlyphs = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        
        
        /// <summary>
        /// Get a random float between the set params.
        /// </summary>
        /// <param name="_min">The min value the float can be.</param>
        /// <param name="_max">The max value the float can be.</param>
        /// <returns>Random float between the defined bounds.</returns>
        public static float Float(float _min = 0f, float _max = 1f)
        {
            return Random.Range(_min, _max);
        }

        
        /// <summary>
        /// Get a random int between the set params.
        /// </summary>
        /// <param name="_min">The min value the int can be.</param>
        /// <param name="_max">The max value the int can be.</param>
        /// <param name="inclusive">Should it NOT be inclusive.</param>
        /// <returns>Random Int between the defined bounds.</returns>
        public static int Int(int _min = 0, int _max = 1, bool inclusive = true)
        {
            // if ? true : false;
            return inclusive 
                ? Random.Range(_min, _max + 1) 
                : Random.Range(_min, _max);
        }
        
        
        /// <summary>
        /// Get a random int between the set params.
        /// </summary>
        /// <param name="_max">The max value the int can be.</param>
        /// <returns>Random Int between the defined bounds.</returns>
        public static int Int<T>(List<T> _max)
        {
            return Random.Range(0, _max.Count - 1);
        }
        
        
        /// <summary>
        /// Get a random int between the set params.
        /// </summary>
        /// <param name="_max">The max value the int can be.</param>
        /// <returns>Random Int between the defined bounds.</returns>
        public static int Int<T>(T[] _max)
        {
            return Random.Range(0, _max.Length - 1);
        }
        
        
        /// <summary>
        /// Get a random double between the set params.
        /// </summary>
        /// <param name="_min">The min value the double can be.</param>
        /// <param name="_max">The max value the double can be.</param>
        /// <returns>Random Double between the defined bounds.</returns>
        public static double Double(double _min = 0f, double _max = 1f)
        {
            return Random.Range((float)_min, (float)_max);
        }


        /// <summary>
        /// Random Color (0-1)
        /// </summary>
        public static Color Color => new Color(Float(), Float(), Float(), 1f);

        
        /// <summary>
        /// Random Color32 (0-255)
        /// </summary>
        public static Color32 Color32 => Color;


        /// <summary>
        /// Random Vector2 (user defined min/max)
        /// </summary>
        /// <param name="min">The min value a coord can be</param>
        /// <param name="max">The max value a coord can be</param>
        /// <returns>A random Vector2 within the min/max defined</returns>
        public static Vector2 Vector2(float min, float max)
        {
            return new Vector2(Float(min, max), Float(min, max));
        }

        
        /// <summary>
        /// Random Vector2 (user defined min/max for each axis)
        /// </summary>
        /// <param name="minX">The min value an X coord can be</param>
        /// <param name="maxX">The max value an X coord can be</param>
        /// <param name="minY">The min value an Y coord can be</param>
        /// <param name="maxY">The max value an Y coord can be</param>
        /// <returns>A random Vector2 within the min/max defined</returns>
        public static Vector2 Vector2(float minX, float maxX, float minY, float maxY)
        {
            return new Vector2(Float(minX, maxX), Float(minY, maxY));
        }

        
        /// <summary>
        /// Random Vector2 (user defined min/max)
        /// </summary>
        /// <param name="org">The Vector2 to edit</param>
        /// <param name="min">The min value a coord can be</param>
        /// <param name="max">The max value a coord can be</param>
        /// <returns>A random Vector3 within the min/max defined</returns>
        public static Vector2 Vector2(Vector2 org, float min, float max)
        {
            return new Vector2(Float(org.x - min, org.x + max), Float(min, max));
        }

        
        /// <summary>
        /// Random Vector2 (user defined min/max for each axis)
        /// </summary>
        /// <param name="org">The Vector2 to edit</param>
        /// <param name="minX">The min value an X coord can be</param>
        /// <param name="maxX">The max value an X coord can be</param>
        /// <param name="minY">The min value an Y coord can be</param>
        /// <param name="maxY">The max value an Y coord can be</param>
        /// <returns>A random Vector3 within the min/max defined</returns>
        public static Vector2 Vector2(Vector2 org, float minX, float maxX, float minY, float maxY)
        {
            return new Vector2(Float(org.x - minX, org.x + maxX), Float(org.y - minY, org.y + maxY));
        }
        
        
        
        /// <summary>
        /// Gets a random point on the line provided...
        /// </summary>
        /// <param name="_a">The start pos of the line...</param>
        /// <param name="line">The line to get from (use GetVector.Line not normalised version)</param>
        /// <returns>Vector3</returns>
        public static Vector3 OnLine(Vector3 _a, Vector3 line)
        {
            return (_a + (Float() * line));
        }
        
        
        
        /// <summary>
        /// Random Vector3 (user defined min/max)
        /// </summary>
        /// <param name="min">The min value a coord can be</param>
        /// <param name="max">The max value a coord can be</param>
        /// <returns>A random Vector3 within the min/max defined</returns>
        public static Vector3 Vector3(float min, float max)
        {
            return new Vector3(Float(min, max), Float(min, max), Float(min, max));
        }



        /// <summary>
        /// Random Vector3 (user defined min/max)
        /// </summary>
        /// <param name="min">The min value a coord can be</param>
        /// <param name="max">The max value a coord can be</param>
        /// <returns>A random Vector3 within the min/max defined</returns>
        public static Vector3 Vector3(Vector3 min, Vector3 max)
        {
            return new Vector3(Float(min.x, max.x), Float(min.y, max.y), Float(min.z, max.z));
        }

        
        /// <summary>
        /// Random Vector3 (user defined min/max for each axis)
        /// </summary>
        /// <param name="minX">The min value an X coord can be</param>
        /// <param name="maxX">The max value an X coord can be</param>
        /// <param name="minY">The min value an Y coord can be</param>
        /// <param name="maxY">The max value an Y coord can be</param>
        /// <param name="minZ">The min value an Z coord can be</param>
        /// <param name="maxZ">The max value an Z coord can be</param>
        /// <returns>A random Vector3 within the min/max defined</returns>
        public static Vector3 Vector3(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
        {
            return new Vector3(Float(minX, maxX), Float(minY, maxY), Float(minZ, maxZ));
        }

        
        /// <summary>
        /// Random Vector3 (user defined min/max)
        /// </summary>
        /// <param name="org">The Vector3 to edit</param>
        /// <param name="min">The min value a coord can be</param>
        /// <param name="max">The max value a coord can be</param>
        /// <returns>A random Vector3 within the min/max defined</returns>
        public static Vector3 Vector3(Vector3 org, float min, float max)
        {
            return new Vector3(Float(org.x - min, org.x + max), Float(org.y - min, org.y + max), Float(org.z - min, org.z + max));
        }

        
        /// <summary>
        /// Random Vector3 (user defined min/max for each axis)
        /// </summary>
        /// <param name="org">The Vector3 to edit</param>
        /// <param name="minX">The min value an X coord can be</param>
        /// <param name="maxX">The max value an X coord can be</param>
        /// <param name="minY">The min value an Y coord can be</param>
        /// <param name="maxY">The max value an Y coord can be</param>
        /// <param name="minZ">The min value an Z coord can be</param>
        /// <param name="maxZ">The max value an Z coord can be</param>
        /// <returns>A random Vector3 within the min/max defined</returns>
        public static Vector3 Vector3(Vector3 org, float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
        {
            return new Vector3(Float(org.x - minX, org.x + maxX), Float(org.y - minY, org.y + maxY), Float(org.z - minZ, org.z + maxZ));
        }

        
        /// <summary>
        /// Random Vector4 (user defined min/max)
        /// </summary>
        /// <param name="min">The min value a coord can be</param>
        /// <param name="max">The max value a coord can be</param>
        /// <returns>A random Vector4 within the min/max defined</returns>
        public static Vector4 Vector4(float min, float max)
        {
            return new Vector4(Float(min, max), Float(min, max), Float(min, max), Float(min, max));
        }

        
        /// <summary>
        /// Random Vector4 (user defined min/max for each axis)
        /// </summary>
        /// <param name="minX">The min value an X coord can be</param>
        /// <param name="maxX">The max value an X coord can be</param>
        /// <param name="minY">The min value an Y coord can be</param>
        /// <param name="maxY">The max value an Y coord can be</param>
        /// <param name="minZ">The min value an Z coord can be</param>
        /// <param name="maxZ">The max value an Z coord can be</param>
        /// <param name="minW">The min value an W coord can be</param>
        /// <param name="maxW">The max value an W coord can be</param>
        /// <returns>A random Vector3 within the min/max defined</returns>
        public static Vector4 Vector4(float minX, float maxX, float minY, float maxY, float minZ, float maxZ, float minW, float maxW)
        {
            return new Vector4(Float(minX, maxX), Float(minY, maxY), Float(minZ, maxZ), Float(minW, maxW));
        }

        
        /// <summary>
        /// Random Vector4 (user defined min/max)
        /// </summary>
        /// <param name="org">The Vector4 to edit</param>
        /// <param name="min">The min value a coord can be</param>
        /// <param name="max">The max value a coord can be</param>
        /// <returns>A random Vector4 within the min/max defined</returns>
        public static Vector4 Vector4(Vector4 org, float min, float max)
        {
            return new Vector4(Float(org.x - min, org.x + max), Float(min, max), Float(org.z - min, org.z + max), Float(org.w - min, org.w + max));
        }

        
        /// <summary>
        /// Random Vector4 (user defined min/max for each axis)
        /// </summary>
        /// <param name="org">The Vector4 to edit</param>
        /// <param name="minX">The min value an X coord can be</param>
        /// <param name="maxX">The max value an X coord can be</param>
        /// <param name="minY">The min value an Y coord can be</param>
        /// <param name="maxY">The max value an Y coord can be</param>
        /// <param name="minZ">The min value an Z coord can be</param>
        /// <param name="maxZ">The max value an Z coord can be</param>
        /// <param name="minW">The min value an W coord can be</param>
        /// <param name="maxW">The max value an W coord can be</param>
        /// <returns>A random Vector3 within the min/max defined</returns>
        public static Vector4 Vector4(Vector4 org, float minX, float maxX, float minY, float maxY, float minZ, float maxZ, float minW, float maxW)
        {
            return new Vector4(Float(org.x - minX, org.x + maxX), Float(org.y - minY, org.y + maxY), Float(org.z - minZ, org.z + maxZ), Float(org.w - minW, org.w + maxW));
        }
        
        
        /// <summary>
        /// Gets a random element from the list entered and returns it.
        /// </summary>
        /// <param name="list">List to use</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>An element from the list</returns>
        public static T FromList<T>(List<T> list)
        {
            return list[Int(0, list.Count, false)];
        }


        /// <summary>
        /// Gets a random element from the array entered and returns it.
        /// </summary>
        /// <param name="array">List to use</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>An element from the list</returns>
        public static T FromArray<T>(T[] array)
        {
            return array[Int(0, array.Length, false)];
        }


        /// <summary>
        /// Gets a random order of the desired length.
        /// </summary>
        /// <param name="lenght">The number of elements needed to randomly order.</param>
        /// <returns>Array of ints</returns>
        public static int[] Order(int lenght)
        {
            var _order = new List<int>();

            for (var i = 0; i < lenght; i++)
                _order.Add(i);

            var _rand = new System.Random();
            var _newOrder = _order.OrderBy(a => _rand.Next());

            return _newOrder.ToArray();
        }

        
        /// <summary>
        /// Gets a random list of ints with the desired values...
        /// </summary>
        /// <param name="numberOfElements">How many you want in the list</param>
        /// <param name="min">The min value that can be chosen</param>
        /// <param name="max">The max value that can be chosen</param>
        /// <returns>Random List of Ints</returns>
        public static List<int> ListOfInts(int numberOfElements, int min = 0, int max = 1)
        {
            var _l = new List<int>();
            var _total = 0;
            
            for (var i = 0; i < numberOfElements; i++)
            {
                _l.Add(Int(min, max));
                _total += _l[i];
            }

            return _l;
        }
        
        
        /// <summary>
        /// Gets a random list of ints with the desired values keeping the sum of all elements in the list to the entered value.
        /// </summary>
        /// <param name="numberOfElements">How many you want in the list</param>
        /// <param name="sum">The sum that all the elements in the list show total to</param>
        /// <param name="min">The min value that can be chosen</param>
        /// <param name="max">The max value that can be chosen</param>
        /// <returns>Random List of Ints</returns>
        public static List<int> ListOfInts(int numberOfElements, int sum, int min = 0, int max = 1)
        {
            var _l = new List<int>();
            var _total = 0;
            
            for (var i = 0; i < numberOfElements; i++)
            {
                _l.Add(Int(min, max));
                _total += _l[i];
            }

            while (_total > sum)
            {
                var _i = GetLowHigh.GetHighestIndex(_l);
                _l[_i]--;
                _total--;
            }

            while (_total < sum)
            {
                var _i = GetLowHigh.GetLowestIndex(_l);
                _l[_i]++;
                _total++;
            }

            return _l;
        }
        
        
        /// <summary>
        /// Gets a random list of ints with the desired values...
        /// </summary>
        /// <param name="numberOfElements">How many you want in the list</param>
        /// <param name="min">The min value that can be chosen</param>
        /// <param name="max">The max value that can be chosen</param>
        /// <returns>Random List of Floats</returns>
        public static List<float> ListOfFloats(int numberOfElements, float min = 0f, float max = 1f)
        {
            var _l = new List<float>();
            var _total = 0f;
            
            for (var i = 0; i < numberOfElements; i++)
            {
                _l.Add(Float(min, max));
                _total += _l[i];
            }

            return _l;
        }
        
        
        /// <summary>
        /// Gets a random list of ints with the desired values...
        /// </summary>
        /// <param name="numberOfElements">How many you want in the list</param>
        /// <param name="min">The min value that can be chosen</param>
        /// <param name="max">The max value that can be chosen</param>
        /// <returns>Random List of Doubles</returns>
        public static List<double> ListOfDoubles(int numberOfElements, double min = 0f, double max = 1f)
        {
            var _l = new List<double>();
            double _total = 0f;
            
            for (var i = 0; i < numberOfElements; i++)
            {
                _l.Add(Double(min, max));
                _total += _l[i];
            }

            return _l;
        }
        
        
        /// <summary>
        /// Gets a random list of ints with the desired values...
        /// </summary>
        /// <param name="numberOfElements">How many you want in the list</param>
        /// <param name="min">The min value that can be chosen</param>
        /// <param name="max">The max value that can be chosen</param>
        /// <returns>Random List of Doubles</returns>
        public static List<string> ListOfStrings(int numberOfElements, int minStringLength, int maxStringLength)
        {
            var _l = new List<string>();

            for (var i = 0; i < numberOfElements; i++)
                _l.Add(String(Int(minStringLength, maxStringLength)));

            return _l;
        }
        
        
        
        /// <summary>
        /// Gets a random list of ints with the desired values...
        /// </summary>
        /// <param name="numberOfElements">How many you want in the list</param>
        /// <param name="min">The min value that can be chosen</param>
        /// <param name="max">The max value that can be chosen</param>
        /// <returns>Random List of Doubles</returns>
        public static List<string> ListOfStrings(int numberOfElements, string prefix, int minStringLength, int maxStringLength)
        {
            var _l = new List<string>();

            for (var i = 0; i < numberOfElements; i++)
                _l.Add(String(prefix,Int(minStringLength, maxStringLength)));

            return _l;
        }
        
        
        
        /// <summary>
        /// Gets a random string of the length entered, good for ID's for things...
        /// </summary>
        /// <param name="length">How many characters you want to have in the string...</param>
        /// <returns>String</returns>
        public static string String(int length = 10)
        {
            var _text = string.Empty;

            for (var i = 0; i < length; i++)
                _text += NormalGlyphs[Int(0, NormalGlyphs.Length - 1)];

            return _text;
        }
        
        
        
        /// <summary>
        /// Gets a random string of the length entered, good for ID's for things...
        /// </summary>
        /// <param name="prefix">The prefix for the string....</param>
        /// <param name="length">How many characters you want to have in the string...</param>
        /// <returns>String</returns>
        public static string String(string prefix, int length = 10)
        {
            var _text = prefix;

            for (var i = 0; i < length; i++)
                _text += NormalGlyphs[Int(0, NormalGlyphs.Length - 1)];

            return _text;
        }


        /// <summary>
        /// Gets a random value between a minmax range...
        /// </summary>
        /// <param name="value">The range to check</param>
        /// <returns>The value it chooses</returns>
        public static float MinMax(MinMax value)
        {
            return Float(value.min, value.max);
        }
    }
}
