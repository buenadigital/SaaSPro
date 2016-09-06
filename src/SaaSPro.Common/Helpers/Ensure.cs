using System;

namespace SaaSPro.Common.Helpers
{
    public static class Ensure
    {
        /// <summary>
        /// Ensures that the given expression is true
        /// </summary>
        /// <typeparam name="TException">Type of exception to throw</typeparam>
        /// <param name="condition">Condition to test/ensure</param>
        /// <param name="message">Message for the exception</param>
        /// <exception>Thrown when
        ///     <cref>TException</cref>
        ///     <paramref name="condition"/> is false</exception>
        /// <remarks><see cref="TException"/> must have a constructor that takes a single string</remarks>
        public static void That<TException>(bool condition, string message = "") where TException : Exception
        {
            if (!condition)
                throw (TException)Activator.CreateInstance(typeof(TException), message);
        }

        /// <summary>
        /// Ensures given object is not null
        /// </summary>
        /// <param name="value">Value of the object to test for null reference</param>
        /// <param name="message">Message for the Null Reference Exception</param>
        /// <exception cref="System.NullReferenceException">Thrown when <paramref name="value"/> is null</exception>
        public static void NotNull(object value, string message = "")
        {
            That<NullReferenceException>(value != null, message);
        }

        /// <summary>
        /// Argument-specific ensure methods
        /// </summary>
        public static class Argument
        {
            /// <summary>
            /// Ensures given condition is true
            /// </summary>
            /// <param name="condition">Condition to test</param>
            /// <param name="message">Message of the exception if condition fails</param>
            /// <exception cref="System.ArgumentException">
            ///     Thrown if <paramref cref="condition"/> is false
            /// </exception>
            public static void Is(bool condition, string message = "")
            {
                That<ArgumentException>(condition, message);
            }

            /// <summary>
            /// Ensures given value is not null
            /// </summary>
            /// <param name="value">Value to test for null</param>
            /// <param name="paramName">Name of the parameter in the method</param>
            /// <exception cref="System.ArgumentNullException">
            ///     Thrown if <paramref cref="value" /> is null
            /// </exception>
            public static void NotNull(object value, string paramName = "")
            {
                That<ArgumentNullException>(value != null, paramName);
            }

            /// <summary>
            /// Ensures the given string value is not null or empty
            /// </summary>
            /// <param name="value">Value to test for null or empty</param>
            /// <param name="paramName">Name of the parameter in the method</param>
            /// <exception cref="System.ArgumentException">
            ///     Thrown if <paramref cref="value"/> is null or empty string
            /// </exception>
            public static void NotNullOrEmpty(string value, string paramName = "")
            {
                if (!String.IsNullOrEmpty(value)) return;
                if (String.IsNullOrEmpty(paramName))
                    throw new ArgumentException("String value cannot be empty");

                throw new ArgumentException("String parameter " + paramName + " cannot be null or empty", paramName);
            }
        }
    }
}