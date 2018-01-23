using System;

namespace Triad.CabinetOffice.Slipping.Data.Extensions
{
    public static class StringExtensions
    {
        public static string Left(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }
    }

    public static class DateTimeExtensions
    {
        public const string UkTimeZoneSystemId = "GMT Standard Time";
        public static DateTime ToUkTimeFromUtc(this DateTime dt)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dt, TimeZoneInfo.FindSystemTimeZoneById(UkTimeZoneSystemId));
        }

        public static DateTime ToUtcFromUkTime(this DateTime dt)
        {
            return TimeZoneInfo.ConvertTimeToUtc(dt, TimeZoneInfo.FindSystemTimeZoneById(UkTimeZoneSystemId));
        }
    }
}
