using System;
using System.Runtime.Serialization;

namespace XVIEW.Ropam.BLL.BO
{
    [Serializable]
    internal class BadFrameException : Exception
    {
        public BadFrameException()
        {
        }

        public BadFrameException(string message) : base(message)
        {
        }

        public BadFrameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadFrameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}