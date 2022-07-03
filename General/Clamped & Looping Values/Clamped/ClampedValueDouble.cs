// ----------------------------------------------------------------------------
// ClampedValueDouble.cs
// 
// Description: A helper class to automatically clamp a value to a range.
//              Useful for menus & UI elements.
// ----------------------------------------------------------------------------

namespace Scarlet.General
{
    public class ClampedValueDouble : ValueSystemBase<double>
    {
        /// <summary>
        /// Converts the value to its read value implicitly for use.
        /// </summary>
        /// <param name="clampedValue">The value to convert</param>
        /// <returns>Int</returns>
        public static implicit operator double(ClampedValueDouble clampedValue)
        {
            return clampedValue.Value;
        }
        
        
        /// <summary>
        /// Constructor | Creates a new clamped int value.
        /// </summary>
        /// <param name="minValue">The min/lower value</param>
        /// <param name="maxValue">The max/upper value</param>
        public ClampedValueDouble(double minValue, double maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            currentValue = minValue;
        }
        
        
        /// <summary>
        /// Constructor | Creates a new clamped int value.
        /// </summary>
        /// <param name="minValue">The min/lower value</param>
        /// <param name="maxValue">The max/upper value</param>
        /// <param name="startingValue">The starting value</param>
        public ClampedValueDouble(double minValue, double maxValue, double startingValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            currentValue = startingValue;
        }
        
        
        /// <summary>
        /// Increments the value by the amount entered.
        /// </summary>
        /// <param name="amount">The amount to increment by</param>
        public void IncrementValue(double amount)
        {
            currentValue = ValueChecks(amount);
        }
        
        
        protected override double ValueChecks(double adjustment)
        {
            currentValue += adjustment;
            
            if (currentValue < minValue)
                return minValue;
            if (currentValue > maxValue)
                return maxValue;

            return currentValue;
        }
    }
}