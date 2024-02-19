using DefineFIT.Domain.Common.Extensions;
using System.Runtime.Serialization;

namespace DefineFIT.Domain.Common.Exceptions;

[Serializable]
public class InternalServerErrorException : Exception, IBusinessException
{
    public InternalServerErrorException(ExceptionInfo exceptionInfo) : base(exceptionInfo.ToMessage())
    {
    }

    public InternalServerErrorException(IEnumerable<ExceptionInfo> exceptionInfos)
        : base(exceptionInfos.ToMessage())
    {
    }

    protected InternalServerErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}