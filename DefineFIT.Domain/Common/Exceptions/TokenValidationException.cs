using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DefineFIT.Domain.Common.Exceptions
{
    [Serializable]

    public class TokenValidationException : Exception, IBusinessException
    {
        public TokenValidationException(string message)
            : base(message)
        {
        }

        public TokenValidationException(string message, Exception e)
            : base(message, e)
        {
        }

        protected TokenValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
