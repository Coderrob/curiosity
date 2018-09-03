using System;

namespace NASA.Api.Utilities
{
    public static class DateTimeExtensions
    {
        public static string ToEarthDateString(this DateTime? date)
        {
            return date.HasValue
                    ? date.Value.ToString("yyyy-MM-dd")
                    : string.Empty;
        }
    }
}