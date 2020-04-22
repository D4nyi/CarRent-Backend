using System;
using System.Runtime.Serialization;

namespace CarRent.Exceptions
{

    [Serializable]
    public class InvalidCredentialsException : ArgumentException
    {
        public InvalidCredentialsException() { }
        public InvalidCredentialsException(string message) : base(message) { }
        public InvalidCredentialsException(string message, Exception inner) : base(message, inner) { }
        public InvalidCredentialsException(string message, string paramName) : base(message, paramName) { }
        public InvalidCredentialsException(string message, string paramName, Exception innerException) : base(message, paramName, innerException) { }

        protected InvalidCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
