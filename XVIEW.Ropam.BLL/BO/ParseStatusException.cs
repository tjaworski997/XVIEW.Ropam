using System;
using System.Runtime.Serialization;

namespace XVIEW.Ropam.BLL.BO
{
    [Serializable]
    internal class ParseStatusException : Exception
    {
        public ParseStatusException()
        {
        }

        public ParseStatusException(string message) : base(message)
        {
        }

        public ParseStatusException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ParseStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}