using System.Numerics;

namespace DVotingBackendApp.utils
{
    public static class DateTimeUtils
    {

        public static string ToDateString(DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString("yyyy-MM-dd");
        }

        public static DateTime FromDateString(string dateString)
        {
            return DateTime.ParseExact(dateString, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        }

    }
}
