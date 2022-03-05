namespace JTools
{
    /// <summary>
    /// Get a formatted position based on the number entered
    /// So 1 = 1st, 2 = 2nd, 3 = 3rd, 4 = 4th, 21 = 21st, 22 = 22nd, 23 = 23rd, 24 = 24th etc.
    /// </summary>
    public static class PositionFormat
    {
        /// <summary>
        /// Returns the position entered formatted with the correct ending characters as a string...
        /// </summary>
        /// <param name="pos">The position to get</param>
        /// <returns>String of the position with the correct ending characters...</returns>
        public static string GetFormattedPosition(int pos)
        {
            var _pos = pos.ToString();
            
            // Catches the 11,12,13th's as they are different...
            if (_pos.Contains("11"))
                return $"{_pos}th";
            if (_pos.Contains("12"))
                return $"{_pos}th";
            if (_pos.Contains("13"))
                return $"{_pos}th";

            var _toCheck = int.Parse(_pos.Substring(_pos.Length - 1, 1));

            return _toCheck switch
            {
                1 => $"{_pos}st",
                2 => $"{_pos}nd",
                3 => $"{_pos}rd",
                _ => $"{_pos}th"
            };
        }
    }
}