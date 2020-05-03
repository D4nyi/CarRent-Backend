using System;

namespace CarRent.Exceptions
{

    [Serializable]
    public class EntryNotFoundException : Exception
    {
        public EntryNotFoundException() { }
        public EntryNotFoundException(string message) : base(message) { }
        public EntryNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected EntryNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
