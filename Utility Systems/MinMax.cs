using System;

namespace JTools
{
    /// <summary>
    /// A min/max range class that can come in handy when you want to have ranges for values...
    /// </summary>
    [Serializable]
    public class MinMax
    {
        /// <summary>
        /// The min value the range allows for...
        /// </summary>
        public float min;
        
        /// <summary>
        /// The max value the range allows for...
        /// </summary>
        public float max = 1;
        
        
        /// <summary>
        /// Default Constructor...
        /// </summary>
        /// <remarks>Min = 0, Max = 1</remarks>
        public MinMax(){}


        /// <summary>
        /// Constructor... Makes a new min/max with the enter values...
        /// </summary>
        /// <param name="_min">The min value for the range...</param>
        /// <param name="_max">The max value for the range...</param>
        public MinMax(float _min, float _max)
        {
            min = _min;
            max = _max;
        }


        /// <summary>
        /// Increases the size of the min/max by the amount entered...
        /// </summary>
        /// <param name="amount">Amount to increase by...</param>
        public void IncreaseMinMax(float amount)
        {
            if (min.Equals(0) && max.Equals(0))
            {
                max = amount;
                return;
            }

            min += amount;
            max += amount;
        }


        /// <summary>
        /// Reset the min/max to default values...
        /// </summary>
        public void ResetMinMax()
        {
            min = 0;
            max = 0;
        }
    }
}