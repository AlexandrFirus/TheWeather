using System;

namespace TheWeather.Helpers
{
    public static class TimeStampHelper
    {
        public static DateTime UnixTimeStampToDateTime(int? unixTimeStamp)
        {
            return unixTimeStamp == null ? DateTime.MinValue :
                DateTime.UnixEpoch.AddSeconds(unixTimeStamp.Value).ToLocalTime();
        }
    }
}
