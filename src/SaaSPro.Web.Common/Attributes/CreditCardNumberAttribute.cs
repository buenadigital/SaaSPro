using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SaaSPro.Web.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    public class CreditCardNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (string.IsNullOrEmpty((string)value)) return false;
            if (value.ToString().Trim().StartsWith("*")) return true;

            var regex = new Regex(@"\D+");
            var cardNumber = regex.Replace((string)value, string.Empty);

            return IsCardNumberValid(cardNumber);
        }

        private static bool IsCardNumberValid(string cardNumber)
        {
            int i, checkSum = 0;

            // Compute checksum of every other digit starting from right-most digit
            for (i = cardNumber.Length - 1; i >= 0; i -= 2)
                checkSum += (cardNumber[i] - '0');

            // Now take digits not included in first checksum, multiple by two,
            // and compute checksum of resulting digits
            for (i = cardNumber.Length - 2; i >= 0; i -= 2)
            {
                int val = ((cardNumber[i] - '0') * 2);
                while (val > 0)
                {
                    checkSum += (val % 10);
                    val /= 10;
                }
            }

            // Number is valid if sum of both checksums MOD 10 equals 0
            return ((checkSum % 10) == 0);
        }
    }
}