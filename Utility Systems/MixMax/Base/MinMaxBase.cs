// ----------------------------------------------------------------------------
// MinMaxBase.cs
// 
// Description: A base class for the min/max value system.
// ----------------------------------------------------------------------------

using System;

namespace Adriana
{
    [Serializable]
    public abstract class MinMaxBase<T>
    {
        /// <summary>
        /// The min value the range allows for...
        /// </summary>
        public T min;
        
        /// <summary>
        /// The max value the range allows for...
        /// </summary>
        public T max;
        
        
        /// <summary>
        /// Default Constructor...
        /// </summary>
        /// <remarks>Min = 0, Max = 1</remarks>
        protected MinMaxBase(){}


        /// <summary>
        /// Constructor... Makes a new min/max with the enter values...
        /// </summary>
        /// <param name="min">The min value for the range...</param>
        /// <param name="max">The max value for the range...</param>
        protected MinMaxBase(T min, T max)
        {
            this.min = min;
            this.max = max;
        }


        /// <summary>
        /// Increases the size of the min/max by the amount entered...
        /// </summary>
        /// <param name="amount">Amount to increase by...</param>
        public virtual void IncreaseMinMax(T amount)
        {
            if (min.Equals(0) && max.Equals(0))
            {
                max = amount;
                return;
            }
        }


        /// <summary>
        /// Reset the min/max to default values...
        /// </summary>
        public virtual void ResetMinMax()
        {
        }
    }
}