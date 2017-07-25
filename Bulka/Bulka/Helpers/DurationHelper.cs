using System;

namespace Bulka.Helpers
{
    public static class DurationHelper
    {
        public static string GetDuration(DateTime begin, DateTime end)
        {
            var subtract = end.Subtract(begin);
            return GetDuration(subtract);
        }

        public static string GetDuration(long ticks)
        {
            var timeSpan = new TimeSpan(ticks);
            return GetDuration(timeSpan);
        }

        private static string GetDuration(TimeSpan timeSpan)
        {
            return string.Format("{0:00}:{1:00}", (int)timeSpan.TotalHours, timeSpan.Minutes);
        }
    }
}