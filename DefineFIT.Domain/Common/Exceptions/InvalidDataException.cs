using DefineFIT.Domain.Common.Extensions;
using System.Runtime.Serialization;

namespace DefineFIT.Domain.Common.Exceptions;

[Serializable]
public class InvalidDataException : Exception, IBusinessException
{   
    public InvalidDataException(ExceptionInfo exceptionInfo) : base(exceptionInfo.ToMessage())
    {
    }

    public InvalidDataException(IEnumerable<ExceptionInfo> exceptionInfos)
        : base(exceptionInfos.ToMessage())
    {
    }

    protected InvalidDataException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}