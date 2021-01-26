using System;
using System.Runtime.Serialization;

namespace XVIEW.Ropam.BLL.BO
{
    [Serializable]
    internal class NotLoggedException : Exception
    {
        public NotLoggedException()
        {
        }

        public NotLoggedException(string message) : base(message)
        {
        }

        public NotLoggedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotLoggedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}