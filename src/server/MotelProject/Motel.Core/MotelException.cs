using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Motel.Core
{
    [Serializable]
    public class MotelException: Exception
    {
        public MotelException()
        {

        }
         public MotelException(string message)
            : base(message)
        {
        }
        public MotelException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
        }
        protected MotelException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
         public MotelException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
