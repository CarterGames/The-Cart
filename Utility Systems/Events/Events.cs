// ----------------------------------------------------------------------------
// Events.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 15/02/2022
// ----------------------------------------------------------------------------

namespace GameEvents
{
    public static class Events
    {
        public static readonly Evt<bool> OnGamePaused = new Evt<bool>();

        public static readonly Evt<bool> OnJournalOpen = new Evt<bool>();
    }
}