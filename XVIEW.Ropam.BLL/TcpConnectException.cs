using System;
using System.Runtime.Serialization;

namespace XVIEW.Ropam.BLL
{
    [Serializable]
    internal class TcpConnectException : Exception
    {
        public TcpConnectException()
        {
        }

        public TcpConnectException(string message) : base(message)
        {
        }

        public TcpConnectException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TcpConnectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}