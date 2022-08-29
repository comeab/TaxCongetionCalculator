using System;
using System.Runtime.Serialization;

namespace Application.Exceptions
{
    [Serializable]
    public class TaxParametersNotFoundException : Exception
    {
        public TaxParametersNotFoundException()
        {
        }

        public TaxParametersNotFoundException(string message) : base(message)
        {
        }

        public TaxParametersNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TaxParametersNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}