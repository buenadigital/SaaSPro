using System;

namespace SaaSPro.Common
{
    public static class DecimalExtesions
    {
        public static string FormatSignificant(this decimal value)
        {
            for (var i = 0; i < 10; i++)
            {
                var result = Math.Round(value, i);
                if (result == value)
                {
                    return string.Format("{0:F" + i + "}", value);
                }
            }
            return $"{value:F10}";
        }
    }
}
