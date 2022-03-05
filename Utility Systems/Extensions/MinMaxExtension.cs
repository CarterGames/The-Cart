namespace JTools
{
    public static class MinMaxExtension
    {
        /// <summary>
        /// Gets a random value between the 2 ranges in the provided min/max...
        /// </summary>
        /// <returns>Random value within the range...</returns>
        public static float Random(this MinMax minMax)
        {
            return JTools.GetRandom.Float(minMax.min, minMax.max);
        }
    }
}