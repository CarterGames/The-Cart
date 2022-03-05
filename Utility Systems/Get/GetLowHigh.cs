using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JTools
{
    /// <summary>
    /// Gets High, Low or in the middle numbers or indexes based on inputted arrays or lists...
    /// </summary>
    public static class GetLowHigh
    {
        /// <summary>
        /// Gets the lowest int in the int array entered and returns its value...
        /// </summary>
        /// <param name="toCheck">Int Array to check</param>
        /// <returns>Int</returns>
        public static int Lowest(IEnumerable<int> toCheck)
        {
            var _list = toCheck.ToList();
            _list.Sort();
            return _list[0];
        }
        
        
        /// <summary>
        /// Gets the lowest int in the int list entered and returns its value...
        /// </summary>
        /// <param name="toCheck">Int List to check</param>
        /// <returns>Int</returns>
        public static int Lowest(List<int> toCheck)
        {
            var _list = toCheck;
            _list.Sort();
            return _list[0];
        }
        
        
        /// <summary>
        /// Gets the median int (rounded up) in the int array entered and return its value...
        /// </summary>
        /// <param name="toCheck">Int Array to check</param>
        /// <returns>Int</returns>
        public static int MedianRoundUp(IEnumerable<int> toCheck)
        {
            var _list = toCheck.ToList();
            _list.Sort();
            return _list[Mathf.RoundToInt((_list.Count - 1) / 2)];
        }
        
        
        /// <summary>
        /// Gets the median int (rounded up) in the int list entered and return its value...
        /// </summary>
        /// <param name="toCheck">Int List to check</param>
        /// <returns>Int</returns>
        public static int MedianRoundUp(List<int> toCheck)
        {
            var _list = toCheck;
            _list.Sort();
            return _list[Mathf.RoundToInt((_list.Count - 1) / 2)];
        }
        
        
        /// <summary>
        /// Gets the median int in (rounded down) the int array entered and return its value...
        /// </summary>
        /// <param name="toCheck">Int Array to check</param>
        /// <returns>Int</returns>
        public static int MedianRoundDown(IEnumerable<int> toCheck)
        {
            var _list = toCheck.ToList();
            _list.Sort();
            return _list[Mathf.FloorToInt((_list.Count - 1) / 2)];
        }
        
        
        /// <summary>
        /// Gets the median int in (rounded down) the int list entered and return its value...
        /// </summary>
        /// <param name="toCheck">Int List to check</param>
        /// <returns>Int</returns>
        public static int MedianRoundDown(List<int> toCheck)
        {
            var _list = toCheck;
            _list.Sort();
            return _list[Mathf.FloorToInt((_list.Count - 1) / 2)];
        }
        
        
        /// <summary>
        /// Gets the lowest int in the int array entered and returns its value...
        /// </summary>
        /// <param name="toCheck">Int Array to check</param>
        /// <returns>Int</returns>
        public static int Highest(IEnumerable<int> toCheck)
        {
            var _list = toCheck.ToList();
            _list.Sort();
            return _list[_list.Count - 1];
        }
        

        /// <summary>
        /// Gets the lowest int in the int list entered and returns its value...
        /// </summary>
        /// <param name="toCheck">Int List to check</param>
        /// <returns>Int</returns>
        public static int Highest(List<int> toCheck)
        {
            var _list = toCheck;
            _list.Sort();
            return _list[_list.Count - 1];
        }
        

        /// <summary>
        /// Gets the highest int in the list and returns its index value...
        /// </summary>
        /// <param name="toCheck">Int List to check</param>
        /// <returns>Int</returns>
        public static int GetHighestIndex(List<int> toCheck)
        {
            var _highestValue = int.MinValue;
            var _highestIndex = -1;

            for (var i = 0; i < toCheck.Count; i++)
            {
                if (toCheck[i] < _highestValue) continue;
                _highestValue = toCheck[i];
                _highestIndex = i;
            }

            return _highestIndex;
        }
        

        /// <summary>
        /// Gets the lowest index in the array and returns its value...
        /// </summary>
        /// <param name="toCheck">Int Array to check</param>
        /// <returns>Int</returns>
        public static int GetLowestIndex(int[] toCheck)
        {
            var _lowestValue = int.MaxValue;
            var _lowestIndex = -1;

            for (var i = 0; i < toCheck.Length; i++)
            {
                if (toCheck[i] > _lowestValue) continue;
                _lowestValue = toCheck[i];
                _lowestIndex = i;
            }

            return _lowestIndex;
        }
        
        
        /// <summary>
        /// Gets the lowest index in the list and returns its value...
        /// </summary>
        /// <param name="toCheck">Int List to check</param>
        /// <returns>Int</returns>
        public static int GetLowestIndex(List<int> toCheck)
        {
            var _lowestValue = int.MaxValue;
            var _lowestIndex = -1;

            for (var i = 0; i < toCheck.Count; i++)
            {
                if (toCheck[i] > _lowestValue) continue;
                
                _lowestValue = toCheck[i];
                _lowestIndex = i;
            }

            return _lowestIndex;
        }
        
        
        /// <summary>
        /// Gets the lowest index in the int array passed in...
        /// </summary>
        /// <param name="toCheck">Array to check...</param>
        /// <param name="ignoreLowerThan">Ignore values lower than....</param>
        /// <returns>List of Ints</returns>
        public static int GetLowestIndex(int[] toCheck, int ignoreLowerThan)
        {
            return GetLowestIndexBase(toCheck.ToList(), 1, ignoreLowerThan, false)[0];
        }


        /// <summary>
        /// Gets the lowest index in the int list passed in...
        /// </summary>
        /// <param name="toCheck">List to check...</param>
        /// <param name="ignoreLowerThan">Ignore values lower than....</param>
        /// <returns>List of Ints</returns>
        public static int GetLowestIndex(List<int> toCheck, int ignoreLowerThan)
        {
            return GetLowestIndexBase(toCheck, 1, ignoreLowerThan, false)[0];
        }
        

        /// <summary>
        /// Gets the lowest index in the int array passed in...
        /// </summary>
        /// <param name="toCheck">Array to check...</param>
        /// <param name="times">How many times it should run...</param>
        /// <param name="ignoreLowerThan">Ignore values lower than....</param>
        /// <returns>List of Ints</returns>
        public static int[] GetLowestIndex(int[] toCheck, int times, int ignoreLowerThan)
        {
            return GetLowestIndexBase(toCheck.ToList(), times, ignoreLowerThan, false).ToArray();
        }
        
        
        /// <summary>
        /// Gets the lowest index in the int list passed in...
        /// </summary>
        /// <param name="toCheck">List to check...</param>
        /// <param name="times">How many times it should run...</param>
        /// <param name="ignoreLowerThan">Ignore values lower than....</param>
        /// <returns>List of Ints</returns>
        public static List<int> GetLowestIndex(List<int> toCheck, int times, int ignoreLowerThan)
        {
            return GetLowestIndexBase(toCheck, times, ignoreLowerThan, false);
        }
        
        
        /// <summary>
        /// Gets the lowest index in the int list passed in...
        /// </summary>
        /// <param name="toCheck">List to check...</param>
        /// <param name="times">How many times it should run...</param>
        /// <param name="ignoreLowerThan">Ignore values lower than....</param>
        /// <returns>List of Ints</returns>
        public static List<int> GetLowestIndex(List<int> toCheck, int times, int ignoreLowerThan, bool allowDups)
        {
            return GetLowestIndexBase(toCheck, times, ignoreLowerThan, allowDups);
        }
        
        
        /// <summary>
        /// The lower index base code, seperated to help reduce duplicate code xD.
        /// </summary>
        /// <param name="toCheck">List of ints to check</param>
        /// <param name="times">Times this should loop</param>
        /// <param name="ignoreLowerThan">ignore lower than</param>
        /// <returns>List of Ints</returns>
        /// <remarks>
        /// Returns -1 if there were no other indexes to find....
        /// It's a good idea to check for -1 in the result before doing any other code...
        /// </remarks>
        private static List<int> GetLowestIndexBase(List<int> toCheck, int times, int ignoreLowerThan, bool allowDups)
        {
            int _lowestValue = int.MaxValue;
            int _lowestIndex = -1;
            var _lowestValues = new List<int>();
            var _lowestIndexes = new List<int>();
            
            // For as many times as this needs to run...
            for (int j = 0; j < times; j++)
            {
                for (int i = 0; i < toCheck.Count; i++)
                {
                    // if the value to check is higher than the lowest value... then ignore this value...
                    if (toCheck[i] > _lowestValue) continue;
                    
                    // If the value to check is lower than the lowest value accepted... then ignore this value...
                    if (toCheck[i] <= ignoreLowerThan) continue;

                    // If it is not the first time running this loop... cont...
                    if (!j.Equals(0))
                    {
                        if (!allowDups)
                        {
                            // If the index currently been checked is already in the list... then ignore this value...
                            if (_lowestValues.Contains(toCheck[i])) continue;
                        }

                        // If the index is already defined... then ignore this value...
                        if (_lowestIndexes.Contains(i)) continue;

                        // Makes the to check the lowest index found...
                        _lowestValue = toCheck[i];
                        _lowestIndex = i;
                    }
                    else
                    {
                        // Makes the to check the lowest index found...
                        _lowestValue = toCheck[i];
                        _lowestIndex = i;
                    }
                }

                if (!_lowestIndex.Equals(-1))
                    _lowestValues.Add(toCheck[_lowestIndex]);
                
                _lowestIndexes.Add(_lowestIndex);
                _lowestIndex = -1;
                _lowestValue = int.MaxValue;
            }
            
            return _lowestIndexes;
        }
    }
}
