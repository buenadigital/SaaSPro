using System;
using System.ComponentModel;
using System.Reflection;

namespace SaaSPro.Common.Helpers
{
    public static class Utilities
    {
        public static string ToDescriptionString(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

    }
}
